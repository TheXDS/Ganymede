using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="DateTime"/> values.
/// </summary>
public class DateTimeInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<DateTime>>
{
    /// <inheritdoc/>
    public StyledElement Build(InputDialogViewModel<DateTime> viewModel)
    {
        return new CalendarDatePicker
        {
            [!CalendarDatePicker.SelectedDateProperty] = new Binding(nameof(viewModel.Value)),
        };
    }
}