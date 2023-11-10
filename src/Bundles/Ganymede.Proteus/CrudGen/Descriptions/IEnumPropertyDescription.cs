﻿using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines a set of properties to be exposed by a descriptor for
/// <see cref="Enum"/> properties.
/// </summary>
public interface IEnumPropertyDescription : IPropertyDescription
{
    /// <summary>
    /// Gets a value that determines if the described Enum should be presented
    /// as Flags.
    /// </summary>
    bool Flags => GetStructValue<bool>() ?? Property.HasAttribute<FlagsAttribute>();
}