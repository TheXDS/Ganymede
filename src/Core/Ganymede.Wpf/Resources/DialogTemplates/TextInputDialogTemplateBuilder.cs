using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate text input controls.
/// </summary>
public class TextInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement Build(InputDialogViewModel viewModel)
    {
        var control = new TextBox();
        control.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.Value)));
        control.SetBinding(TextBox.MaxLengthProperty, new Binding(nameof(viewModel.MaxLength)));
        return control;
    }
}