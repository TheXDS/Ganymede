namespace TheXDS.Ganymede.Models;

/// <summary>
/// Flag used to indicate a request to cancel an operation from a related
/// resource.
/// </summary>
/// <param name="isCancelled">
/// Indicates the initial cancellation state.
/// </param>
public class CancelFlag(bool isCancelled = false) : IEquatable<CancelFlag>
{
    /// <summary>
    /// Indicates whether a cancellation has been requested.
    /// </summary>
    public bool IsCancelled { get; private set; } = isCancelled;

    /// <summary>
    /// Indicates intent to cancel.
    /// </summary>
    public void Cancel() => IsCancelled = true;


    bool IEquatable<CancelFlag>.Equals(CancelFlag? other)
    {
        return IsCancelled == other?.IsCancelled;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return IsCancelled.GetHashCode();
    }
}
