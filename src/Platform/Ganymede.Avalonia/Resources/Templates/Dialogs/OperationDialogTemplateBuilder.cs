using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

public class OperationDialogTemplateBuilder : IDialogTemplateBuilder<OperationDialogViewModel>
{
    public Control Build(OperationDialogViewModel viewModel)
    {
        return new ProgressBar
        {
            Margin = new(0, 20, 0, 10),
            [!ProgressBar.IsIndeterminateProperty] = new Binding(nameof(viewModel.IsIndeterminate)),
            [!RangeBase.ValueProperty] = new Binding(nameof(viewModel.Progress))
        };
    }
}