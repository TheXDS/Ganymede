using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Describes a property that holds a <c><see cref="Model"/></c> collection.
/// </summary>
public class CollectionPropertyDescriptor
    : PropertyDescriptor,
    ICollectionPropertyDescriptor,
    ICollectionPropertyDescription
{
}
