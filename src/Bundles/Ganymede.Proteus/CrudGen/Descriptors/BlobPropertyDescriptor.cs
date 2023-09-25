using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Triton.Models.Base;

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

/// <summary>
/// Describes a property that holds a <c><see cref="Model"/></c> collection.
/// </summary>
public class CollectionPropertyDescription
    : PropertyDescriptor,
    ICollectionPropertyDescriptor,
    ICollectionPropertyDescription
{
}