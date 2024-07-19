using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="DateTime"/> ranges.
/// </summary>
public class DateTimeRangeInputDialogTemplateBuilder : IDialogTemplateBuilder<RangeInputDialogViewModel<DateTime>>
{
    /// <inheritdoc/>
    public StyledElement? Build(RangeInputDialogViewModel<DateTime> viewModel)
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