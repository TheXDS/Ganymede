using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for text entry.
/// </summary>
public class TextInputDialogTemplateBuilder : IDialogTemplateBuilder<TextInputDialogViewModel>
{
    /// <inheritdoc/>
    public StyledElement Build(TextInputDialogViewModel viewModel) => new TextBox
    {
        [!TextBox.MaxLengthProperty] = new Binding(nameof(viewModel.MaxLength)),
        [!TextBox.TextProperty] = new Binding(nameof(viewModel.Value)),
    };
}