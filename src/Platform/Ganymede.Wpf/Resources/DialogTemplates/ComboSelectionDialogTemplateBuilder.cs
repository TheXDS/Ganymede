using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate an item selection
/// Combo box.
/// </summary>
public class ComboSelectionDialogTemplateBuilder : IDialogTemplateBuilder<SelectionDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement? Build(SelectionDialogViewModel viewModel)
    {
        var control = new ComboBox();
        control.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(nameof(viewModel.Options)));
        control.SetBinding(Selector.SelectedItemProperty, new Binding(nameof(viewModel.Value)));
        return control;
    }
}