using System;
using System.Windows;
using System.Windows.Markup;
using TheXDS.Ganymede.Component;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that returns a
/// <see cref="ProteusStackVisualResolver"/> adapted to resolve visuals of type
/// <see cref="FrameworkElement"/>.
/// </summary>
public class ProteusCrudBuilder : MarkupExtension
{
    /// <inheritdoc/>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return new ProteusStackVisualResolver();
    }
}
