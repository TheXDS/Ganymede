using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="IFormattablePropertyDescriptor"/> interface.
/// </summary>
public static class FormattablePropertyDescriptorExtensions
{
    /// <summary>
    /// Specifies a format string to use when presenting the value of this
    /// property.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <param name="format">Format string to use.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor Format<TDescriptor>(this TDescriptor descriptor, string format)
        where TDescriptor : IFormattablePropertyDescriptor
    {
        descriptor.SetValue(format);
        return descriptor;
    }
}
