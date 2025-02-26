using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that will always return
/// a new instance of the specified type.
/// </summary>
/// <typeparam name="TVisual">Type of visual element to return.</typeparam>
public class ConstVisualResolver<TVisual> : IVisualResolver<TVisual> where TVisual : new()
{
    TVisual? IVisualResolver<TVisual>.Resolve(IViewModel viewModel) => new();
}
