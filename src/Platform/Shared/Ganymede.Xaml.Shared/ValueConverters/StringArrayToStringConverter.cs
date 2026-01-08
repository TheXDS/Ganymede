using System.Globalization;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Implements a value converter that can split and join strings to and from
/// string arrays.
/// </summary>
public class StringArrayToStringConverter : IValueConverter<string, string[]>
{
    private string GetSeparator(CultureInfo? culture)
    {
        return Separator ?? (culture ?? CultureInfo.InvariantCulture).TextInfo.ListSeparator;
    }

    /// <summary>
    /// Gets or sets the separator string to use when splitting and joining
    /// strings.
    /// </summary>
    /// <remarks>
    /// If not specified, the current culture's list separator will be used or,
    /// in its abscence, the invariant culture's list separator.
    /// </remarks>
    public string? Separator { get; set; }

    /// <inheritdoc/>
    public string[] Convert(string value, object? parameter, CultureInfo? culture)
    {
        return value.Split(GetSeparator(culture));
    }

    /// <inheritdoc/>
    public string ConvertBack(string[] value, object? parameter, CultureInfo culture)
    {
        return string.Join(GetSeparator(culture), value);
    }
}