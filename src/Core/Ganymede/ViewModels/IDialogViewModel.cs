using System.Drawing;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that exposes dialog
/// functionality.
/// </summary>
public interface IDialogViewModel : IViewModel
{
    /// <summary>
    /// Gets or sets the icon to be displayed on the dialog.
    /// </summary>
    string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a brush to be used to draw the background for the icon of
    /// the dialog if displayed.
    /// </summary>
    Color? IconBgColor { get; set; }

    /// <summary>
    /// Gets a collection of interactions to be displayed in the dialog.
    /// </summary>
    ICollection<ButtonInteraction> Interactions { get; }

    /// <summary>
    /// Gets a value that indicates if the icon should be made visible in the
    /// dialog.
    /// </summary>
    bool IsIconVisible => !string.IsNullOrWhiteSpace(Icon);

    /// <summary>
    /// Gets a value that indicates if the title should be made visible.
    /// </summary>
    bool IsTitleVisible => !string.IsNullOrWhiteSpace(Title);

    /// <summary>
    /// Gets or sets a message to be displayed on the dialog.
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// Gets or sets this dialog title.
    /// </summary>
    string? Title { get; set; }
}