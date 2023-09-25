using TheXDS.Ganymede.CrudGen.Descriptions;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Implements a <see cref="PropertyDescriptor"/> for numeric properties.
/// </summary>
/// <typeparam name="T">Type of property to describe.</typeparam>
public class NumericPropertyDescriptor<T> : PropertyDescriptor, INullableNumericPropertyDescriptor<T>, INullablePropertyDescription, INumericPropertyDescriptor<T>, INumericPropertyDescription<T> where T : unmanaged, IComparable<T>
{
}
