using System.Windows.Markup;
using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Markup extension that allows quick definition of a dialog service of
/// type <see cref="WpfNativeDialogService"/>.
/// </summary>
public sealed class NativeDialogs : MarkupExtension
{
    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new WpfNativeDialogService();
    }
}