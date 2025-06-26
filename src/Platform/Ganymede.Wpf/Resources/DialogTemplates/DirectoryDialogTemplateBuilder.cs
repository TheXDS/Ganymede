using System.Windows.Data;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template that creates a control that allows the user to
/// select a directory on the filesystem.
/// </summary>
public abstract class DirectoryDialogTemplateBuilder : IDialogTemplateBuilder<DirectoryDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement? Build(DirectoryDialogViewModel viewModel)
    {
        var txt = new DirectorySelectorTextBox();
        txt.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.Value)) { Mode = BindingMode.TwoWay });
        return txt;
    }
}