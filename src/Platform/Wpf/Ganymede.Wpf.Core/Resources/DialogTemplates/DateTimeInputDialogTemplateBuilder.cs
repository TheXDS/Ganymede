namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class DateTimeInputDialogTemplateBuilder : ComparableValueDialogTemplateBuilder<DateTime, DatePicker>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => DatePicker.DisplayDateEndProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => DatePicker.DisplayDateStartProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => DatePicker.SelectedDateProperty;
}