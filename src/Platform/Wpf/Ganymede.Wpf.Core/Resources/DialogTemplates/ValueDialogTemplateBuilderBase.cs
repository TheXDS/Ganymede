﻿using System.Numerics;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Exceptions;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Base class for all Template builders that generate simple value controls.
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TControl"></typeparam>
public abstract class ValueDialogTemplateBuilderBase<TViewModel, TValue, TControl>
    : IDialogTemplateBuilder<TViewModel>
    where TViewModel : IInputDialogViewModel<TValue>
    where TValue : struct, IComparable<TValue>
    where TControl : Control, new()
{
    /// <summary>
    /// Gets a reference to the dependency property that holds the 
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </summary>
    /// <returns>
    /// The dependency property that holds the
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </returns>
    protected abstract DependencyProperty GetValueProperty();

    /// <summary>
    /// Gets a reference to the dependency property that holds the 
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </summary>
    /// <returns>
    /// The dependency property that holds the
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </returns>
    protected abstract DependencyProperty GetMinProperty();

    /// <summary>
    /// Gets a reference to the dependency property that holds the 
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </summary>
    /// <returns>
    /// The dependency property that holds the
    /// <typeparamref name="TControl"/>'s minimum value.
    /// </returns>
    protected abstract DependencyProperty GetMaxProperty();

    /// <inheritdoc/>
    public abstract FrameworkElement Build(TViewModel viewModel);

    /// <summary>
    /// When overriden in a derived class, allows for custom control
    /// configuration.
    /// </summary>
    /// <param name="control">Control to configure.</param>
    protected virtual void ConfigureControl(TControl control)
    {
    }

    /// <summary>
    /// When overriden in a derived class, allows for custom binding
    /// configuration.
    /// </summary>
    /// <param name="binding">Binding to configure.</param>
    protected virtual void ConfigureValueBinding(Binding binding)
    {
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
    }

    /// <summary>
    /// Generates a new control with the required properties pre-configured for
    /// binding.
    /// </summary>
    /// <param name="valueProperty">
    /// <see cref="DependencyProperty"/> used by the control to hold the
    /// current value.
    /// </param>
    /// <param name="valuePath">
    /// Value path to bind the control's value property to.
    /// </param>
    /// <param name="viewModel">
    /// Reference to the input ViewModel for which the input control is being
    /// generated.
    /// </param>
    /// <param name="uniqueConfig">Unique control configuration callback.</param>
    /// <returns></returns>
    protected TControl NewControl(DependencyProperty valueProperty, string valuePath, IInputDialogViewModel<TValue> viewModel, Action<TControl>? uniqueConfig = null)
    {
        var control = new TControl();
        uniqueConfig?.Invoke(control);
        var binding = new Binding(valuePath) { Mode = BindingMode.TwoWay };
        ConfigureControl(control);
        ConfigureValueBinding(binding);
        control.SetBinding(valueProperty, binding);
        if (viewModel.Minimum is not null) control.SetBinding(GetMinProperty(), new Binding(nameof(IInputDialogViewModel<TValue>.Minimum)));
        if (viewModel.Maximum is not null) control.SetBinding(GetMaxProperty(), new Binding(nameof(IInputDialogViewModel<TValue>.Maximum)));
        return control;
    }
}