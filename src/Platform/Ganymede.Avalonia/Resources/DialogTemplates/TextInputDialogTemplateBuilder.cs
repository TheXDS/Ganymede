using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for text entry.
/// </summary>
public class TextInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel>
{
    /// <inheritdoc/>
    public StyledElement Build(InputDialogViewModel viewModel) => new TextBox
    {
        [!TextBox.MaxLengthProperty] = new Binding(nameof(viewModel.MaxLength)),
        [!TextBox.TextProperty] = new Binding(nameof(viewModel.Value)),
    };
}