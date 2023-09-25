namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines an <see cref="INumericPropertyDescriptor{T}"/> used to specifically
/// describe <see cref="DateTime"/> properties.
/// </summary>
public interface IDatePropertyDescriptor : INumericPropertyDescriptor<DateTime>
{
    /// <summary>
    /// Specifies that the described <see cref="DateTime"/> property must
    /// include the time component.
    /// </summary>
    /// <returns>This same descriptor instance.</returns>
    IDatePropertyDescriptor WithTime()
    {
        SetValue(true);
        return this;
    }
}
