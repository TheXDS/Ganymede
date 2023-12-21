using System.Globalization;
using System.Windows;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Converts a <see cref="IViewModel"/> into a visual by resolving it using a
/// <see cref="ConventionVisualResolver{TVisual}"/> of type
/// <see cref="FrameworkElement"/>.
/// </summary>
public sealed class ConventionVisualConverter : IOneWayValueConverter<IViewModel?, FrameworkElement?>
{
    private readonly ConventionVisualResolver<FrameworkElement> _resolver = new();

    /// <inheritdoc/>
    public FrameworkElement? Convert(IViewModel? value, object? parameter, CultureInfo? culture)
    {
        return value is not null ? _resolver.Resolve(value) : null;
    }
}

