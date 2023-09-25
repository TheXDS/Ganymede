namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Enumerates common property descriptions that can be used to configure
/// visual elements generation for properties that might include default value
/// description.
/// </summary>
/// <typeparam name="T">Type of property value.</typeparam>
public interface IPropertyDescription<T> : IPropertyDescription
{
    /// <summary>
    /// Gets a default value to use as the initial state of the visual element
    /// used to edit the property.
    /// </summary>
    T DefaultValue => GetValue() is T v ? v : default!;
}