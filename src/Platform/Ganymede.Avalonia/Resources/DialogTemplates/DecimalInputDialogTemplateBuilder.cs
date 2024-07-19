using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="decimal"/> values.
/// </summary>
public class DecimalInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<decimal>>
{
    /// <inheritdoc/>
    public StyledElement Build(InputDialogViewModel<decimal> viewModel)
    {
        return new NumericUpDown
        {
            FormatString = "{0:0.00}",
            [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
            [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
            [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.Value)),
        };
    }
}