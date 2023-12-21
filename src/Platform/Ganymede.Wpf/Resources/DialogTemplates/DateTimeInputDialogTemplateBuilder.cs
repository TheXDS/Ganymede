using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class DateTimeInputDialogTemplateBuilder : ComparableValueDialogTemplateBuilder<DateTime, DatePicker>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => throw new NotImplementedException();

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => DatePicker.SelectedDateProperty;
}