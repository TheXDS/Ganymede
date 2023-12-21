using System;
using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Base class for all Template builders that generate simple value controls.
/// </summary>
/// <typeparam name="TValue">
/// Type of value to generate controls for.
/// </typeparam>
/// <typeparam name="TControl">Type of control to generate.</typeparam>
public abstract class ComparableValueDialogTemplateBuilder<TValue, TControl>
    : ValueDialogTemplateBuilderBase<InputDialogViewModel<TValue>, TValue, TControl>
    where TValue : struct, IComparable<TValue>
    where TControl : Control, new()
{
    /// <inheritdoc/>
    public override FrameworkElement Build(InputDialogViewModel<TValue> viewModel)
    {
        return NewControl(GetValueProperty(), nameof(InputDialogViewModel<TValue>.Value));
    }
}
