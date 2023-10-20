using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
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
using TheXDS.Triton.Services;
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
    private readonly ObservableCollectionWrap<Model> _entities;
    private Model? _selectedEntity;
    private bool _isEditing = false;

    /// <summary>
    /// Contains the current navigation service used to handle navigation to
    /// detail and editor panels for any selected entity.
    /// </summary>
    public INavigationService CrudNavService { get; } = UiThread.Invoke(() => new NavigationService());

    /// <summary>
    /// Contains the current set of entities to be displayed on this ViewModel.
    /// </summary>
    public ICollection<Model> Entities => _entities;

    /// <summary>
    /// Gets an array of all defned descriptions for this CRUD page.
    /// </summary>
    public ICrudDescription[] Descriptions { get; }

    /// <summary>
    /// Gets an array of model types handled by this ViewModel.
    /// </summary>
    public Type[] Models => Descriptions.Select(p => p.Model).ToArray();

    /// <summary>
    /// Gets a reference to the command used to unselect any entity, navigating
    /// to the CRUD dashboard.
    /// </summary>
    public ICommand UnselectCommand { get; }

    /// <summary>
    /// Gets a reference to the command used to refresh the current dataset.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Gets a reference to the command used to create new entities.
    /// </summary>
    public ICommand NewCommand { get; }

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
    public ICollection<ButtonInteraction> CrudInteractions { get; } = new ObservableCollection<ButtonInteraction>();

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
    /// <remarks>
    /// When using this contructor, any create or update operation will only
    /// write the changes onto the dataset in memory, and refresh operations
    /// will not be available.
    /// </remarks>
    public CrudPageViewModel(ICollection<Model> entities, ICrudDescription[] descriptions)
    {
        Descriptions = descriptions;
        _entities = UiThread.Invoke(() => new ObservableCollectionWrap<Model>(entities));
        var b = new CommandBuilder<CrudPageViewModel>(this);
        UnselectCommand = b.BuildSimple(() => SelectedEntity = null);
        RefreshCommand = b.BuildBusyOperation(OnRefresh);
        NewCommand = b.BuildSimple(OnNew);
        UpdateInteraction = new(b.BuildObserving(OnUpdate).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), "Update");
        DeleteInteraction = new(b.BuildObserving(OnDelete).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), "Delete");
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudPageViewModel"/> class.
    /// </summary>
    /// <param name="descriptions">
    /// Model description for the entities.
    /// </param>
    /// <param name="dataService">
    /// Data service to use when saving an entity or updating the entity set.
    /// </param>
    public CrudPageViewModel(ICrudDescription[] descriptions, ITritonService dataService) : this(new List<Model>(), descriptions)
    {
        _dataService = dataService;
    }

    protected override async Task OnCreated()
    {
        if (!IsInitialized)
        { 
            await (DialogService?.RunOperation(OnRefresh) ?? OnRefresh(null));
        }
        await base.OnCreated();
    }

    private async Task OnNew()
    {
        ICrudDescription? desc = Descriptions.Length == 1 ? Descriptions[0] : await SelectNew();
        if (desc is null) return;
        Model e = desc.Model.New<Model>();
        if (await PresentEditingContent(e, desc, new(true, desc.Model)))
        {
            Entities.Add(e);
        }
    }

    private async Task<ICrudDescription?> SelectNew()
    {
        var options = Descriptions.Select(p => p.FriendlyName).ToArray();
        var i = await (DialogService?.SelectOption("new item", "Select the type of item to create", options) ?? Task.FromResult(-1));
        return i >= 0 ? Descriptions[i] : null;
    }

    private async Task OnRefresh(IProgress<ProgressReport>? status)
    {
        if (_dataService is null) return;
        SelectedEntity = null;
        status?.Report("Connecting...");
        await using var transaction = _dataService!.GetReadTransaction();
        status?.Report("Fetching data...");

        _entities.Clear();
        //TODO: perform paging here.
        _entities.Replace(await Models.SelectMany(t => All(transaction, t)).ToListAsync());
        if (!IsEditing) PresentRegularContent();
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
        UiThread.Invoke(() => CrudNavService.NavigateAndReset(Descriptions[0].DashboardViewModel?.New<ViewModel>()));
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
        CrudNavService.NavigateAndReset(null);
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

    private static QueryServiceResult<Model> All(ICrudReadTransaction transaction, Type model)
    {
        var m = transaction.GetType().GetMethod(nameof(All), 1, BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null)!.MakeGenericMethod(model);
        object o = m.Invoke(transaction, Array.Empty<object>())!;
        ServiceResult r = (ServiceResult)o;
        if (r.Success)
        {
            return new QueryServiceResult<Model>((IQueryable<Model>)o);
        }
        else
        {
            return new QueryServiceResult<Model>(r.Reason ?? FailureReason.Unknown, r.Message);
        }
    }
}
