namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines a set of properties to be exposed by a descriptor for
/// nullable properties.
/// </summary>
public interface INullablePropertyDescription : IPropertyDescription
{
    /// <summary>
    /// Gets a value that indicates if the property can be set to null.
    /// </summary>
    bool Nullable => GetStructValue<bool>() ?? false;
}