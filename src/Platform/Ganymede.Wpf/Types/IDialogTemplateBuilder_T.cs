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
public interface IDialogTemplateBuilder<in T> : IDialogTemplateBuilder where T : IDialogViewModel
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

    /// <summary>
    /// Gets a value that indicates if this dialog template builder can be used
    /// for generating the dialog's UI for the specified
    /// <see cref="IDialogViewModel"/>.
    /// </summary>
    /// <param name="vm">
    /// <see cref="IDialogViewModel"/> instance to verify.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this template builder can be used to generate
    /// the dialog's UI for the specified <see cref="IDialogViewModel"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    /// <remarks>
    /// The default interface implementation will just check that the ViewModel
    /// type matches <typeparamref name="T"/>. Implement this method directly
    /// to customize the logic based on your requirements.
    /// </remarks>
    bool IDialogTemplateBuilder.CanBuild(IDialogViewModel vm) => vm is T;

    /// <summary>
    /// Builds the dialog's UI for the specified
    /// <see cref="IDialogViewModel"/>.
    /// </summary>
    /// <param name="vm">
    /// <see cref="IDialogViewModel"/> to build the dialog UI for.
    /// </param>
    /// <returns>
    /// A <see cref="FrameworkElement"/> that defines the dialog UI for the
    /// specified <see cref="IDialogViewModel"/>, or <see langword="null"/> if
    /// no UI was generated.
    /// </returns>
    FrameworkElement? IDialogTemplateBuilder.Build(IDialogViewModel vm) => Build((T)vm);
}
