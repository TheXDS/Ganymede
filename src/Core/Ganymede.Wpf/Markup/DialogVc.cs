using System;
using System.Windows.Markup;
using TheXDS.Ganymede.ValueConverters;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that helps define a
/// <see cref="DialogVisualConverter"/> quickly.
/// </summary>
public sealed class DialogVc : MarkupExtension
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider) => new DialogVisualConverter();
}
