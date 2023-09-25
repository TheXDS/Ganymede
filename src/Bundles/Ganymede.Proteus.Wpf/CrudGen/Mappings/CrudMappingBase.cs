using System;
using TheXDS.Ganymede.CrudGen.Descriptions;

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
}
