using System.Drawing;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Component;

namespace TheXDS.Ganymede.Services;
public partial class NavigatingDialogService
{
    /// <summary>
    /// Runs a long-running operation and displays a dialog that shows the
    /// progress of it.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that can be used to await the operation.
    /// The task will return <see langword="true"/> if the operation ran
    /// successfully, or <see langword="false"/> if it was cancelled by the
    /// user.
    /// </returns>
    public async Task<bool> RunOperation(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        CancellationTokenSource ct = new();
        ButtonInteraction cancel = new(new SimpleCommand(ct.Cancel), "Cancel");
        var (vm, progress) = CreateOperationVm(title);
        vm.Interactions.Add(cancel);
        Navigate(vm);
        var task = operation.Invoke(ct.Token, progress);
        try { await task; }
        finally { NavigateBack(); }
        return !(ct.IsCancellationRequested || task.IsFaulted);
    }

    /// <summary>
    /// Runs a long-running, non-cancellable operation and displays a dialog
    /// that shows the progress of it.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the operation.
    /// </returns>
    public T RunOperation<T>(string? title, Func<IProgress<ProgressReport>, T> operation) where T : Task
    {
        var (vm, progress) = CreateOperationVm(title);
        Navigate(vm);
        var task = operation.Invoke(progress);
        try { return task; }
        finally { NavigateBack(); }
    }

    private static (OperationDialogViewModel viewModel, IProgress<ProgressReport> progress) CreateOperationVm(string? title)
    {
        var ivm = new OperationDialogViewModel
        {
            Title = title,
            Message = string.Empty,
            Progress = double.NaN,
            Icon = "⚙",
            IconBgColor = Color.DarkGray,
        };
        void ReportProgress(ProgressReport p)
        {
            ivm.Progress = p.Progress;
            if (p.Status is not null)
            {
                ivm.Message = p.Status;
            }
        }
        return (ivm, new Progress<ProgressReport>(ReportProgress));
    }
}
