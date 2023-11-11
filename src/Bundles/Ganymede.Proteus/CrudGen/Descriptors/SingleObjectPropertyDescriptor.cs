using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Describes a property that holds a single <c><see cref="Model"/></c>
/// instance.
/// </summary>
public class SingleObjectPropertyDescriptor
    : PropertyDescriptor,
    ISingleObjectPropertyDescriptor,
    ISingleObjectPropertyDescription
{
}
