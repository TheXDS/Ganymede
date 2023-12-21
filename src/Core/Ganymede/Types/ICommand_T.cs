using System.Windows.Input;

namespace TheXDS.Ganymede.Types;

/// <summary>
/// Defines a command that strongly specifies the parameter type to use.
/// </summary>
/// <typeparam name="T">
/// Type of parameter to use in the command call. The command parameter will be
/// attempted to be cast to this type.
/// </typeparam>
public interface ICommand<T> : ICommand
{
    bool ICommand.CanExecute(object? parameter) => parameter is T value && CanExecute(value);

    void ICommand.Execute(object? parameter) => Execute(parameter is T value ? value : default);
}