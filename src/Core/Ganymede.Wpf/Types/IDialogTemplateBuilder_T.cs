using System.Windows;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Types;

/// <summary>
/// Defines a set of members to be implemented by a type that provides template
/// generation for dialog boxes that can interact with a
/// <see cref="DialogViewModel"/> of a specific type.
/// </summary>
/// <typeparam name="T">
/// Type of <see cref="DialogViewModel"/> to generate controls for.
/// </typeparam>
public interface IDialogTemplateBuilder<in T> : IDialogTemplateBuilder where T : DialogViewModel
{
    /// <summary>
    /// Generates the controls to include in the dialog view to allow the user
    /// to interact with the <see cref="DialogViewModel"/>.
    /// </summary>
    /// <param name="viewModel">
    /// ViewModel instance for which to generate controls.
    /// </param>
    /// <returns>
    /// A <see cref="FrameworkElement"/> that allows for interactivity with the
    /// specified <see cref="DialogViewModel"/>.
    /// </returns>
    FrameworkElement? Build(T viewModel);

    bool IDialogTemplateBuilder.CanBuild(DialogViewModel vm) => vm is T;

    FrameworkElement? IDialogTemplateBuilder.Build(DialogViewModel vm) => Build((T)vm);
}

