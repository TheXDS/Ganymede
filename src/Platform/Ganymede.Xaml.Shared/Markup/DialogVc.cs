using System;
using TheXDS.Ganymede.ValueConverters;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that helps define a
/// <see cref="DialogVisualConverter"/> quickly.
/// </summary>
public sealed partial class DialogVc
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider) => new DialogVisualConverter();
}
