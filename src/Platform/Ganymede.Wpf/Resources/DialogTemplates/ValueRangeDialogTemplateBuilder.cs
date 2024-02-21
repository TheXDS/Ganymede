using System;
using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Base class for all Template builders that generate simple value controls.
/// </summary>
/// <typeparam name="TValue">
/// Type of value to generate a set of range controls for.
/// </typeparam>
/// <typeparam name="TControl">
/// Type of control to generate for both the minimum and maximum values.
/// </typeparam>
public abstract class ValueRangeDialogTemplateBuilder<TValue, TControl>
    : ValueDialogTemplateBuilderBase<RangeInputDialogViewModel<TValue>, TValue, TControl>
    where TValue : struct, IComparable<TValue>
    where TControl : Control, new()
{
    /// <inheritdoc/>
    public override FrameworkElement Build(RangeInputDialogViewModel<TValue> viewModel)
    {
        return new StackPanel()
        {
            Children =
            {
                NewControl(GetValueProperty(), nameof(viewModel.MinimumValue)),
                NewControl(GetValueProperty(), nameof(viewModel.MaximumValue), c => c.Margin = new Thickness(0, 10, 0, 0))
            }
        };
    }
}
