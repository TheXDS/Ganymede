using System;
using System.Windows.Markup;
using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Markup extension that allows quick definition of a dialog service of
/// type <see cref="NavigatingDialogService"/>.
/// </summary>
public sealed class NavDialogService : MarkupExtension
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new NavigatingDialogService();
    }
}