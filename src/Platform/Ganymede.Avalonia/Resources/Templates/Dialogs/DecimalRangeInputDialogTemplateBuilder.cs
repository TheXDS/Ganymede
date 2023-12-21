using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class DecimalRangeInputDialogTemplateBuilder : IDialogTemplateBuilder<RangeInputDialogViewModel<decimal>>
{
    public virtual Control Build(RangeInputDialogViewModel<decimal> viewModel)
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
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.MinimumValue)),
                },
                new NumericUpDown
                {
                    FormatString = "{0:0.00}",
                    [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.MaximumValue)),
                }
            }
        };
    }
}