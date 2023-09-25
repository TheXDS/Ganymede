using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for ranges of value of type
/// <typeparamref name="T"/>.
/// </summary>
public class NumericRangeInputDialogTemplateBuilder<T> : ValueRangeDialogTemplateBuilder<T, TextBox>
    where T : struct, IComparable<T>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => TextBox.TextProperty;
}