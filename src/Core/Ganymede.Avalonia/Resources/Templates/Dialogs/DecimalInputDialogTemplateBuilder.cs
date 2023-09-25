using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class DecimalInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<decimal>>
{
    public Control Build(InputDialogViewModel<decimal> viewModel)
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