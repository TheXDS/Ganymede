using System.Collections.ObjectModel;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// ViewModel that allows a user to manage a set of entities, and exposes Crud
/// operations for them.
/// </summary>
public class CrudPageViewModel : ViewModel
{
    private readonly ITritonService? _dataService;
    private Model? _selectedEntity;
    private bool _isEditing = false;

    /// <summary>
    /// Contains the current navigation service used to handle navigation to
    /// detail and editor panels for any selected entity.
    /// </summary>
    public INavigationService CrudNavService { get; }

    /// <summary>
    /// Contains the current set of entities to be displayed on this ViewModel.
    /// </summary>
    public ICollection<Model> Entities { get; }

    /// <summary>
    /// Gets an array of model types handled by this ViewModel.
    /// </summary>
    public Type[] Models { get; }

    /// <summary>
    /// Enumerates all the commands available to create new entities.
    /// </summary>
    public IEnumerable<ButtonInteraction> NewCommands { get; }

    /// <summary>
    /// Gets a reference to the command used to edit the currently selected entity on the set.
    /// </summary>
    public ButtonInteraction UpdateInteraction { get; }

    /// <summary>
    /// Gets a reference to the command used to remove the currently selected entity from the set.
    /// </summary>
    public ButtonInteraction DeleteInteraction { get; }

    /// <summary>
    /// Enumerates the commands to be displayed below the generated CRUD page
    /// content.
    /// </summary>
    public ICollection<ButtonInteraction> CrudInteractions { get; }

    /// <summary>
    /// Gets an array of all defned descriptions for this CRUD page.
    /// </summary>
    public ICrudDescription[] Descriptions { get; }

    /// <summary>
    /// Gets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model? SelectedEntity
    {
        get => _selectedEntity;
        set
        {
            if (Change(ref _selectedEntity, value))
            {
                PresentRegularContent();
            }
        }
    }

    /// <summary>
    /// Gets a value that indicates if the ViewModel is currently editing an
    /// entity.
    /// </summary>
    public bool IsEditing
    { 
        get => _isEditing;
        private set => Change(ref _isEditing, value);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudPageViewModel"/> class.
    /// </summary>
    /// <param name="entities">
    /// Collection of entities to manage. May be a data set, or a
    /// sub-collection inside an entity.
    /// </param>
    /// <param name="descriptions">
    /// Model description for the entities.
    /// </param>
    /// <param name="dataService">
    /// Data service to use when saving an entity or updating the entity set.
    /// If not specified or set to <see langword="null"/>, a create or update
    /// operation will only write the changes onto the underlying entity and/or
    /// copy of the dataset in memory.
    /// </param>
    public CrudPageViewModel(ICollection<Model> entities, ICrudDescription[] descriptions, ITritonService? dataService = null)
    {
        CrudNavService = UiThread.Invoke(() => new NavigationService()); // <--------------------------------
        Descriptions = descriptions;
        Models = descriptions.Select(p => p.Model).ToArray();
        var b = new CommandBuilder<CrudPageViewModel>(this);
        Entities = UiThread.Invoke(() => new ObservableCollectionWrap<Model>(entities));
        NewCommands = descriptions.Select(t => CreateNewCommand(b, t)).ToArray();
        CrudInteractions = new ObservableCollection<ButtonInteraction>();
        UpdateInteraction = new(b.BuildObserving(OnUpdate).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), "Update");
        DeleteInteraction = new(b.BuildObserving(OnDelete).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), "Delete");
        _dataService = dataService;
    }

    private ButtonInteraction CreateNewCommand(CommandBuilder<CrudPageViewModel> builder, ICrudDescription desc)
    {
        return new(builder.BuildSimple(async () =>
        {
            var e = desc.Model.New<Model>();
            if (await PresentEditingContent(e, desc, new(true, desc.Model)))
            {
                Entities.Add(e);
            }
        }), desc.FriendlyName);
    }

    private Task OnUpdate()
    {
        if (SelectedEntity is null) return Task.CompletedTask;
        return PresentEditingContent(SelectedEntity, GetCurrentDescription(), new(false, SelectedEntity.GetType()));
    }

    private async Task OnDelete(IProgress<ProgressReport> progress)
    {
        if (await (DialogService?.Ask(St.AreYouSure) ?? Task.FromResult(true)))
        {
            Entities.Remove(SelectedEntity ?? throw new InvalidOperationException());
            await TrySaveData(progress, SelectedEntity!, (p, q) => p.Delete(q));
        }
    }

    private ICrudDescription GetCurrentDescription()
    {
        if (SelectedEntity is null) throw new TamperException();
        return Descriptions.First(p => p.Model == SelectedEntity.GetType());
    }

    private void PresentUnselectedContent()
    {
        CrudNavService.NavigateAndReset(null);
    }

    private void PresentSelectedContent()
    {
        var vm = ViewModelBuilder.BuildDetailsFrom(SelectedEntity!, GetCurrentDescription());
        vm.CrudActions = new ButtonInteraction[] {
            UpdateInteraction,
            DeleteInteraction
        };
        CrudNavService.NavigateAndReset(vm);
    }

    private void PresentRegularContent()
    {
        if (SelectedEntity is not null) PresentSelectedContent();
        else PresentUnselectedContent();
    }

    private async Task<bool> PresentEditingContent(Model entity, ICrudDescription description, CrudEditorViewModelContext context)
    {
        var retval = false;
        IsEditing = true;
        LaunchEditorSettings s = new()
        {
            Entity = entity,
            Description = description,
            Context = context,
            NavigationService = CrudNavService,
            DialogService = DialogService!,
        };
        if (await CrudCommon.LaunchEditor(s))
        {
            retval = (await DialogService!.RunOperation(p => TrySaveData(p, entity))) ?? true;
        }
        PresentRegularContent();
        IsEditing = false;
        return retval;
    }

    private Task<bool?> TrySaveData(IProgress<ProgressReport> progress, Model entity)
    {
        return TrySaveData(progress, entity, (p, q) => p.CreateOrUpdate(q));
    }

    private async Task<bool?> TrySaveData(IProgress<ProgressReport> progress, Model entity, Action<ICrudWriteTransaction, Model> operation)
    {
        if (_dataService is not null)
        {
            progress.Report(St.Saving);
            await using var svc = _dataService.GetWriteTransaction();
            operation.Invoke(svc, entity);
            return (await svc.CommitAsync()).Success;
        }
        return null;
    }
}
