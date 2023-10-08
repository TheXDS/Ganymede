using TheXDS.Ganymede.CrudGen.Descriptions;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Describes a binary property, generally using a <c><see cref="byte"/>[]</c>
/// type.
/// </summary>
public class BlobPropertyDescriptor
    : PropertyDescriptor,
    IBlobPropertyDescriptor,
    IPasswordPropertyDescriptor,
    IBlobPropertyDescription,
    IPasswordPropertyDescription
{
}
