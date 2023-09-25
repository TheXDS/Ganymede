using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that supports numeric values through a
/// numeric input control.
/// </summary>
/// <typeparam name="T">Type of numeric value.</typeparam>
public class NumericInputDialogTemplateBuilder<T> : ComparableValueDialogTemplateBuilder<T, TextBox>
    where T : struct, IComparable<T>
{

    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => TextBox.TextProperty;
}