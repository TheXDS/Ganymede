using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="decimal"/> ranges.
/// </summary>
public class DecimalRangeInputDialogTemplateBuilder : IDialogTemplateBuilder<RangeInputDialogViewModel<decimal>>
{
    /// <inheritdoc/>
    public virtual StyledElement Build(RangeInputDialogViewModel<decimal> viewModel)
    {
        return new StackPanel
        {
            Children =
            {
                new NumericUpDown
                {
                    FormatString = "{0:0.00}",
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.RangeStart)),
                },
                new NumericUpDown
                {
                    FormatString = "{0:0.00}",
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.RangeEnd)),
                }
            }
        };
    }
}