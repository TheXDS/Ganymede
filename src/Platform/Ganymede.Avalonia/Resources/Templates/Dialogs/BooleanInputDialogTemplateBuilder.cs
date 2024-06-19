using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Component.Templating;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Resources.Templates.Dialogs;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class BooleanInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<bool>>
{
    /// <inheritdoc/>
    public Control Build(InputDialogViewModel<bool> viewModel)
    {
        return new ToggleSwitch
        {
            [!ToggleButton.IsCheckedProperty] = new Binding(nameof(viewModel.Value))
        };
    }
}