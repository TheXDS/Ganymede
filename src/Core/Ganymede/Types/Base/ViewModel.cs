using TheXDS.Ganymede.Services;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Types.Base;

/// <summary>
/// Base class for all Ganymede ViewModels.
/// </summary>
public abstract class ViewModel : ViewModelBase, IViewModel, IViewModel_Internal
{
    private string? _Title;
    private IDialogService? dialogService;

    /// <summary>
    /// Gets a value that indicates if this ViewModel has been initialized.
    /// </summary>
    protected bool IsInitialized { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class.
    /// </summary>
    protected ViewModel()
    {
    }

    /// <summary>
    /// Gets or sets this ViewModel title.
    /// </summary>
    public string? Title
    {
        get => _Title;
        set => Change(ref _Title, value);
    }

    /// <summary>
    /// Gets a reference to the current dialog service.
    /// </summary>
    public IDialogService? DialogService
    {
        get => dialogService;
        set => dialogService = value;
    }

    /// <summary>
    /// Gets a reference to a navigation service bound to this instance.
    /// </summary>
    public INavigationService? NavigationService { get; set; }

    /// <summary>
    /// Called by the navigation service after a successful navigation to this
    /// ViewModel. Override this method to do lengthy initializations on the
    /// ViewModel, like fetching and preparing data to be displayed, etc.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the async operation.
    /// </returns>
    protected virtual Task OnCreated()
    {
        return Task.CompletedTask;
    }

    async Task IViewModel_Internal.InvokeOnCreated()
    {
        IsBusy = true;
        await OnCreated();
        IsInitialized = true;
        IsBusy = false;
    }

    bool IViewModel.IsBusy
    {
        get => IsBusy; 
        set => IsBusy = value;
    }
}
