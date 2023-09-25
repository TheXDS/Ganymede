using System.Globalization;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Implements a value converter that connverts between
/// <c><see cref="bool"/>?</c> and <c><see cref="bool"/></c> objects.
/// </summary>
public class NullableBoolToBoolConverter : IValueConverter<bool?, bool>
{
    /// <inheritdoc/>
    public bool Convert(bool? value, object? parameter, CultureInfo? culture)
    {
        return value ?? false;
    }

    /// <inheritdoc/>
    public bool? ConvertBack(bool value, object? parameter, CultureInfo culture)
    {
        return value;
    }
}