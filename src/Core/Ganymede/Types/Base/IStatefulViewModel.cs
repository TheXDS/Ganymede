namespace TheXDS.Ganymede.Types.Base;

/// <summary>
/// Defines an <see cref="IViewModel"/> that supports setting its initial state
/// using a predictable API.
/// </summary>
/// <typeparam name="TState">Type of state to be used.</typeparam>
public interface IStatefulViewModel<TState> : IViewModel
{
    /// <summary>
    /// Gets or sets the current ViewModel state data.
    /// </summary>
    TState State { get; set; }
}