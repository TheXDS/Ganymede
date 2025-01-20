using System.Drawing;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Resources;

/// <summary>
/// Includes a collection of pre-defined dialog templates.
/// </summary>
public static class CommonDialogTemplates
{
    /// <summary>
    /// Gets a template for simple message dialogs.
    /// </summary>
    public static readonly DialogTemplate Message = new()
    {
        Title = Common.Message,
        Color = Color.SteelBlue,
        Icon = "i"
    };

    /// <summary>
    /// Gets a template for a message dialog that indicates success.
    /// </summary>
    public static readonly DialogTemplate Success = Message with
    {
        Icon = "✔️",
        Color = Color.DarkGreen,
    };

    /// <summary>
    /// Gets a template for warning messages.
    /// </summary>
    public static readonly DialogTemplate Warning = Message with
    {
        Title = Common.Warning,
        Color = Color.Orange,
        Icon = "⚠"
    };

    /// <summary>
    /// Gets a template for error messages.
    /// </summary>
    /// <remarks>
    /// Simple error messages are meant to be displayed when a single operation
    /// cannot be completed successfully, but it does not compromise the
    /// ability for the application to keep running normally.
    /// </remarks>
    public static readonly DialogTemplate Error = Message with
    {
        Title = Common.Error,
        Color = Color.DarkRed,
        Icon = "❌"
    };

    /// <summary>
    /// Gets a template for critical error messages.
    /// </summary>
    /// <remarks>
    /// Critical error messages are meant to be displayed when a severe error
    /// occurs that may compromise the ability for the application to run
    /// reliably, while not closing it immediately and maybe giving a chance of
    /// recovery or some other preparations before forcefully closing the app.
    /// </remarks>
    public static readonly DialogTemplate Critical = Error with
    {
        Icon = "💣"
    };

    /// <summary>
    /// Gets a template for fatal error messages.
    /// </summary>
    /// <remarks>
    /// Fatal error messages are meant to be displayed when an error occurs
    /// where it forces the application to forcefully quit. These are
    /// non-recoverable, and result in the application to immediately stop
    /// execution upon being thrown.
    /// </remarks>
    public static readonly DialogTemplate Fatal = Error with
    {
        Icon = "💀"
    };

    /// <summary>
    /// Gets a template for question dialogs.
    /// </summary>
    public static readonly DialogTemplate Question = new()
    {
        Color = Color.DarkGreen,
        Icon = "?"
    };

    /// <summary>
    /// Gets a template for dialogs that request some form of input from the
    /// user.
    /// </summary>
    public static readonly DialogTemplate Input = new()
    {
        Color = Color.DarkGray,
        Icon = "✍",
    };

    /// <summary>
    /// Gets a template for login dialogs.
    /// </summary>
    public static readonly DialogTemplate Login = new()
    {
        Icon = "👤",
        Color = Color.MediumAquamarine,
    };

    /// <summary>
    /// Gets a template for File open dialogs.
    /// </summary>
    public static readonly DialogTemplate FileOpen = new()
    {
        Icon = "📂",
        Color = Color.DarkSeaGreen,
        Title = Common.Open
    };

    /// <summary>
    /// Gets a template for File save dialogs.
    /// </summary>
    public static readonly DialogTemplate FileSave = new()
    {
        Icon = "💾",
        Color = Color.DarkSeaGreen,
        Title = Common.Save
    };

    /// <summary>
    /// Gets a template for Directory select dialogs.
    /// </summary>
    public static readonly DialogTemplate DirectorySelect = new()
    {
        Icon = "📁",
        Color = Color.PaleGoldenrod
    };
}
