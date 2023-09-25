using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class BooleanInputDialogTemplateBuilder : ValueDialogTemplateBuilder<bool, CheckBox>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => ToggleButton.IsCheckedProperty;
}