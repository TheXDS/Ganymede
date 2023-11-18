using System;
using System.Windows;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Base class for all CRUD mappings that includes some helper functions to aid
/// in control generation.
/// </summary>
public abstract class CrudMappingBase
{
    /// <summary>
    /// Tries to get a value from a <see cref="IPropertyDescription"/> of a 
    /// specific type, and executes the <paramref name="setCallback"/> if a
    /// non-null value is found.
    /// </summary>
    /// <typeparam name="TDescription">
    /// Type of <see cref="IPropertyDescription"/> to try to cast
    /// <paramref name="description"/> to.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to get from <paramref name="description"/>.
    /// </typeparam>
    /// <param name="description">Property description to try to cast.</param>
    /// <param name="valueGetter">
    /// Callback to fetch the required value from
    /// <paramref name="description"/> if successfully cast to
    /// <typeparamref name="TDescription"/>.
    /// </param>
    /// <param name="setCallback">
    /// Callback to execute if the result of calling
    /// <paramref name="valueGetter"/> is not <see langword="null"/>.
    /// </param>
    protected static void SetIf<TDescription, TValue>(IPropertyDescription description, Func<TDescription, TValue?> valueGetter, Action<TValue> setCallback)
        where TValue : notnull
    {
        if (description is TDescription d && valueGetter(d) is { } v) setCallback(v);
    }

    /// <summary>
    /// Tries to get a value from a <see cref="IPropertyDescription"/> of a 
    /// specific type, and executes the <paramref name="setCallback"/> if a
    /// non-default value is found.
    /// </summary>
    /// <typeparam name="TDescription">
    /// Type of <see cref="IPropertyDescription"/> to try to cast
    /// <paramref name="description"/> to.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to get from <paramref name="description"/>.
    /// </typeparam>
    /// <param name="description">Property description to try to cast.</param>
    /// <param name="valueGetter">
    /// Callback to fetch the required value from
    /// <paramref name="description"/> if successfully cast to
    /// <typeparamref name="TDescription"/>.
    /// </param>
    /// <param name="setCallback">
    /// Callback to execute if the result of calling
    /// <paramref name="valueGetter"/> is not <see langword="default"/>.
    /// </param>
    protected static void SetIf<TDescription, TValue>(IPropertyDescription description, Func<TDescription, TValue?> valueGetter, Action<TValue> setCallback)
        where TValue : struct
    {
        if (description is TDescription d && valueGetter(d) is { } v) setCallback(v);
    }

    /// <summary>
    /// Manually sets a value onto the entity.
    /// </summary>
    /// <typeparam name="TValue">Type of value to be set.</typeparam>
    /// <param name="control">Control instance.</param>
    /// <param name="valueCallback">Value callback.</param>
    /// <param name="description">Property description.</param>
    protected static void SetEntityValue<TValue>(object control, ValueGetCallback<TValue> valueCallback, IPropertyDescription description)
    {
        if (control is FrameworkElement { DataContext: CrudEditorViewModel { Entity: Model entity } } f)
        {
            description.Property.SetValue(entity, valueCallback.Invoke(f, (TValue)description.Property.GetValue(entity)!));
        }
    }

    /// <summary>
    /// Describes a method that receives an old value, and outputs a new value.
    /// </summary>
    /// <typeparam name="TValue">Value to get.</typeparam>
    /// <param name="control">Control from which to get a new value.</param>
    /// <param name="oldValue">Old value currently set on the entity.</param>
    /// <returns>The new value to be set onto the entity.</returns>
    protected delegate TValue ValueGetCallback<TValue>(FrameworkElement control, TValue oldValue);
}
