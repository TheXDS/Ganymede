using Avalonia.Media;
using TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Services;

public partial class DialogService
{
    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Message(string message) => Message(Common.Message, message);
    
    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Message(string? title, string message) => SimpleMessage("i", Brushes.SteelBlue, title, message);
    
    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Warning(string message) => Warning(Common.Warning, message);
    
    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Warning(string? title, string message) => SimpleMessage("⚠", Brushes.Orange, title, message);
    
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Error(string message) => Error(Common.Error, message);
    
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Error(string? title, string message) => SimpleMessage("❌", Brushes.DarkRed, title, message);
}