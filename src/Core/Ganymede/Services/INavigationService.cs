using System.Windows.Input;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types.Base;

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
    ViewModel? CurrentViewModel
    {
        get;
    }

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
