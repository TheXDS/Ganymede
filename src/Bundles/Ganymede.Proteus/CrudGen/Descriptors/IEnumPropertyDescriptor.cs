namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a property descriptor used to describe <see cref="Enum"/>
/// properties.
/// </summary>
public interface IEnumPropertyDescriptor : IPropertyDescriptor
{
    /// <summary>
    /// Explicitly indicates that the enum values should be rendered as if they are flags.
    /// </summary>
    /// <returns>This same descriptor instance.</returns>
    IEnumPropertyDescriptor Flags()
    {
        SetValue(true);
        return this;
    }
}