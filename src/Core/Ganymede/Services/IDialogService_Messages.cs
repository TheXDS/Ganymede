using TheXDS.MCART.Resources.Strings;
using static TheXDS.MCART.Resources.Strings.Composition;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that can provide of
/// dialog services to interact with the user.
/// </summary>
public partial interface IDialogService
{
    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Message(string message) => Message(null, message);

    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Message(string? title, string message);

    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Warning(string message) => Warning(null, message);

    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Warning(string? title, string message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(string message) => Error(null, message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="exception">
    /// <see cref="Exception"/> with the information about the error.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(Exception exception) => Error(St.Error, ExDump(exception, exDumpOptions));

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(string? title, string message);

#if DEBUG
    private const ExDumpOptions exDumpOptions = ExDumpOptions.All;
#else
    private const ExDumpOptions exDumpOptions = ExDumpOptions.Message;
#endif
}
