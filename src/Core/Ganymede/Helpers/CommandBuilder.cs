using System.Windows.Input;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Contains helper methods to reduce boilerplate syntax when creating
/// <see cref="CommandBuilder{TViewModel}"/> instances.
/// </summary>
public static class CommandBuilder
{
    /// <summary>
    /// Creates a new <see cref="CommandBuilder{TViewModel}"/> for the
    /// specified ViewModel.
    /// </summary>
    /// <typeparam name="T">
    /// Type of ViewModel to create a command builder for.
    /// </typeparam>
    /// <param name="viewModel">
    /// ViewModel to create a command builder for. Usually,
    /// <c><see langword="this"/></c> (<c><see langword="Me"/></c> in Visual
    /// Basic) is passed.
    /// </param>
    /// <returns>
    /// A new <see cref="CommandBuilder{TViewModel}"/> that can be used to
    /// create commands bound to the specified ViewModel.
    /// </returns>
    /// <remarks>
    /// This method is intended to be used in the constructor or
    /// initizalization code for a ViewModel to help configure new
    /// <see cref="ICommand"/> objects.
    /// </remarks>
    public static CommandBuilder<T> For<T>(T viewModel) where T : IViewModel
    {
        return new CommandBuilder<T>(viewModel);
    }
}
