using System.Globalization;
using TheXDS.MCART.ValueConverters.Base;
using DC = System.Drawing.Color;
using MB = Avalonia.Media.Brush;
using MC = Avalonia.Media.Color;
using SB = Avalonia.Media.SolidColorBrush;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Implements a value converter that converts between <see cref="DC"/> and <see cref="MB"/> objects.
/// </summary>
public class DrawingColorToMediaBrushConverter : IValueConverter<DC, MB>
{
    /// <inheritdoc/>
    public MB Convert(DC value, object? parameter, CultureInfo? culture) => new SB(MC.FromArgb(value.A, value.R, value.G, value.B));

    /// <inheritdoc/>
    public DC ConvertBack(MB value, object? parameter, CultureInfo culture)
    {
        return value is SB { Color: { } c }
            ? DC.FromArgb(c.A, c.R, c.G, c.B)
            : DC.Transparent;
    }
}
