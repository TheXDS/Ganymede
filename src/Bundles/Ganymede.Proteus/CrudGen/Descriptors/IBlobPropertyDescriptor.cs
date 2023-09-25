using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a set of members to be implemented by a property descriptor for
/// properties of type <c><see cref="byte"/>[]</c>.
/// </summary>
public interface IBlobPropertyDescriptor : IPropertyDescriptor<byte[]>
{
}

/// <summary>
/// Defines a set of members to be implemented by a property descriptor for
/// properties of type <c><see cref="Model"/></c>.
/// </summary>
public interface ICollectionPropertyDescriptor : IPropertyDescriptor, IWidgetConfigurableDescriptor
{
}

public interface IWidgetConfigurableDescriptor : IPropertyDescriptor
{
}