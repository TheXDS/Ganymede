using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.Ganymede.CrudGen.Descriptions;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Implements the basic functionality of a property descriptor class.
/// </summary>
public class PropertyDescriptor : IPropertyDescriptor, IPropertyDescription
{
    private readonly Dictionary<string, object?> values = new();

    /// <summary>
    /// Gets a value from this property descriptor.
    /// </summary>
    /// <param name="name">Name of the value to get.</param>
    /// <returns>
    /// The requested property description value, or <see langword="null"/> if
    /// the requested property description value has not been set.
    /// </returns>
    public object? GetValue([CallerMemberName] string name = null!) => values.TryGetValue(name, out object? value) ? value : null;

    /// <summary>
    /// Sets a value on this property descriptor.
    /// </summary>
    /// <param name="value">Value to set.</param>
    /// <param name="name">Name of the value to set.</param>
    public void SetValue(object? value, [CallerMemberName] string name = null!)
    {
        if (!values.ContainsKey(name)) values.Add(name, value);
        else values[name] = value;
    }

    /// <inheritdoc/>
    public PropertyInfo Property { get; init; } = null!;
}

/// <summary>
/// Implements the basic functionality of a property descriptor class.
/// </summary>
/// <typeparam name="T">Type of the described property.</typeparam>
public class PropertyDescriptor<T> : PropertyDescriptor, IPropertyDescriptor<T>
{
}