using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class DateTimeInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<DateTime>>
{
    public Control Build(InputDialogViewModel<DateTime> viewModel)
    {
        return new CalendarDatePicker
        {
            [!CalendarDatePicker.SelectedDateProperty] = new Binding(nameof(viewModel.Value)),
        };
    }
}