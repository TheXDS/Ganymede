using System.Globalization;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Implements a value converter that can split and join strings to and from
/// string arrays.
/// </summary>
public class StringArrayToStringConverter : IValueConverter<string, string[]>
{
    private static string Separator(CultureInfo? culture)
    {
        return (culture ?? CultureInfo.InvariantCulture).TextInfo.ListSeparator;
    }

    /// <inheritdoc/>
    public string[] Convert(string value, object? parameter, CultureInfo? culture)
    {
        return value.Split(Separator(culture));
    }

    /// <inheritdoc/>
    public string ConvertBack(string[] value, object? parameter, CultureInfo culture)
    {
        return string.Join(Separator(culture), value);
    }
}