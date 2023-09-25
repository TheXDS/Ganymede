using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Services;

public partial interface IDialogService
{
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
    Task RunOperation(Action<IProgress<ProgressReport>> operation) => RunOperation(null, operation);

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
    Task RunOperation(string? title, Action<IProgress<ProgressReport>> operation) => RunOperation(title, p => Task.Run(() => operation(p)));

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
    T RunOperation<T>(Func<IProgress<ProgressReport>, T> operation) where T : Task => RunOperation(null, operation);

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
    T RunOperation<T>(string? title, Func<IProgress<ProgressReport>, T> operation) where T : Task;

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
    Task<bool> RunOperation(Action<CancellationToken, IProgress<ProgressReport>> operation) => RunOperation(null, operation);

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
    Task<bool> RunOperation(string? title, Action<CancellationToken, IProgress<ProgressReport>> operation) => RunOperation(title, (ct, p) => Task.Run(() => operation(ct, p), ct));

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
    Task<bool> RunOperation(Func<CancellationToken, IProgress<ProgressReport>, Task> operation) => RunOperation(null, operation);

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
    Task<bool> RunOperation(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task> operation);
}
