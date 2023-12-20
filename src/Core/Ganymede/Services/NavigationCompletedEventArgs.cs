using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Includes event information for when an instance of
/// <see cref="INavigationService"/> completes navigation and triggers the
/// <see cref="INavigationService.NavigationCompleted"/> event.
/// </summary>
public class NavigationCompletedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NavigationCompletedEventArgs"/> class.
    /// </summary>
    /// <param name="viewModel">
    /// <see cref="IViewModel"/> that was navigated
    /// to.
    /// </param>
    public NavigationCompletedEventArgs(IViewModel? viewModel)
    {
        ViewModel = viewModel;
    }

    /// <summary>
    /// Gets a reference to the 
    /// <see cref="IViewModel"/> that was navigated
    /// to.
    /// </summary>
    public IViewModel? ViewModel { get; }
}
