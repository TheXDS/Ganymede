using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class DateTimeRangeInputDialogTemplateBuilder : IDialogTemplateBuilder<RangeInputDialogViewModel<DateTime>>
{
    public Control? Build(RangeInputDialogViewModel<DateTime> viewModel)
    {
        return new StackPanel()
        {
            Children =
            {
                new CalendarDatePicker
                {
                    [!CalendarDatePicker.DisplayDateStartProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!CalendarDatePicker.DisplayDateEndProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!CalendarDatePicker.SelectedDateProperty] = new Binding(nameof(viewModel.MinimumValue)),
                },
                new CalendarDatePicker
                {
                    [!CalendarDatePicker.DisplayDateStartProperty] = new Binding(nameof(viewModel.Minimum)),
                    [!CalendarDatePicker.DisplayDateEndProperty] = new Binding(nameof(viewModel.Maximum)),
                    [!CalendarDatePicker.SelectedDateProperty] = new Binding(nameof(viewModel.MaximumValue)),
                },
            }
        };
    }
}