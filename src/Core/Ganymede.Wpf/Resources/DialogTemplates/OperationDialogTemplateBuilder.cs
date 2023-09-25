using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that generates the content of an operation dialog.
/// </summary>
public class OperationDialogTemplateBuilder : IDialogTemplateBuilder<OperationDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement Build(OperationDialogViewModel viewModel)
    {
        var control = new ProgressBar
        {
            Height = 4,
            Margin = new(0, 20, 0, 10),
        };
        control.SetBinding(ProgressBar.IsIndeterminateProperty, new Binding(nameof(viewModel.IsIndeterminate)));
        control.SetBinding(System.Windows.Controls.Primitives.RangeBase.ValueProperty, new Binding(nameof(viewModel.Progress)));
        return control;
    }
}