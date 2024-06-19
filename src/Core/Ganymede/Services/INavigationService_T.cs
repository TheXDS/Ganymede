using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that provides
/// consumers of ViewModel navigation services.
/// </summary>
/// <typeparam name="TViewModel">
/// ViewModel type to expose.
/// </typeparam>
public interface INavigationService<TViewModel> : INavigationService where TViewModel : class, IViewModel
{
    /// <summary>
    /// Gets a reference to the currently active
    /// <typeparamref name="TViewModel"/>.
    /// </summary>
    new TViewModel? CurrentViewModel { get; }

    IViewModel? INavigationService.CurrentViewModel => CurrentViewModel;

    /// <summary>
    /// Gets or sets the Navigation Stack's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation stack will remain
    /// as-is, and upon a request to navigate back until the navigation stack
    /// is empty, the active page will be set to this value.
    /// </remarks>
    new TViewModel? HomePage { get; set; }

    IViewModel? INavigationService.HomePage
    {
        get => HomePage;
        set => HomePage = value as TViewModel;
    }

    /// <summary>
    /// Gets the current Navigation Stack.
    /// </summary>
    new IEnumerable<TViewModel> NavigationStack { get; }

    IEnumerable<IViewModel> INavigationService.NavigationStack => NavigationStack;

    /// <summary>
    /// Navigates to a specific ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If already present in the navigation
    /// stack, the navigation will activate the existing ViewModel instance.
    /// </param>
    void Navigate(TViewModel viewModel);

    void INavigationService.Navigate(IViewModel viewModel) => Navigate((TViewModel)viewModel);

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

    void INavigationService.NavigateAndReset(IViewModel? viewModel) => NavigateAndReset(viewModel as TViewModel);

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
