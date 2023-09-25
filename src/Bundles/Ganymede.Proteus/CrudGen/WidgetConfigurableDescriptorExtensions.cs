using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="IWidgetConfigurableDescriptor"/> interface.
/// </summary>
public static class WidgetConfigurableDescriptorExtensions
{
    /// <summary>
    /// Sets the desired widget size.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <param name="size">Desired widget size.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor WidgetSize<TDescriptor>(this TDescriptor descriptor, WidgetSize size) where TDescriptor : IWidgetConfigurableDescriptor
    {
        descriptor.SetValue(size);
        return descriptor;
    }
}