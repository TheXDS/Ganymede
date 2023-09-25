using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class NumericRangeInputDialogTemplateBuilder<T> : IDialogTemplateBuilder<RangeInputDialogViewModel<T>>
    where T : struct, IComparable<T>
{
    public virtual Control Build(RangeInputDialogViewModel<T> viewModel)
    {
        return new StackPanel
        {
            Children =
            {
                new NumericUpDown
                {
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.MinimumValue)),
                },
                new NumericUpDown
                {
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.MaximumValue)),
                }
            }
        };
    }
}