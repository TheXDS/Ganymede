namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines an <see cref="IPropertyDescriptor"/> that strongly specifies the
/// property type to which it applies.
/// </summary>
/// <typeparam name="T">Type of the property being described.</typeparam>
public interface IPropertyDescriptor<out T> : IPropertyDescriptor
{
}