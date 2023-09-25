using TheXDS.Ganymede.CrudGen.Descriptors;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the 
/// <see cref="INumericPropertyDescriptor{T}"/> interface.
/// </summary>
public static class NumericPropertyDescriptorExtensions
{
    /// <summary>
    /// Specifies a valid range of values for this property.
    /// </summary>
    /// <typeparam name="TDescriptor">Type of property descriptor.</typeparam>
    /// <typeparam name="TValue">Type of property value.</typeparam>
    /// <param name="descriptor">Descriptor instance to configure.</param>
    /// <param name="min">Minimum valid value for the property.</param>
    /// <param name="max">Maximum valid value for the property.</param>
    /// <returns>The same instance as <paramref name="descriptor"/>.</returns>
    public static TDescriptor ValidRange<TDescriptor, TValue>(this TDescriptor descriptor, TValue min, TValue max)
        where TDescriptor : INumericPropertyDescriptor<TValue>
        where TValue : unmanaged, IComparable<TValue>
    {
        descriptor.SetValue(new Range<TValue>(min, max));
        return descriptor;
    }
}
