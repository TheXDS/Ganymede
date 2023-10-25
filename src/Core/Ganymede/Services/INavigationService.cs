using System.Windows.Input;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that provides
/// consumers of ViewModel navigation services.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Triggers whenever navigation to a new ViewModel is completed.
    /// </summary>
    event EventHandler<NavigationCompletedEventArgs>? NavigationCompleted;

    /// <summary>
    /// Gets a reference to the currently active <see cref="ViewModel"/>.
    /// </summary>
    ViewModel? CurrentViewModel { get; }

    /// <summary>
    /// Gets or sets the Navigation Stack's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation stack will remain
    /// as-is, and upon a request to navigate back until the navigation stack
    /// is empty, the active page will be set to this value.
    /// </remarks>
    ViewModel? HomePage { get; set; }

    /// <summary>
    /// Gets a reference to a command that can be used to navigate back in the
    /// navigation stack.
    /// </summary>
    ICommand NavigateBackCommand { get; }

    /// <summary>
    /// Gets a value that indicates the depth of the current navigation stack.
    /// </summary>
    int NavigationStackDepth { get; }

    /// <summary>
    /// Gets the current Navigation Stack.
    /// </summary>
    IEnumerable<ViewModel> NavigationStack { get; }

    /// <summary>
    /// Navigates to a specific ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If already present in the navigation
    /// stack, the navigation will activate the existing ViewModel instance.
    /// </param>
    void Navigate(ViewModel viewModel);

    /// <summary>
    /// Navigates to a new instance of the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to. This method will always navigate to a
    /// new ViewModel instance.
    /// </typeparam>
    void Navigate<TViewModel>() where TViewModel : ViewModel, new() => Navigate(new TViewModel());

    /// <summary>
    /// Clears the navigation stack and inmediately navigates to the specified
    /// ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If <see langword="null"/>, the navigation
    /// stack will be cleared.
    /// </param>
    void NavigateAndReset(ViewModel? viewModel);

    /// <summary>
    /// Clears the navigation stack and inmeditely navigates to a new instance
    /// of the specified ViewModel type.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to.
    /// </typeparam>
    void NavigateAndReset<TViewModel>() where TViewModel : ViewModel, new() => NavigateAndReset(new TViewModel());

    /// <summary>
    /// Pops the current ViewModel out of the navigation stack, and navigates
    /// to the previously active ViewModel.
    /// </summary>
    /// <remarks>
    /// If the navigation stack is empty, This method will do nothing.
    /// </remarks>
    void NavigateBack();

    /// <summary>
    /// Manually triggers the <see cref="NavigationCompleted"/> event.
    /// </summary>
    void Refresh();

    /// <summary>
    /// Clears the navigation stack completely, navigating to the
    /// <see cref="ViewModel"/> set as the <see cref="HomePage"/>, or
    /// <see langword="null"/> if the home ViewModel has not been set.
    /// </summary>
    void Reset() => NavigateAndReset(null);
}

/// <summary>
/// Defines a set of members to be implemented by a type that provides
/// consumers of ViewModel navigation services.
/// </summary>
/// <typeparam name="TViewModel">
/// ViewModel type to expose.
/// </typeparam>
public interface INavigationService<TViewModel> : INavigationService where TViewModel : ViewModel
{
    /// <summary>
    /// Gets a reference to the currently active <see cref="ViewModel"/>.
    /// </summary>
    new TViewModel? CurrentViewModel { get; }

    ViewModel? INavigationService.CurrentViewModel => CurrentViewModel;

    /// <summary>
    /// Gets or sets the Navigation Stack's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation stack will remain
    /// as-is, and upon a request to navigate back until the navigation stack
    /// is empty, the active page will be set to this value.
    /// </remarks>
    new TViewModel? HomePage { get; set; }

    ViewModel? INavigationService.HomePage
    {
        get => HomePage;
        set => HomePage = value as TViewModel;
    }

    /// <summary>
    /// Gets the current Navigation Stack.
    /// </summary>
    new IEnumerable<TViewModel> NavigationStack { get; }

    IEnumerable<ViewModel> INavigationService.NavigationStack => NavigationStack;

    /// <summary>
    /// Navigates to a specific ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If already present in the navigation
    /// stack, the navigation will activate the existing ViewModel instance.
    /// </param>
    void Navigate(TViewModel viewModel);

    void INavigationService.Navigate(ViewModel viewModel) => Navigate((TViewModel)viewModel);

    /// <summary>
    /// Navigates to a new instance of the specified ViewModel.
    /// </summary>
    /// <typeparam name="T">
    /// Type of ViewModel to navigate to. This method will always navigate to a
    /// new ViewModel instance.
    /// </typeparam>
    new void Navigate<T>() where T : TViewModel, new() => Navigate(new T());

    void INavigationService.Navigate<T>() => Navigate(new T());

    /// <summary>
    /// Clears the navigation stack and inmediately navigates to the specified
    /// ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If <see langword="null"/>, the navigation
    /// stack will be cleared.
    /// </param>
    void NavigateAndReset(TViewModel? viewModel);

    void INavigationService.NavigateAndReset(ViewModel? viewModel) => NavigateAndReset(viewModel as TViewModel);

    /// <summary>
    /// Clears the navigation stack and inmeditely navigates to a new instance
    /// of the specified ViewModel type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of ViewModel to navigate to.
    /// </typeparam>
    new void NavigateAndReset<T>() where T : TViewModel, new() => NavigateAndReset(new T());

    void INavigationService.NavigateAndReset<T>() => NavigateAndReset(new T());
}
