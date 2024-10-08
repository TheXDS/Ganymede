using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for numerical <see langword="struct"/> ranges
/// of type <typeparam name="T"/>.
/// </summary>
public class NumericRangeInputDialogTemplateBuilder<T> : IDialogTemplateBuilder<RangeInputDialogViewModel<T>>
    where T : struct, IComparable<T>
{
    /// <inheritdoc/>
    public StyledElement Build(RangeInputDialogViewModel<T> viewModel)
    {
        return new StackPanel
        {
            Children =
            {
                new NumericUpDown
                {
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.RangeStart)),
                },
                new NumericUpDown
                {
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.RangeEnd)),
                }
            }
        };
    }
}