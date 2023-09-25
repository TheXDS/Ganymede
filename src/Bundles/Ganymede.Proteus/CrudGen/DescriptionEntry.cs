using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Descriptors;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Represents a single description entry.
/// </summary>
/// <param name="Descriptor">
/// Descriptor instance with the description setters used to describe the 
/// property.
/// </param>
/// <param name="Description">
/// Description instance with the information about the description of the
/// property.
/// </param>
public record class DescriptionEntry(IPropertyDescriptor Descriptor, IPropertyDescription Description);
