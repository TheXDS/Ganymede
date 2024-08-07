﻿using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;

namespace TheXDS.Ganymede.Types.Extensions;

/// <summary>
/// Includes extensions that help reduce boilerplate when creating commands
/// for various <see cref="IDialogViewModel"/> implementations.
/// </summary>
public static class CommandBuilderExtensions
{
    /// <summary>
    /// Builds a simple command that will set the dialog result to
    /// <see langword="true"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IAwaitableDialogViewModel{T}"/> to build the command
    /// for.
    /// </typeparam>
    /// <param name="cb">
    /// Command builder to use when creating the command.
    /// </param>
    /// <returns>
    /// A new command that will set the dialog result to <see langword="true"/>
    /// and close the dialog.
    /// </returns>
    public static ICommand BuildOkCommand<TViewModel>(this CommandBuilder<TViewModel> cb) where TViewModel : IAwaitableDialogViewModel<bool>
    {
        return cb.BuildResultCommand(true);
    }

    /// <summary>
    /// Builds a command that can observe changes on the ViewModel and, upon
    /// execution, will set the dialog result to <see langword="true"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IAwaitableDialogViewModel{T}"/> to build the command
    /// for.
    /// </typeparam>
    /// <param name="cb">
    /// Command builder to use when creating the command.
    /// </param>
    /// <param name="configCallback">
    /// Callback used to configure the observing command.
    /// </param>
    /// <returns>
    /// A new command that will set the dialog result to <see langword="true"/>
    /// and close the dialog.
    /// </returns>
    public static ICommand BuildOkCommand<TViewModel>(this CommandBuilder<TViewModel> cb, Action<ObservingCommandBuilder<TViewModel>> configCallback) where TViewModel : IAwaitableDialogViewModel<bool>
    {
        var cmd = cb.BuildObserving(() => cb.ViewModelReference.CloseDialog(true));
        configCallback.Invoke(cmd);
        return cmd.Build();
    }

    /// <summary>
    /// Builds a command that can observe changes on the ViewModel and, upon
    /// execution, will set the dialog result to <see langword="true"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IAwaitableDialogViewModel{T}"/> to build the command
    /// for.
    /// </typeparam>
    /// <param name="cb">
    /// Command builder to use when creating the command.
    /// </param>
    /// <param name="configCallback">
    /// Callback used to configure the observing command.
    /// </param>
    /// <returns>
    /// A new command that will set the dialog result to <see langword="true"/>
    /// and close the dialog.
    /// </returns>
    public static ICommand BuildOkCommand<TViewModel>(this CommandBuilder<TViewModel> cb, Func<ObservingCommandBuilder<TViewModel>, ObservingCommandBuilder<TViewModel>> configCallback) where TViewModel : IAwaitableDialogViewModel<bool>
    {
        var cmd = cb.BuildObserving(() => cb.ViewModelReference.CloseDialog(true));
        return configCallback.Invoke(cmd).Build();
    }

    /// <summary>
    /// Builds a simple command that will set the dialog result to
    /// <see langword="false"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IAwaitableDialogViewModel{T}"/> to build the command
    /// for.
    /// </typeparam>
    /// <param name="cb">
    /// Command builder to use when creating the command.
    /// </param>
    /// <returns>
    /// A new command that will set the dialog result to
    /// <see langword="false"/> and close the dialog.
    /// </returns>
    public static ICommand BuildCancelCommand<TViewModel>(this CommandBuilder<TViewModel> cb) where TViewModel : IAwaitableDialogViewModel<bool>
    {
        return cb.BuildResultCommand(false);
    }

    /// <summary>
    /// Builds a simple command that will set the dialog result to
    /// the provided value.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IAwaitableDialogViewModel{T}"/> to build the command
    /// for.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to be returned by the dialog.
    /// </typeparam>
    /// <param name="cb">
    /// Command builder to use when creating the command.
    /// </param>
    /// <param name="result">Result to be set for the dialog.</param>
    /// <returns>
    /// A new command that will set the dialog result to
    /// <paramref name="result"/> and close the dialog.
    /// </returns>
    public static ICommand BuildResultCommand<TViewModel, TValue>(this CommandBuilder<TViewModel> cb, TValue result) where TViewModel : IAwaitableDialogViewModel<TValue>
    {
        return cb.BuildSimple(() => cb.ViewModelReference.CloseDialog(result));
    }
}