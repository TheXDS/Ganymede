using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Includes event information for when an instance of
/// <see cref="INavigationService"/> completes navigation and triggers the
/// <see cref="INavigationService.NavigationCompleted"/> event.
/// </summary>
/// <param name="viewModel">
/// <see cref="IViewModel"/> that was navigated
/// to.
/// </param>
/// <param name="isReplacingView"><see langword="true"/> to indicate that the view is being replaced and not overlaid on the stack.</param>
public class NavigationCompletedEventArgs(IViewModel? viewModel, bool isReplacingView) : EventArgs
{
    /// <summary>
    /// Gets a reference to the 
    /// <see cref="IViewModel"/> that was navigated
    /// to.
    /// </summary>
    public IViewModel? ViewModel { get; } = viewModel;

    /// <summary>
    /// Gets a value that indicates if the navigation is replacing the stack with the current view.
    /// </summary>
    public bool IsReplacingView { get; } = isReplacingView;
}
