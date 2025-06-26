namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for ranges of <see cref="DateTime"/>.
/// </summary>
public class DateTimeRangeInputDialogTemplateBuilder : ValueRangeDialogTemplateBuilder<DateTime, DatePicker>
{
    /// <inheritdoc/>
    protected override DependencyProperty GetValueProperty() => DatePicker.SelectedDateProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMaxProperty() => DatePicker.DisplayDateEndProperty;

    /// <inheritdoc/>
    protected override DependencyProperty GetMinProperty() => DatePicker.DisplayDateStartProperty;
}