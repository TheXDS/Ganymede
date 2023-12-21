using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class TextInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel>
{
    public Control Build(InputDialogViewModel viewModel)
    {
        return new TextBox
        {
            [!TextBox.MaxLengthProperty] = new Binding(nameof(viewModel.MaxLength)),
            [!TextBox.TextProperty] = new Binding(nameof(viewModel.Value)),
        };
    }
}