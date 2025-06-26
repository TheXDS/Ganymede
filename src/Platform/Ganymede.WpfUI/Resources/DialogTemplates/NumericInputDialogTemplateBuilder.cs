using Wpf.Ui.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that supports numeric values through a
/// numeric input control.
/// </summary>
/// <typeparam name="TValue">Type of numeric value.</typeparam>
public class NumericInputDialogTemplateBuilder<TValue> : ComparableValueDialogTemplateBuilder<TValue, NumberBox>
    where TValue : unmanaged, IComparable<TValue>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => NumberBox.MaximumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => NumberBox.MinimumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => NumberBox.ValueProperty;
}
