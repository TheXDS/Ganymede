using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Services.Base;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
using Pst = TheXDS.Ganymede.Resources.Strings.ProteusCommon;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.ViewModels.CustomDialogs;

/// <summary>
/// Implements an entity selection dialog ViewModel bound to a data source.
/// </summary>
public class DataCrudSelectorViewModel : AwaitableDialogViewModel
{
    private Model? _selectedEntity;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DataCrudSelectorViewModel"/> class.
    /// </summary>
    /// <param name="dataService">
    /// Data service to use when fetching entities.
    /// </param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if either <paramref name="dataService"/> or
    /// <paramref name="description"/> is null.
    /// </exception>
    public DataCrudSelectorViewModel(ITritonService dataService, ICrudDescription description)
    {
        Provider = new TritonEntityProvider(dataService, description);
        Icon = "👆";
        IconBgColor = System.Drawing.Color.SlateGray;
        Title = Pst.SelectItem;
        Message = Pst.SelectItemHelp;

        var cb = new CommandBuilder<DataCrudSelectorViewModel>(this);
        Interactions.Add(new(cb.BuildObserving(CloseDialog).CanExecuteIfNotNull(p => p.SelectedEntity).Build(), Pst.Select));
        Interactions.Add(new(cb.BuildSimple(OnCancel), St.Cancel));
    }

    /// <inheritdoc/>
    protected override async Task OnCreated()
    {
        Provider.DialogService = NavigationService as NavigatingDialogService;
        await (IsInitialized ? base.OnCreated() : Provider.FetchDataAsync());
    }

    /// <summary>
    /// Gets a reference to the Entity provider instance that contains the
    /// collection of entities selectable from this ViewModel.
    /// </summary>
    public IEntityProvider Provider { get; }

    /// <summary>
    /// Gets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model? SelectedEntity
    {
        get => _selectedEntity;
        set
        {
            Change(ref _selectedEntity, value);
        }
    }

    private void OnCancel()
    {
        SelectedEntity = null;
        CloseDialog();
    }
}
