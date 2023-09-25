using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Base class for all Template builders that generate simple value controls.
/// </summary>
/// <typeparam name="TValue">
/// Type of value to generate controls for.
/// </typeparam>
/// <typeparam name="TControl">
/// Type of control to generate.
/// </typeparam>
public abstract class ValueDialogTemplateBuilder<TValue, TControl>
    : IDialogTemplateBuilder<InputDialogViewModel<TValue>> 
    where TValue : struct, IComparable<TValue>
    where TControl : Control, new()
{
    /// <summary>
    /// Gets a reference to the dependency property that holds the 
    /// <typeparamref name="TControl"/>'s value.
    /// </summary>
    /// <returns>
    /// The dependency property that holds the
    /// <typeparamref name="TControl"/>'s value.
    /// </returns>
    protected abstract DependencyProperty GetValueProperty();

    /// <inheritdoc/>
    public FrameworkElement Build(InputDialogViewModel<TValue> viewModel)
    {
        var control = new TControl();
        control.SetBinding(GetValueProperty(), new Binding(nameof(viewModel.Value)));
        return control;
    }
}
