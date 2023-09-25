namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines an <see cref="INumericPropertyDescriptor{T}"/> used to specifically
/// describe <see cref="DateTime"/> properties.
/// </summary>
public interface INullableDatePropertyDescriptor : INullableNumericPropertyDescriptor<DateTime>
{
    /// <summary>
    /// Specifies that the described <see cref="DateTime"/> property must
    /// include the time component.
    /// </summary>
    /// <returns>This same descriptor instance.</returns>
    INullableDatePropertyDescriptor WithTime()
    {
        SetValue(true);
        return this;
    }
}
