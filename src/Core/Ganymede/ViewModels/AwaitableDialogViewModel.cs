using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a <see cref="IDialogViewModel"/> for all dialogs that implement 
/// their own await logic.
/// </summary>
public class AwaitableDialogViewModel : DialogViewModel, IAwaitableDialogViewModel
{
    private TaskCompletionSource awaiter = new();

    Task IAwaitableDialogViewModel.DialogAwaiter => awaiter.Task;

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    public void Close()
    {
        IsBusy = false;
        awaiter.SetResult();
        awaiter = new TaskCompletionSource();
    }
}
