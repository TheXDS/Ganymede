namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that includes an
/// object used to await for the completion of the dialog.
/// </summary>
public interface IAwaitableDialogViewModel<T> : IAwaitableDialogViewModel
{
    /// <summary>
    /// Gets a reference to the <see cref="Task"/> used to await for the
    /// completion of the dialog.
    /// </summary>
    new Task<T> DialogAwaiter { get; }

    Task IAwaitableDialogViewModel.DialogAwaiter => DialogAwaiter;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    /// <param name="result">
    /// Result of the dialog.
    /// </param>
    void CloseDialog(T result);

    void IAwaitableDialogViewModel.CloseDialog() => CloseDialog(default!);
}