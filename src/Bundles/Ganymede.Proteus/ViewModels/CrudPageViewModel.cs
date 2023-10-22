using System.Collections.ObjectModel;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Services.Base;
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
    private readonly IEntityProvider _entityProvider;
    private readonly ITritonService? _dataService;
    private Model? _selectedEntity;
    private bool _isEditing = false;
    private readonly ObservableCollectionWrap<Model> _entities = UiThread.Invoke(() => new ObservableCollectionWrap<Model>());

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

    public ICommand FirstPageCommand { get; }
    public ICommand LastPageCommand { get; }


    public ICommand NextPageCommand { get; }

    public ICommand PrevousPageCommand { get; }




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

    public int ItemsPerPage
    {
        get => _entityProvider.ItemsPerPage;
        set
        {
            _entityProvider.ItemsPerPage = value;
            DialogService!.RunOperation(OnRefresh);
        }
    }

    public IEntityProvider EntityProvider => _entityProvider;

    private CrudPageViewModel(ICrudDescription[] descriptions)
    {
        _entityProvider = null!;
        Descriptions = descriptions;
        var b = new CommandBuilder<CrudPageViewModel>(this);
        UnselectCommand = b.BuildSimple(() => SelectedEntity = null);
        RefreshCommand = b.BuildBusyOperation(OnRefresh);
        NewCommand = b.BuildSimple(OnNew);
        FirstPageCommand = b.BuildSimple(OnFirst);
        LastPageCommand = b.BuildSimple(OnLast);
        NextPageCommand = b.BuildObserving(OnNextPage).ListensTo(p => p.Entities).CanExecute(CanGoNext).Build();
        PrevousPageCommand = b.BuildObserving(OnPreviousPage).ListensTo(p => p.Entities).CanExecute(CanGoPrevious).Build();
        UpdateInteraction = new(b.BuildObserving(OnUpdate).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), St.Update);
        DeleteInteraction = new(b.BuildObserving(OnDelete).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), St.Delete);
    }

    private Task OnLast()
    {
        _entityProvider.Page = _entityProvider.TotalPages;
        return DialogService!.RunOperation(OnRefresh);
    }

    private Task OnFirst()
    {
        _entityProvider.Page = 1;
        return DialogService!.RunOperation(OnRefresh);
    }

    private Task OnNextPage()
    {
        _entityProvider.Page++;
        return DialogService!.RunOperation(OnRefresh);
    }

    private bool CanGoNext()
    {
        return _entityProvider.Page < _entityProvider.TotalPages;
    }

    private bool CanGoPrevious()
    {
        return _entityProvider.Page > 1;
    }

    private Task OnPreviousPage()
    {
        _entityProvider.Page--;
        return DialogService!.RunOperation(OnRefresh);
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
    public CrudPageViewModel(ICollection<Model> entities, ICrudDescription[] descriptions) : this(descriptions)
    {
        _entities.Substitute(entities);
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
    public CrudPageViewModel(ICrudDescription[] descriptions, ITritonService dataService) : this(descriptions)
    {
        _entityProvider = new DataboundEntityProvider(dataService, descriptions[0].Model);
        _dataService = dataService;
    }

    /// <inheritdoc/>
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
        await PresentEditingContent(e, desc, new(true, desc.Model, null, _dataService));
    }

    private async Task<ICrudDescription?> SelectNew()
    {
        var options = Descriptions.Select(p => p.FriendlyName).ToArray();
        var i = await (DialogService?.SelectOption(St.NewItem, St.NewItemHelp, options) ?? Task.FromResult(-1));
        return i >= 0 ? Descriptions[i] : null;
    }

    private async Task OnRefresh(IProgress<ProgressReport>? status)
    {
        if (_dataService is null) return;
        SelectedEntity = null;
        status?.Report(St.FetchingData);
        await _entityProvider.FetchDataAsync();
        _entities.Substitute(_entityProvider.Results.ToList());
        Notify(nameof(Entities), nameof(EntityProvider));
        if (!IsEditing) PresentRegularContent();
    }

    private Task OnUpdate()
    {
        if (SelectedEntity is null) return Task.CompletedTask;
        return PresentEditingContent(SelectedEntity, GetCurrentDescription(), new(false, SelectedEntity.GetType(), null, _dataService));
    }

    private async Task OnDelete(IProgress<ProgressReport> progress)
    {
        if (await (DialogService?.Ask(St.AreYouSure) ?? Task.FromResult(true)))
        {
            IsBusy = true;
            await TrySaveData(progress, SelectedEntity ?? throw new InvalidOperationException(), (p, q) => p.Delete(q));
            await OnRefresh(null);
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
            await OnRefresh(null);
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
