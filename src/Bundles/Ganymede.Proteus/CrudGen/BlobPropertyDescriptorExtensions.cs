using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="IBlobPropertyDescriptor"/> interface.
/// </summary>
public static class BlobPropertyDescriptorExtensions
{
    /// <summary>
    /// Declares the type of data stored in this property.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <param name="type">Type of data stored in this property.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Type<TDescriptor>(this TDescriptor descriptor, BlobType type)
        where TDescriptor : IBlobPropertyDescriptor
    {
        descriptor.SetValue(type);
        return descriptor;
    }

    /// <summary>
    /// Marks this binary blob as a password storage field, allowing for
    /// further password-related descriptions to be applied to it.
    /// </summary>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <returns>
    /// An <see cref="IPasswordPropertyDescriptor"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// Thrown if the descriptor cannot be cast to a
    /// <see cref="IPasswordPropertyDescriptor"/> instance.
    /// </exception>
    public static IPasswordPropertyDescriptor Password(this IBlobPropertyDescriptor descriptor)
    {
        descriptor.Type(BlobType.Password);
        return descriptor as IPasswordPropertyDescriptor ?? throw new InvalidCastException();
    }
}

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="ICollectionPropertyDescriptor"/> interface.
/// </summary>
public static class CollectionPropertyDescriptiorExtensions
{
    /// <summary>
    /// Indicates that a collection must support adding existing entities.
    /// </summary>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Linkable<TDescriptor>(this TDescriptor descriptor) where TDescriptor : ICollectionPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    /// <summary>
    /// Indicates that a collection must support creating new entities. This
    /// also implies the ability to update items already on the list.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Creatable<TDescriptor>(this TDescriptor descriptor) where TDescriptor : ICollectionPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    public static TDescriptor AvailableModels<TDescriptor>(this TDescriptor descriptor, params Type[] models) where TDescriptor : ICollectionPropertyDescriptor
    {
        descriptor.SetValue(models);
        return descriptor;
    }
}
