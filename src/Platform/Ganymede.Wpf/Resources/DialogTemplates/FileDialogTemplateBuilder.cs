using System.Windows.Data;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template that creates a control that allows the user to
/// select a file path.
/// </summary>
/// <typeparam name="T">
/// Type of ViewModel to use for file path selection.
/// </typeparam>
public abstract class FileDialogTemplateBuilder<T> : IDialogTemplateBuilder<T> where T : FileDialogViewModel
{
    /// <inheritdoc/>
    public virtual FrameworkElement? Build(T viewModel)
    {
        var txt = new FileOpenSelectorTextBox();
        txt.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.Value)) { Mode = BindingMode.TwoWay });
        txt.SetBinding(FileOpenSelectorTextBox.FileFiltersProperty, new Binding(nameof(viewModel.FileFilters)) { Mode = BindingMode.OneWay });
        return txt;
    }
}
