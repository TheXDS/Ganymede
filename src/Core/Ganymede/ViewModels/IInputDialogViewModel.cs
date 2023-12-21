namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that includes a dialog
/// result.
/// </summary>
/// <typeparam name="T">Type of data returned by the dialog.</typeparam>
public interface IInputDialogViewModel<T> : IDialogViewModel
{
    /// <summary>
    /// Gets or sets the value associated with the dialog result.
    /// </summary>
    T Value { get; set; }
}
