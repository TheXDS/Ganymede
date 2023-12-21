using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Component;
using TheXDS.MCART.Helpers;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Class that simplifies the creation of several kinds of 
/// <see cref="ICommand"/> objects to be used inside a <see cref="ViewModel"/>.
/// </summary>
/// <typeparam name="TViewModel">
/// Type of <see cref="ViewModel"/> for which to generate
/// <see cref="ICommand"/> instances.
/// </typeparam>
public class CommandBuilder<TViewModel> where TViewModel : IViewModel
{
    private readonly TViewModel _vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandBuilder{TViewModel}"/> class.
    /// </summary>
    /// <param name="vm">
    /// <see cref="ViewModel"/> instance to use when defining new
    /// <see cref="ICommand"/> instances.
    /// </param>
    public CommandBuilder(TViewModel vm)
    {
        _vm = vm;
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Action action)
    {
        return ObservingCommandBuilder.Create(_vm, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified asyncronous
    /// operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Func<Task> action)
    {
        return ObservingCommandBuilder.Create(_vm, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Action<object?> action)
    {
        return ObservingCommandBuilder.Create(_vm, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified asyncronous
    /// operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Func<object?, Task> action)
    {
        return ObservingCommandBuilder.Create(_vm, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified asyncronous
    /// operation with support for progress reporting.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Func<IProgress<ProgressReport>, Task> action)
    {
        return ObservingCommandBuilder.Create(_vm, () => RunBusyOp(RunInOperationDialog(action)));
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified cancellable
    /// asyncronous operation with support for progress reporting.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Func<CancellationToken, IProgress<ProgressReport>, Task> action)
    {
        return ObservingCommandBuilder.Create(_vm, () => RunBusyOp(RunInOperationDialog(action)));
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified operation with
    /// support for progress reporting.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Action<IProgress<ProgressReport>> action)
    {
        return ObservingCommandBuilder.Create(_vm, () => RunBusyOp(RunInOperationDialog(action)));
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> that observes
    /// the <see cref="ViewModel"/> and executes the specified cancellable
    /// operation with support for progress reporting.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> that can be configured
    /// to customize the resulting <see cref="ObservingCommand"/> behavior.
    /// </returns>
    public ObservingCommandBuilder<TViewModel> BuildObserving(Action<CancellationToken, IProgress<ProgressReport>> action)
    {
        return ObservingCommandBuilder.Create(_vm, () => RunBusyOp(RunInOperationDialog(action)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildSimple(Action action)
    {
        return new(action);
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildSimple(Action<object?> action)
    {
        return new(action);
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified asyncronous operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildSimple(Func<Task> action)
    {
        return new(action);
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified asyncronous operation.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildSimple(Func<object?, Task> action)
    {
        return new(action);
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified cancellable async operation with progress report in a
    /// busy context.
    /// </summary>
    /// <param name="title">Title to use on the operation dialog.</param>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Func<CancellationToken, IProgress<ProgressReport>, Task> action, string? title = null)
    {
        return new(() => RunBusyOp(RunInOperationDialog(action, title)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified cancellable operation with progress report in a busy
    /// context.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <param name="title">Title to use on the operation dialog.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Action<CancellationToken, IProgress<ProgressReport>> action, string? title = null)
    {
        return new(() => RunBusyOp(RunInOperationDialog(action, title)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified async operation with progress report in a busy context.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <param name="title">Title to use on the operation dialog.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Func<IProgress<ProgressReport>, Task> action, string? title = null)
    {
        return new(() => RunBusyOp(RunInOperationDialog(action, title)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified operation with progress report in a busy context.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <param name="title">Title to use on the operation dialog.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Action<IProgress<ProgressReport>> action, string? title = null)
    {
        return new(() => RunBusyOp(RunInOperationDialog(action, title)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified async operation in a busy context.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Func<Task> action)
    {
        return new(() => RunBusyOp(action()));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will execute
    /// the specified operation in a busy context.
    /// </summary>
    /// <param name="action">Operation to execute.</param>
    /// <returns>A new <see cref="SimpleCommand"/> that will execute the
    /// specified operation.
    /// </returns>
    public SimpleCommand BuildBusyOperation(Action action)
    {
        return new(() => RunBusyOp(Task.Run(action)));
    }

    /// <summary>
    /// Creates a new <see cref="SimpleCommand"/> instance that will navigate
    /// to the specified <see cref="ViewModel"/> type upon execution.
    /// </summary>
    /// <typeparam name="T">
    /// Type of <see cref="ViewModel"/> to navigate to.
    /// </typeparam>
    /// <returns>A new <see cref="SimpleCommand"/> that will navigate to the
    /// specified <see cref="ViewModel"/>.
    /// </returns>
    public SimpleCommand BuildNavigate<T>() where T: ViewModel, new()
    {
        return new(() => _vm.NavigationService?.Navigate<T>());
    }

    private Task RunInOperationDialog(Func<IProgress<ProgressReport>, Task> action, string? title = null)
    {
        return _vm.DialogService?.RunOperation(title, action) ?? action(new Progress<ProgressReport>());
    }

    private Task RunInOperationDialog(Func<CancellationToken, IProgress<ProgressReport>, Task> action, string? title = null)
    {
        return _vm.DialogService?.RunOperation(title, action) ?? action(new CancellationToken(), new Progress<ProgressReport>());
    }

    private Task RunInOperationDialog(Action<IProgress<ProgressReport>> action, string? title = null)
    {
        return _vm.DialogService?.RunOperation(title, action) ?? Task.Run(() => action(new Progress<ProgressReport>()));
    }

    private Task RunInOperationDialog(Action<CancellationToken, IProgress<ProgressReport>> action, string? title = null)
    {
        return _vm.DialogService?.RunOperation(title, action) ?? Task.Run(() => action(new CancellationToken(), new Progress<ProgressReport>()));
    }

    private async Task RunBusyOp(Task callback)
    {
        _vm.IsBusy = true;
        try 
        {
            await callback;
        }
        finally
        {
            _vm.IsBusy = false;
        }
    }
}
