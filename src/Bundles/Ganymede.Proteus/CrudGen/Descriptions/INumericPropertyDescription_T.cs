using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines a set of properties to be exposed by a descriptor for
/// numerical properties.
/// </summary>
public interface INumericPropertyDescription<T> : IPropertyDescription<T> where T : unmanaged, IComparable<T>
{
    /// <summary>
    /// Gets a string with the intended string format for visual presentation
    /// of the value.
    /// </summary>
    string? Format => GetClassValue<string>();

    /// <summary>
    /// Gets a range of valid values for the property, or
    /// <see langword="null"/> if no o valid range of values has been
    /// specified.
    /// </summary>
    Range<T>? ValidRange => GetStructValue<Range<T>>();
}
