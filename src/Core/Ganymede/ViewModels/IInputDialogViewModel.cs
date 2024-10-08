namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that serves as a
/// ViewModel for value input dialogs with support for minimum and maximum
/// allowable values.
/// </summary>
/// <typeparam name="T">
/// Type of value to provide a minimum and maximum value.
/// </typeparam>
public interface IInputDialogViewModel<T> : IDialogViewModel where T : struct, IComparable<T>
{
    /// <summary>
    /// Gets or sets the maximum allowable value associated with this instance.
    /// </summary>
    T? Maximum { get; set; }

    /// <summary>
    /// Gets or sets the minimum allowable value associated with this instance.
    /// </summary>
    T? Minimum { get; set; }
}