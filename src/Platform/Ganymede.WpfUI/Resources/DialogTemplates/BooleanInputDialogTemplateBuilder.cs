using System.Windows.Controls.Primitives;
using Wpf.Ui.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class BooleanInputDialogTemplateBuilder : ValueDialogTemplateBuilder<bool, ToggleSwitch>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => ToggleButton.IsCheckedProperty;
}