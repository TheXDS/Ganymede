using System;
using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Markup extension that allows quick definition of a dialog service of
/// type <see cref="NavigatingDialogService"/>.
/// </summary>
public sealed partial class NavDialogService
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new NavigatingDialogService();
    }
}
