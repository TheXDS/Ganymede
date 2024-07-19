using Avalonia;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Views;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a <see cref="ConventionVisualResolver{TVisual}"/> that specifies
/// the type of visual elements to resolve as <see cref="StyledElement"/>.
/// </summary>
public class AvaloniaConventionVisualResolver : ConventionVisualResolver<StyledElement>
{
#if DEBUG
    /// <inheritdoc/>
    public override StyledElement? Resolve(IViewModel viewModel)
    {
        return base.Resolve(viewModel) ?? new GanymedeNavErrorFallbackView();
    }
#endif
}
