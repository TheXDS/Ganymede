namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines a set of properties to be exposed by a descriptor for
/// <see cref="DateTime"/> properties.
/// </summary>
public interface IDatePropertyDescription : INumericPropertyDescription<DateTime>
{
    /// <summary>
    /// Gets a value that indicates if the property should be presented with
    /// the time component.
    /// </summary>
    bool WithTime => GetStructValue<bool>() ?? false;
}
