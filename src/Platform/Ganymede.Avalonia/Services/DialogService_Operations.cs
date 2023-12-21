using ReactiveUI;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Services;

public partial class DialogService
{
    /// <summary>
    /// Runs a long-running operation and displays a dialog that shows the
    /// progress of it.
    /// </summary>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that can be used to await the operation.
    /// The task will return <see langword="true"/> if the operation ran
    /// successfully, or <see langword="false"/> if it was cancelled by the
    /// user.
    /// </returns>
    public Task<bool> RunOperation(Action<CancellationToken, IProgress<ProgressReport>> operation)
        => RunOperation(null, operation);

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
    public Task<bool> RunOperation(string? title, Action<CancellationToken, IProgress<ProgressReport>> operation)
        => RunOperation(title, (ct, p) => Task.Run(() => operation(ct, p), ct));

    /// <summary>
    /// Runs a long-running operation and displays a dialog that shows the
    /// progress of it.
    /// </summary>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that can be used to await the operation.
    /// The task will return <see langword="true"/> if the operation ran
    /// successfully, or <see langword="false"/> if it was cancelled by the
    /// user.
    /// </returns>
    public Task<bool> RunOperation(Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
        => RunOperation(null, operation);

    /// <summary>
    /// Runs a long-running, non-cancellable operation and displays a dialog
    /// that shows the progress of it.
    /// </summary>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the operation.
    /// </returns>
    public Task RunOperation(Action<IProgress<ProgressReport>> operation)
        => RunOperation(null, operation);

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
    public Task RunOperation(string? title, Action<IProgress<ProgressReport>> operation)
        => RunOperation(title, p => Task.Run(() => operation(p)));

    /// <summary>
    /// Runs a long-running, non-cancellable operation and displays a dialog
    /// that shows the progress of it.
    /// </summary>
    /// <param name="operation">
    /// Operation to execute.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the operation.
    /// </returns>
    public Task RunOperation(Func<IProgress<ProgressReport>, Task> operation)
        => RunOperation(null, operation);

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
    public async Task<bool> RunOperation(string? title,
        Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        CancellationTokenSource ct = new();
        ButtonInteraction cancel = new(ReactiveCommand.Create(ct.Cancel), Common.Cancel);
        CreateOperationVm(title, out var vm, out var progress);
        vm.Interactions.Add(cancel);
        Content.Router.NavigateAndReset.Execute(vm);
        var task = operation.Invoke(ct.Token, progress);
        try { await task; }
        finally { Content.Router.NavigationStack.Clear(); }
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
    public async Task RunOperation(string? title, Func<IProgress<ProgressReport>, Task> operation)
    {
        CreateOperationVm(title, out var vm, out var progress);
        Content.Router.NavigateAndReset.Execute(vm);
        await operation.Invoke(progress);
        Content.Router.NavigationStack.Clear();
    }
}