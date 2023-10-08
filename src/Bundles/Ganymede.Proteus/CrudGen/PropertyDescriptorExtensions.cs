using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Contains a set of extensions that help describe the layout generation for a
/// property, allowing the property descriptor type to cascade down each
/// description call.
/// </summary>
public static class PropertyDescriptorExtensions
{
    /// <summary>
    /// Specifies a label to be used when presenting this property.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor onto which add the description.
    /// </typeparam>
    /// <param name="descriptor">
    /// Descriptor onto which add the description.
    /// </param>
    /// <param name="label">Label to be displayed for the property.</param>
    /// <returns>
    /// The same instance as <paramref name="descriptor"/>, allowing the use of
    /// Fluent syntax.
    /// </returns>
    public static TDescriptor Label<TDescriptor>(this TDescriptor descriptor, string label)
        where TDescriptor : IPropertyDescriptor
    {
        descriptor.SetValue(label);
        return descriptor;
    }

    /// <summary>
    /// Sets a default value to be used when editing the specified property.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor onto which add the description.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to be used as a default for the specified property.
    /// </typeparam>
    /// <param name="descriptor">
    /// Descriptor onto which add the description.
    /// </param>
    /// <param name="value">
    /// Value to be used as a default for the specified property.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="descriptor"/>, allowing the use of
    /// Fluent syntax.
    /// </returns>
    public static TDescriptor DefaultValue<TDescriptor, TValue>(this TDescriptor descriptor, TValue value)
        where TDescriptor : IPropertyDescriptor<TValue>
    {
        descriptor.SetValue(value);
        return descriptor;
    }

    /// <summary>
    /// Sets the property to be read-only on the generated editor view.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor onto which add the description.
    /// </typeparam>
    /// <param name="descriptor">
    /// Descriptor onto which add the description.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="descriptor"/>, allowing the use of
    /// Fluent syntax.
    /// </returns>
    public static TDescriptor ReadOnly<TDescriptor>(this TDescriptor descriptor)
        where TDescriptor : IPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    /// <summary>
    /// Indicates that the property should not be shown in the generated
    /// details view.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor onto which add the description.
    /// </typeparam>
    /// <param name="descriptor">
    /// Descriptor onto which add the description.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="descriptor"/>, allowing the use of
    /// Fluent syntax.
    /// </returns>
    public static TDescriptor HideFromDetails<TDescriptor>(this TDescriptor descriptor)
            where TDescriptor : IPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    /// <summary>
    /// Indicates that the property should not be shown in the generated
    /// details view.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor onto which add the description.
    /// </typeparam>
    /// <param name="descriptor">
    /// Descriptor onto which add the description.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="descriptor"/>, allowing the use of
    /// Fluent syntax.
    /// </returns>
    public static TDescriptor HideFromEditor<TDescriptor>(this TDescriptor descriptor)
            where TDescriptor : IPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }
}
