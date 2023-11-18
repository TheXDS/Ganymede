using System.Collections.ObjectModel;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Services.Base;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Exceptions;
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
    private readonly IEntityProvider _entityProvider;
    private readonly ITritonService _tritonService;
    private Model? _selectedEntity;
    private bool _isEditing = false;

    /// <summary>
    /// Contains the current navigation service used to handle navigation to
    /// detail and editor panels for any selected entity.
    /// </summary>
    public INavigationService<CrudViewModelBase> CrudNavService { get; } = UiThread.Invoke(() => new NavigationService<CrudViewModelBase>());

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
    public ButtonInteraction UnselectInteraction { get; }

    /// <summary>
    /// Gets a reference to the command used to create new entities.
    /// </summary>
    public ButtonInteraction NewEntityInteraction { get; }

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
    /// Gets a reference to the Entity provider instance that contains the
    /// collection of entities managed by this ViewModel.
    /// </summary>
    public IEntityProvider EntityProvider => _entityProvider;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudPageViewModel"/> class.
    /// </summary>
    /// <param name="descriptions">
    /// Model description for the entities.
    /// </param>
    /// <param name="tritonService">
    /// Triton service instance to use for write operations.
    /// </param>
    /// <param name="entityProvider">
    /// Entity provider instance to use on this and any children ViewModel.
    /// </param>
    public CrudPageViewModel(ICrudDescription[] descriptions, ITritonService tritonService, IEntityProvider entityProvider)
    {
        _tritonService = tritonService;
        _entityProvider = entityProvider;
        Descriptions = descriptions;
        var b = new CommandBuilder<CrudPageViewModel>(this);
        UnselectInteraction = new(b.BuildSimple(() => SelectedEntity = null), St.Back);
        NewEntityInteraction = new(b.BuildSimple(OnNew), St.NewItem);
        UpdateInteraction = new(b.BuildObserving(OnUpdate).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), St.Update);
        DeleteInteraction = new(b.BuildObserving(OnDelete).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), St.Delete);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudPageViewModel"/> class.
    /// </summary>
    /// <param name="descriptions">
    /// Model description for the entities.
    /// </param>
    /// <param name="tritonService">
    /// Triton service instance to use for write operations.
    /// </param>
    public CrudPageViewModel(ICrudDescription[] descriptions, ITritonService tritonService)
        : this(descriptions, tritonService, new TritonFlatEntityProvider(tritonService, descriptions[0]))
    {
    }

    /// <inheritdoc/>
    protected override async Task OnCreated()
    {
        _entityProvider.DialogService = DialogService;
        await (IsInitialized ? base.OnCreated() : EntityProvider.FetchDataAsync());
        if (!IsEditing) PresentRegularContent();
    }

    private async Task OnNew()
    {
        ICrudDescription? desc = Descriptions.Length == 1 ? Descriptions[0] : await SelectNew();
        if (desc is null) return;
        Model e = desc.Model.New<Model>();
        await PresentEditingContent(e, desc, new(true, desc.Model, null, _tritonService));
    }

    private async Task<ICrudDescription?> SelectNew()
    {
        var options = Descriptions.Select(p => p.FriendlyName).ToArray();
        var i = await (DialogService?.SelectOption(St.NewItem, St.NewItemHelp, options) ?? Task.FromResult(-1));
        return i >= 0 ? Descriptions[i] : null;
    }

    private Task OnUpdate()
    {
        if (SelectedEntity is null) return Task.CompletedTask;
        return PresentEditingContent(SelectedEntity, GetCurrentDescription(), new(false, SelectedEntity.GetType(), null, _tritonService));
    }

    private async Task OnDelete(IProgress<ProgressReport> progress)
    {
        if (await (DialogService?.Ask(St.AreYouSure) ?? Task.FromResult(true)))
        {
            IsBusy = true;
            await TrySaveData(progress, SelectedEntity ?? throw new InvalidOperationException(), (p, q) => p.Delete(q));
            await EntityProvider.FetchDataAsync();
            IsBusy = false;
        }
    }

    private ICrudDescription GetCurrentDescription()
    {
        if (SelectedEntity is null) throw new TamperException();
        return Descriptions.First(p => p.Model == SelectedEntity.GetType());
    }

    private void PresentUnselectedContent()
    {
        var vm = Descriptions[0].DashboardViewModel?.New<CrudViewModelBase>() ?? new BlankCrudViewModel();
        vm.CrudActions = vm.CrudActions.Append(NewEntityInteraction).ToArray();
        UiThread.Invoke(() => CrudNavService.NavigateAndReset(vm));
    }

    private void PresentSelectedContent()
    {
        var vm = Descriptions[0].DetailsViewModel?.New<EntityCrudViewModelBase>() ?? ViewModelBuilder.BuildDetailsFrom(SelectedEntity!, GetCurrentDescription());
        vm.Entity = SelectedEntity!;
        vm.CrudActions = vm.CrudActions.Concat(new ButtonInteraction[] {
            UnselectInteraction,
            NewEntityInteraction,
            UpdateInteraction,
            DeleteInteraction
        }).ToArray();
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
            retval = await DialogService!.RunOperation(p => TrySaveData(p, entity));
            await EntityProvider.FetchDataAsync();
        }
        PresentRegularContent();
        IsEditing = false;
        return retval;
    }

    private Task<bool> TrySaveData(IProgress<ProgressReport> progress, Model entity)
    {
        return TrySaveData(progress, entity, (p, q) => p.CreateOrUpdate(q));
    }

    private async Task<bool> TrySaveData(IProgress<ProgressReport> progress, Model entity, Action<ICrudWriteTransaction, Model> operation)
    {
        progress.Report(St.Saving);
        await using var svc = _tritonService.GetWriteTransaction();
        operation.Invoke(svc, entity);
        return (await svc.CommitAsync()).Success;
    }
}
