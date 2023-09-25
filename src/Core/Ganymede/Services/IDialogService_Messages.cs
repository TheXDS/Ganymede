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
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task Error(string? title, string message);
}
