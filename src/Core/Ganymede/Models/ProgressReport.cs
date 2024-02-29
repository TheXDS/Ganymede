namespace TheXDS.Ganymede.Models;

/// <summary>
/// Represents a progress status report from a long-running operation or task.
/// </summary>
/// <param name="Progress">
/// Progress value. Should be either in the range of <c>0</c> to <c>100</c>, or
/// equal to <see cref="double.NaN"/>.
/// </param>
/// <param name="Status">
/// Description of the current status. May be <see langword="null"/> or omitted
/// to indicate that the current status description has not changed.
/// </param>
public readonly record struct ProgressReport(double Progress, string? Status = null)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressReport"/> class,
    /// indicating an indeterminate progress and clearing the description
    /// status.
    /// </summary>
    public ProgressReport() : this(double.NaN, string.Empty) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressReport"/> class,
    /// indicating that the operation has indeterminate progress and changing
    /// the status to the specified string.
    /// </summary>
    /// <param name="status">
    /// <see cref="string"/> that describes the current status.
    /// </param>
    public ProgressReport(string status) : this(double.NaN, status) { }

    /// <summary>
    /// Implicitly converts a <see cref="double"/> into a
    /// <see cref="ProgressReport"/>.
    /// </summary>
    /// <param name="progress">Progress value.</param>
    /// <returns>
    /// A new instance of the <see cref="ProgressReport"/> struct that
    /// indicates the progress of the operation without changing its
    /// description.
    /// </returns>
    public static implicit operator ProgressReport(double progress) => new(progress);
    
    /// <summary>
    /// Implicitly converts a <see cref="string"/> into a
    /// <see cref="ProgressReport"/>.
    /// </summary>
    /// <param name="status">
    /// <see cref="string"/> that describes the current status.
    /// </param>
    /// <returns>
    /// A new instance of the <see cref="ProgressReport"/> struct that
    /// indicates indeterminate progress of the operation and changes the
    /// status description to the specified value.
    /// </returns>
    public static implicit operator ProgressReport(string status) => new(status);
}
