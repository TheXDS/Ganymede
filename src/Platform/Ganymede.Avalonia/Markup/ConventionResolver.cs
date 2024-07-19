using Avalonia;
using Avalonia.Markup.Xaml;
using TheXDS.Ganymede.Component;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that returns a
/// <see cref="ConventionVisualResolver{TVisual}"/> adapted to resolve visuals
/// of type <see cref="StyledElement"/>.
/// </summary>
public sealed class ConventionResolver : MarkupExtension
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AvaloniaConventionVisualResolver();
    }
}
