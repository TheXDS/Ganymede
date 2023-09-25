using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the
/// <see cref="INullablePropertyDescriptor"/>
/// </summary>
public static class NullablePropertyDescriptorExtensions
{
    /// <summary>
    /// Marks a property as nullable.
    /// </summary>
    /// <typeparam name="TDescriptor"></typeparam>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    public static TDescriptor Nullable<TDescriptor>(this TDescriptor descriptor)
        where TDescriptor : INullablePropertyDescriptor
    {
        descriptor.SetValue(true);
        return descriptor;
    }
}