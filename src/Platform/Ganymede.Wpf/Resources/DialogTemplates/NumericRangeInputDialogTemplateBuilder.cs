using System;
using System.Windows;
using TheXDS.Ganymede.Controls.Primitives;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for value ranges of type
/// <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">
/// Type of value to generate a set of range controls for.
/// </typeparam>
/// <typeparam name="TControl">
/// Type of control to generate for both the minimum and maximum values.
/// </typeparam>
public class NumericRangeInputDialogTemplateBuilder<TValue, TControl> : ValueRangeDialogTemplateBuilder<TValue, TControl>
    where TValue : unmanaged, IComparable<TValue>
    where TControl : NumericInputControl<TValue>, new()
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => NumericInputControl<TValue>.MaximumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => NumericInputControl<TValue>.MinimumProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => NumericInputControl<TValue>.ValueProperty;
}