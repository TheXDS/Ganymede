using System.Windows;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Views;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a <see cref="ConventionVisualResolver{TVisual}"/> that specifies
/// the type of visual elements to resolve as <see cref="FrameworkElement"/>.
/// </summary>
public class WpfConventionVisualResolver : ConventionVisualResolver<FrameworkElement>
{
#if DEBUG
    /// <inheritdoc/>
    public override FrameworkElement? Resolve(IViewModel viewModel)
    {
        return base.Resolve(viewModel) ?? new GanymedeNavErrorFallbackView();
    }
#endif
}