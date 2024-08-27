using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a <see cref="ViewModel"/> with nested navigation capabilities.
/// </summary>
/// <param name="navigationService">
/// Navigation service to use for children navigation.
/// </param>
/// <param name="dialogService">
/// Dialog service to expose to ViewModels being navigated to.
/// </param>
public abstract class HostViewModelBase(INavigationService? navigationService, IDialogService? dialogService) : ViewModel
{
    private INavigationService? childNavService = navigationService;
    private IDialogService? childDlgService = dialogService;

    /// <summary>
    /// Gets or sets a reference to the child navigation service on this
    /// instance.
    /// </summary>
    /// <value>
    /// If the local instance of a navigation serivce is set to
    /// <see langword="null"/>, the parent
    /// <see cref="ViewModel.NavigationService"/> instance will be returned.
    /// </value>
    public INavigationService? ChildNavService
    {
        get => childNavService ?? NavigationService;
        set => childNavService = value;
    }

    /// <summary>
    /// Gets or sets a reference to the child dialog service on this instance.
    /// </summary>
    /// <value>
    /// If the local instance of a dialog serivce is set to
    /// <see langword="null"/>, the parent
    /// <see cref="ViewModel.DialogService"/> instance will be returned.
    /// </value>
    public IDialogService? ChildDialogService
    {
        get => childDlgService ?? DialogService;
        set => childDlgService = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HostViewModelBase"/>
    /// class, setting a new instance for the child navigation service and 
    /// reusing the dialog service from this instance to expose them to any
    /// children ViewModels.
    /// </summary>
    protected HostViewModelBase() : this(new HostNavigationService<ViewModel>(), null)
    {
        ((HostNavigationService<ViewModel>)ChildNavService!).SetHost(this);
    }

    /// <summary>
    /// Creates a new <see cref="ButtonInteraction"/> used to navigate to the
    /// specified <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IViewModel"/> to navigate to upon command
    /// invocation.
    /// </typeparam>
    /// <param name="label">Label to be displayed on the interaction.</param>
    /// <returns>
    /// A new <see cref="ButtonInteraction"/> that can be used to navigate to
    /// the specified <see cref="IViewModel"/>.
    /// </returns>
    protected ButtonInteraction CreateNavInteraction<TViewModel>(string label) where TViewModel : class, IViewModel, new()
    {
        return new(() => ChildNavService?.Navigate<TViewModel>(), label);
    }

    /// <summary>
    /// Creates a new <see cref="ButtonInteraction"/> used to navigate to the
    /// specified <see cref="IStatefulViewModel{TState}"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IStatefulViewModel{TState}"/> to navigate to upon
    /// command invocation.
    /// </typeparam>
    /// <typeparam name="TState">
    /// Type of the <see cref="IStatefulViewModel{TState}"/> state data.
    /// </typeparam>
    /// <param name="state">
    /// State data to be set onto the <see cref="IStatefulViewModel{TState}"/>
    /// upon navigation.
    /// </param>
    /// <param name="label">Label to be displayed on the interaction.</param>
    /// <returns>
    /// A new <see cref="ButtonInteraction"/> that can be used to navigate to
    /// the specified <see cref="IStatefulViewModel{TState}"/>.
    /// </returns>
    protected ButtonInteraction CreateNavInteraction<TViewModel, TState>(TState state, string label) where TViewModel : class, IStatefulViewModel<TState>, new()
    {
        return new(() => ChildNavService?.Navigate<TViewModel, TState>(state), label);
    }
}
