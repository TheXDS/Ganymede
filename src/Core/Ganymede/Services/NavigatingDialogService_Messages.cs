using System.Drawing;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <summary>
    /// Displays a simple message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Message(string? title, string message) => SimpleMessage("i", Color.SteelBlue, title, message);
    
    /// <summary>
    /// Displays a warning to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Warning(string? title, string message) => SimpleMessage("⚠", Color.Orange, title, message);
    
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to display.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    public Task Error(string? title, string message) => SimpleMessage("❌", Color.DarkRed, title, message);
}
