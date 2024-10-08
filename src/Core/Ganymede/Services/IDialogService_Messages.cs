using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that can provide of
/// dialog services to interact with the user.
/// </summary>
public partial interface IDialogService
{
    /// <summary>
    /// Displays a simple message dialog from a pre-configured visual template.
    /// </summary>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    Task Show(DialogTemplate template);

    /// <summary>
    /// Displays a dialog from a pre-configured visual template.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IDialogViewModel"/> to be displayed.
    /// </typeparam>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    Task Show<TViewModel>(DialogTemplate template) where TViewModel : IAwaitableDialogViewModel, new();

    /// <summary>
    /// Navigates to a user-defined <see cref="DialogViewModel"/> under the
    /// dialog navigation system.
    /// </summary>
    /// <param name="dialogVm">Dialog ViewModel to navigate to. It must
    /// implement <see cref="IAwaitableDialogViewModel"/> to be able to notify
    /// of its own completion.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task Show(IAwaitableDialogViewModel dialogVm);

    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Message(string message);

    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Message(string? title, string message);

    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Warning(string message);

    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Warning(string? title, string message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(string message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="exception">
    /// <see cref="Exception"/> with the information about the error.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(Exception exception);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Text to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(string? title, string message);
}
