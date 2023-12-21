using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class BooleanInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<bool>>
{
    public Control Build(InputDialogViewModel<bool> viewModel)
    {
        return new ToggleSwitch
        {
            [!ToggleButton.IsCheckedProperty] = new Binding(nameof(viewModel.Value))
        };
    }
}