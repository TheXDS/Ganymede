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
    /// <param name="changedValue">
    /// Callback to execute whenever this dependency property changes its
    /// value.
    /// </param>
    /// <param name="coerceValue">
    /// Callback to execute whenever a value needs to be coerced.
    /// </param>
    /// <param name="validate">
    /// Callback to execute whenever a value need to be validated before 
    /// assigning the dependency property.
    /// </param>
    /// <returns>A new dependency property.</returns>
    public static DependencyProperty NewDp<TValue, TOwner>(string name, [MaybeNull] TValue defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null, ValidateValueCallback? validate = null)
        where TOwner : DependencyObject
    {
        return DependencyProperty.Register(name, typeof(TValue), typeof(TOwner), new PropertyMetadata(defaultValue, changedValue, coerceValue), validate);
    }

    /// <summary>
    /// Shortens the syntax/semantics required to declare and create a new
    /// dependency property that binds 2-way by default.
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
    /// <param name="changedValue">
    /// Callback to execute whenever this dependency property changes its
    /// value.
    /// </param>
    /// <param name="coerceValue">
    /// Callback to execute whenever a value needs to be coerced.
    /// </param>
    /// <param name="validate">
    /// Callback to execute whenever a value need to be validated before 
    /// assigning the dependency property.
    /// </param>
    /// <returns>A new dependency property.</returns>
    public static DependencyProperty NewDp2Way<TValue, TOwner>(string name, [MaybeNull] TValue defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null, ValidateValueCallback? validate = null)
        where TOwner : DependencyObject
    {
        return DependencyProperty.Register(name, typeof(TValue), typeof(TOwner), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, changedValue, coerceValue), validate);
    }

    /// <summary>
    /// Shortens the syntax/semantics required to add an owner type to a
    /// dependency property.
    /// </summary>
    /// <typeparam name="TOwner">Owner type to add.</typeparam>
    /// <param name="property">Property to add the new owner to.</param>
    /// <param name="defaultValue">
    /// Optional. Defines a default value to use for this dependency property.
    /// </param>
    /// <param name="changedValue">
    /// Callback to execute whenever this dependency property changes its
    /// value.
    /// </param>
    /// <param name="coerceValue">
    /// Callback to execute whenever a value needs to be coerced.
    /// </param>
    public static void AddOwner<TOwner>(this DependencyProperty property, [MaybeNull] object defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null)
            where TOwner : DependencyObject
    {
        property.AddOwner(typeof(TOwner), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, changedValue, coerceValue));
    }

    /// <summary>
    /// Shortens the syntax/semantics required to override the metadata
    /// associated with a dependency property.
    /// </summary>
    /// <typeparam name="TOwner">Owner type to add.</typeparam>
    /// <param name="property">Property to add the new owner to.</param>
    /// <param name="defaultValue">
    /// Optional. Defines a default value to use for this dependency property.
    /// </param>
    /// <param name="changedValue">
    /// Callback to execute whenever this dependency property changes its
    /// value.
    /// </param>
    /// <param name="coerceValue">
    /// Callback to execute whenever a value needs to be coerced.
    /// </param>
    public static void OverrideMetadata<TOwner>(this DependencyProperty property, [MaybeNull] object defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null)
            where TOwner : DependencyObject
    {
        property.OverrideMetadata(typeof(TOwner), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, changedValue, coerceValue));
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
    /// <param name="changedValue">
    /// Callback to execute whenever this dependency property changes its
    /// value.
    /// </param>
    /// <param name="coerceValue">
    /// Callback to execute whenever a value needs to be coerced.
    /// </param>
    /// <param name="validate">
    /// Callback to execute whenever a value need to be validated before
    /// assigning the dependency property.
    /// </param>
    /// <returns>
    /// A tuple consisting of a new <see cref="DependencyPropertyKey"/> and its
    /// corresponding <see cref="DependencyProperty"/> that represents the
    /// dependency property.
    /// </returns>
    public static (DependencyPropertyKey, DependencyProperty) NewDpRo<TValue, TOwner>(string name, [MaybeNull] TValue defaultValue = default!, PropertyChangedCallback? changedValue = null, CoerceValueCallback? coerceValue = null, ValidateValueCallback? validate = null)
        where TOwner : DependencyObject
    {
        var dpk = DependencyProperty.RegisterReadOnly(name, typeof(TValue), typeof(TOwner), new PropertyMetadata(defaultValue, changedValue, coerceValue), validate);
        return (dpk, dpk.DependencyProperty);
    }

    /// <summary>
    /// Shortens the syntax/semantics required to override and set the default
    /// style for a custom control.
    /// </summary>
    /// <typeparam name="T">
    /// Type of control to set the default style for.
    /// </typeparam>
    /// <param name="styleDp">
    /// Style property. Must be equal to
    /// <see cref="FrameworkElement.DefaultStyleKeyProperty"/>.
    /// </param>
    public static void SetControlStyle<T>(DependencyProperty styleDp) where T : FrameworkElement
    {
        styleDp.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(typeof(T)));
    }
}
