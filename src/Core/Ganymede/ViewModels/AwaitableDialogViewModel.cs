namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all dialogs that implement their own await logic.
/// </summary>
public abstract class AwaitableDialogViewModel : DialogViewModel, IAwaitableDialogViewModel
{
    private TaskCompletionSource awaiter = new();

    Task IAwaitableDialogViewModel.DialogAwaiter => awaiter.Task;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    public void CloseDialog()
    {
        IsBusy = false;
        awaiter.SetResult();
        awaiter = new TaskCompletionSource();
    }
}
