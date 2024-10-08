using System.Drawing;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Models;

/// <summary>
/// Represents a simple dialog template to be displayed by Ganymede.
/// </summary>
/// <remarks>
/// This struct will include information about the decorations, interactions
/// and message for the dialogs. These values may still be defined on a custom
/// <see cref="IDialogViewModel"/> to be used as the dialog content.
/// </remarks>
public readonly record struct DialogTemplate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DialogTemplate"/> struct.
    /// </summary>
    public DialogTemplate()
    {
        Text = string.Empty;
        Color = Color.Gray;
    }

    /// <summary>
    /// Gets or initializes the title for this dialog template.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets or initializes the message text for this dialog template.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Gets or initializes an optional icon glyph for this dialog template.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    /// Gets or initializes an optional background color to be displayed for
    /// the icon for this dialog template.
    /// </summary>
    public Color Color { get; init; }

    /// <summary>
    /// Configures a <see cref="IDialogViewModel"/> using the properties from
    /// this instance.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to configure.
    /// </typeparam>
    /// <param name="viewModel">ViewModel to be configured.</param>
    /// <returns>The same instance as <paramref name="viewModel"/>.</returns>
    public TViewModel Configure<TViewModel>(TViewModel viewModel) where TViewModel: IDialogViewModel
    {
        viewModel.Title = Title;
        viewModel.Message = Text;
        viewModel.Icon = Icon;
        viewModel.IconBgColor = Color;
        return viewModel;
    }
}
