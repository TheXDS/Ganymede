namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all dialogs that implement their own await logic.
/// </summary>
public abstract class AwaitableDialogViewModel : DialogViewModel, IAwaitableDialogViewModel
{
    private readonly TaskCompletionSource awaiter = new();

    Task IAwaitableDialogViewModel.DialogAwaiter => awaiter.Task;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    protected void CloseDialog() => awaiter.SetResult();
}

/// <summary>
/// Base class for all dialogs that implement their own await logic and can
/// return data.
/// </summary>
public abstract class AwaitableDialogViewModel<T> : DialogViewModel, IAwaitableDialogViewModel<T>
{
    private readonly TaskCompletionSource<T> awaiter = new();

    Task<T> IAwaitableDialogViewModel<T>.DialogAwaiter => awaiter.Task;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    /// <param name="result">
    /// Result of the dialog.
    /// </param>
    protected void CloseDialog(T result) => awaiter.SetResult(result);
}