using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.Ganymede.Helpers;

namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Enumerates common property descriptions that can be used to configure
/// visual elements generation.
/// </summary>
public interface IPropertyDescription
{
    /// <summary>
    /// Gets a reference to the described property.
    /// </summary>
    PropertyInfo Property { get; init; }

    /// <summary>
    /// Gets a reference to the full Model description.
    /// </summary>
    ICrudDescription Parent { get; init; }

    /// <summary>
    /// Gets the desired label to be displayed for the visual element used to
    /// view/edit the property.
    /// </summary>
    string Label => Parent.ResourceType.GetLabel(GetClassValue<string>() ?? Property.Name);

    /// <summary>
    /// Gets an optional desired icon to be set on the generated visual
    /// element. If not specified, the visual element generator may choose a
    /// default icon to be presented.
    /// </summary>
    string? Icon => GetClassValue<string>();

    /// <summary>
    /// Gets a value that indicates whether or not the visual element must be
    /// generated as read-only for editor Views.
    /// </summary>
    bool ReadOnly => GetStructValue<bool>() ?? !Property.CanWrite;

    /// <summary>
    /// Gets a value that indicates whether or not the property should be
    /// hidden from the generated details view.
    /// </summary>
    bool HideFromDetails => GetStructValue<bool>() ?? false;

    /// <summary>
    /// Gets a value that indicates whether or not the property should be
    /// hidden from the generated details view.
    /// </summary>
    bool HideFromEditor => GetStructValue<bool>() ?? false;

    /// <summary>
    /// Gets a reference type value from the property description repository.
    /// </summary>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <param name="name">Name of the value to get.</param>
    /// <returns>
    /// The value from the property description repository, or 
    /// <see langword="null"/> if no such value exists or cannot be cast to
    /// <typeparamref name="T"/>.
    /// </returns>
    protected T? GetClassValue<T>([CallerMemberName] string name = null!) where T : class
    {
        return GetValue(name) as T;
    }

    /// <summary>
    /// Gets a value type value from the property description repository.
    /// </summary>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <param name="name">Name of the value to get.</param>
    /// <returns>
    /// The value from the property description repository, or 
    /// <see langword="default"/> if no such value exists or cannot be cast to
    /// <typeparamref name="T"/>. The return value might be null for
    /// <see cref="Nullable{T}"/> value types.
    /// </returns>
    protected T? GetStructValue<T>([CallerMemberName] string name = null!) where T : struct
    {
        return GetValue(name) is T v ? v : default;
    }

    /// <summary>
    /// Gets a value from the property description repository.
    /// </summary>
    /// <param name="name">Name of the value to get.</param>
    /// <returns>
    /// The value from the property description repository, or
    /// <see langword="null"/> if no such value exists.
    /// </returns>
    object? GetValue([CallerMemberName] string name = null!);
}
