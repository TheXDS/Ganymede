namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines an <see cref=" INumericPropertyDescriptor{T}"/> that accepts
/// nullability configuration.
/// </summary>
/// <typeparam name="T">Type of the property being described.</typeparam>
public interface INullableNumericPropertyDescriptor<T> : INumericPropertyDescriptor<T>, INullablePropertyDescriptor where T : unmanaged, IComparable<T>
{
}