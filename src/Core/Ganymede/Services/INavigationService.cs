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
    /// Gets a reference to the currently active <see cref="IViewModel"/>.
    /// </summary>
    IViewModel? CurrentViewModel { get; }

    /// <summary>
    /// Gets or sets the Navigation set's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation set will remain
    /// as-is, and upon a request to navigate back until the navigation set is
    /// empty, the active page will be set to this value.
    /// </remarks>
    IViewModel? HomePage { get; set; }

    /// <summary>
    /// Gets a reference to a command that can be used to navigate back in the
    /// navigation set.
    /// </summary>
    ICommand NavigateBackCommand { get; }

    /// <summary>
    /// Gets a value that indicates the size of the current navigation set.
    /// </summary>
    int NavigationSetCount { get; }

    /// <summary>
    /// Gets the current Navigation set.
    /// </summary>
    IEnumerable<IViewModel> NavigationSet { get; }

    /// <summary>
    /// Navigates to a specific ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If already present in the navigation
    /// set, the navigation will activate the existing ViewModel instance.
    /// </param>
    void Navigate(IViewModel viewModel);

    /// <summary>
    /// Navigates to a new instance of the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to. This method will always navigate to a
    /// new ViewModel instance.
    /// </typeparam>
    void Navigate<TViewModel>() where TViewModel : class, IViewModel, new() => Navigate(new TViewModel());

    /// <summary>
    /// Navigates to a new instance of the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to. This method will always navigate to a
    /// new ViewModel instance.
    /// </typeparam>
    /// <typeparam name="TState">
    /// State to be set onto the ViewModel.
    /// </typeparam>
    void Navigate<TViewModel, TState>(TState state) where TViewModel : class, IStatefulViewModel<TState>, new()
    {
        Navigate(new TViewModel() { State = state });
    }

    /// <summary>
    /// Clears the navigation set and inmediately navigates to the specified
    /// ViewModel instance.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel to navigate to. If <see langword="null"/>, the navigation
    /// set will be cleared.
    /// </param>
    void NavigateAndReset(IViewModel? viewModel);

    /// <summary>
    /// Clears the navigation set and inmeditely navigates to a new instance
    /// of the specified ViewModel type.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to.
    /// </typeparam>
    void NavigateAndReset<TViewModel>() where TViewModel : class, IViewModel, new() => NavigateAndReset(new TViewModel());

    /// <summary>
    /// Clears the navigation set and inmeditely navigates to a new instance
    /// of the specified ViewModel type.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to.
    /// </typeparam>
    /// <typeparam name="TState">
    /// State to be set onto the ViewModel.
    /// </typeparam>
    void NavigateAndReset<TViewModel, TState>(TState state) where TViewModel : class, IStatefulViewModel<TState>, new()
    {
        NavigateAndReset(new TViewModel() { State = state });
    }

    /// <summary>
    /// Clears the navigation set and inmeditely navigates to a new instance
    /// of the specified ViewModel type.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to navigate to.
    /// </typeparam>
    void NavigateAndReset<TViewModel>(object? state) where TViewModel : class, IStatefulViewModel<object?>, new()
    {
        NavigateAndReset<TViewModel, object?>(state);
    }

    /// <summary>
    /// Pops the current ViewModel out of the navigation set, and navigates
    /// to the previously active ViewModel.
    /// </summary>
    /// <remarks>
    /// If the navigation set is empty, This method will do nothing.
    /// </remarks>
    Task NavigateBack();

    /// <summary>
    /// Manually triggers the <see cref="NavigationCompleted"/> event.
    /// </summary>
    void Refresh();

    /// <summary>
    /// Clears the navigation set completely, navigating to the
    /// <see cref="ViewModel"/> set as the <see cref="HomePage"/>, or
    /// <see langword="null"/> if the home ViewModel has not been set.
    /// </summary>
    void Reset() => NavigateAndReset(null);
}
