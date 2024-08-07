namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all dialogs that implement their own await logic and can
/// return data.
/// </summary>
public abstract class AwaitableDialogViewModel<T> : DialogViewModel, IAwaitableDialogViewModel<T>
{
    private TaskCompletionSource<T> awaiter = new();

    Task<T> IAwaitableDialogViewModel<T>.DialogAwaiter => awaiter.Task;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    /// <param name="result">
    /// Result of the dialog.
    /// </param>
    public void CloseDialog(T result)
    {
        IsBusy = false;
        awaiter.SetResult(result);
        awaiter = new TaskCompletionSource<T>();
    }
}
