using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ValueConverters;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template that creates a control that allows the user to
/// select multiple file paths.
/// </summary>
public class FileMultiSelectDialogTemplateBuilder : IDialogTemplateBuilder<FileMultiSelectDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement? Build(FileMultiSelectDialogViewModel viewModel)
    {
        var txt = new FileOpenSelectorTextBox() { AllowMultiSelect = true };
        txt.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.Value)) { Mode = BindingMode.TwoWay, Converter = new StringArrayToStringConverter() });
        txt.SetBinding(FileOpenSelectorTextBox.FileFiltersProperty, new Binding(nameof(viewModel.FileFilters)) { Mode = BindingMode.OneWay });
        return txt;
    }
}