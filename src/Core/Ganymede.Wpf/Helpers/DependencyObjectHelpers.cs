using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Includes a set of helper functions to create dependency properties.
/// </summary>
public static class DependencyObjectHelpers
{
    /// <summary>
    /// Shortens the syntax/semantics required to declare and create a new
    /// dependency property.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to be stored to and retrieved by the new dependency
    /// property.
    /// </typeparam>
    /// <typeparam name="TOwner">Dependency property owner type.</typeparam>
    /// <param name="name">Name of the new dependency property.</param>
    /// <param name="defaultValue">
    /// Optional. Defines a default value to use for this dependency property.
    /// </param>
    /// <param name="changedValue">Callback to execute whenever this dependency property changes its value.</param>
    /// <param name="coerceValue">Callback to execute whenever a value needs to be coerced.</param>
    /// <param name="validate">Callback to execute whenever a value need to be validated before assigning the dependency property.</param>
    /// <returns>A new dependency property.</returns>
    public static DependencyProperty NewDp<TValue, TOwner>(string name, [MaybeNull] TValue defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null, ValidateValueCallback? validate = null)
        where TValue : notnull
        where TOwner : DependencyObject
    {
        return DependencyProperty.Register(name, typeof(TValue), typeof(TOwner), new PropertyMetadata(defaultValue, changedValue, coerceValue), validate);
    }

    /// <summary>
    /// Shortens the syntax/semantics required to declare and create a new
    /// read-only dependency property.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to be stored to and retrieved by the new dependency
    /// property.
    /// </typeparam>
    /// <typeparam name="TOwner">Dependency property owner type.</typeparam>
    /// <param name="name">Name of the new dependency property.</param>
    /// <param name="defaultValue">
    /// Optional. Defines a default value to use for this dependency property.
    /// </param>
    /// <param name="changedValue">Callback to execute whenever this dependency property changes its value.</param>
    /// <param name="coerceValue">Callback to execute whenever a value needs to be coerced.</param>
    /// <param name="validate">Callback to execute whenever a value need to be validated before assigning the dependency property.</param>
    /// <returns>A new <see cref="DependencyPropertyKey"/> that represents</returns>
    public static DependencyPropertyKey NewDpRo<TValue, TOwner>(string name, [MaybeNull] TValue defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null, ValidateValueCallback? validate = null)
        where TValue : notnull
        where TOwner : DependencyObject
    {
        return DependencyProperty.RegisterReadOnly(name, typeof(TValue), typeof(TOwner), new PropertyMetadata(defaultValue, changedValue, coerceValue), validate);
    }
}
