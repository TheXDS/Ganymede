using System;
using System.Windows;
using TheXDS.Ganymede.Controls.Primitives;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that supports numeric values through a
/// numeric input control.
/// </summary>
/// <typeparam name="TValue">Type of numeric value.</typeparam>
/// <typeparam name="TControl">Type of control to bind to.</typeparam>
public class NumericInputDialogTemplateBuilder<TValue, TControl> : ComparableValueDialogTemplateBuilder<TValue, TControl>
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
