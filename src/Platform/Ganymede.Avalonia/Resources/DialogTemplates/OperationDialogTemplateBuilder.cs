using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for dialogs that display operation status.
/// </summary>
public class OperationDialogTemplateBuilder : IDialogTemplateBuilder<OperationDialogViewModel>
{
    /// <inheritdoc/>
    public StyledElement Build(OperationDialogViewModel viewModel)
    {
        return new ProgressBar
        {
            Margin = new(0, 20, 0, 10),
            [!ProgressBar.IsIndeterminateProperty] = new Binding(nameof(viewModel.IsIndeterminate)),
            [!RangeBase.ValueProperty] = new Binding(nameof(viewModel.Progress))
        };
    }
}