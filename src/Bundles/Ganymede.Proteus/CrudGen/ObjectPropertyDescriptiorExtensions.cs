using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="ICollectionPropertyDescriptor"/> interface.
/// </summary>
public static class ObjectPropertyDescriptiorExtensions
{
    /// <summary>
    /// Indicates that a property must support adding existing entities.
    /// </summary>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Selectable<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IObjectPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    /// <summary>
    /// Indicates that a property must support creating new entities. This
    /// also implies the ability to update existing items.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Creatable<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IObjectPropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }

    /// <summary>
    /// Indicates the available models (by their specific CRUD descriptions)
    /// that can be created, selected and/or edited on the generated UI element.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <param name="models">
    /// Array of <see cref="ICrudDescription"/> with all of the available
    /// models that can be created/selected/edited.
    /// </param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor AvailableModels<TDescriptor>(this TDescriptor descriptor, params ICrudDescription[] models) where TDescriptor : IObjectPropertyDescriptor
    {
        descriptor.SetValue(models);
        return descriptor;
    }
}
