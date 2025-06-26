using Wpf.Ui.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for value ranges of type
/// <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">
/// Type of value to generate a set of range controls for.
/// </typeparam>
public class NumericRangeInputDialogTemplateBuilder<TValue> : ValueRangeDialogTemplateBuilder<TValue, NumberBox>
    where TValue : unmanaged, IComparable<TValue>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => NumberBox.MaximumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => NumberBox.MinimumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => NumberBox.ValueProperty;
}