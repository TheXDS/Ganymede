using System.Windows;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Types;

/// <summary>
/// Defines a set of members to be implemented by a type that provides template
/// generation for dialog boxes that can interact with a
/// <see cref="DialogViewModel"/> of a specific type.
/// </summary>
public interface IDialogTemplateBuilder
{
    /// <summary>
    /// Gets a value that indicates if this instance can create a control for
    /// the specified <see cref="IDialogViewModel"/>.
    /// </summary>
    /// <param name="viewModel">ViewModel to check against.</param>
    /// <returns>
    /// <see langword="true"/> if this instance can generate controls to
    /// interact with the specified <see cref="IDialogViewModel"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    bool CanBuild(IDialogViewModel viewModel);

    /// <summary>
    /// Generates the controls to include in the dialog view to allow the user
    /// to interact with the <see cref="IDialogViewModel"/>.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel instance for which to generate controls.
    /// </param>
    /// <returns>
    /// A <see cref="FrameworkElement"/> that allows for interactivity with the
    /// specified <see cref="IDialogViewModel"/>.
    /// </returns>
    FrameworkElement? Build(IDialogViewModel viewModel);
}
