namespace TheXDS.Ganymede.Models;

/// <summary>
/// Flag used to indicate a request to cancel an operation from a related
/// resource.
/// </summary>
/// <param name="IsCancelled">
/// Indicates whether or not a cancellation has been requested.
/// </param>
public record struct CancelFlag(bool IsCancelled)
{
    /// <summary>
    /// Indicates intent to cancel.
    /// </summary>
    public void Cancel() => IsCancelled = true;
}