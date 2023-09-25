using ReactiveUI;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents the routing state for a Routed ViewModel-View Host.
/// </summary>
public class UiPresenter: ReactiveObject, IScreen
{
    /// <summary>
    /// Implements the current routing state of the screen.
    /// </summary>
    public RoutingState Router { get; } = new();
}