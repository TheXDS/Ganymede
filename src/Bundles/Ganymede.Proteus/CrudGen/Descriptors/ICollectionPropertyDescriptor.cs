using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a set of members to be implemented by a property descriptor for
/// properties that hold collections of <c><see cref="Model"/></c>.
/// </summary>
public interface ICollectionPropertyDescriptor : IObjectPropertyDescriptor, IWidgetConfigurableDescriptor
{
}
