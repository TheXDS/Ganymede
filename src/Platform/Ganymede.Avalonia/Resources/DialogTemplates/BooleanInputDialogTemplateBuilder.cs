using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class BooleanInputDialogTemplateBuilder : IDialogTemplateBuilder<InputDialogViewModel<bool>>
{
    /// <inheritdoc/>
    public StyledElement? Build(InputDialogViewModel<bool> viewModel) => new ToggleSwitch
    {
        [!ToggleButton.IsCheckedProperty] = new Binding(nameof(viewModel.Value))
    };
}