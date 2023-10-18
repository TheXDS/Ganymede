namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that includes an
/// object used to await for the completion of the dialog.
/// </summary>
public interface IAwaitableDialogViewModel : IDialogViewModel
{
    /// <summary>
    /// Gets a reference to the <see cref="Task"/> used to await for the
    /// completion of the dialog.
    /// </summary>
    Task DialogAwaiter { get; }
}