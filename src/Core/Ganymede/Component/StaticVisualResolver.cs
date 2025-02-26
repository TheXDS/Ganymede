using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that will resolve a
/// view only for the specified ViewModel type, returning
/// <see langword="null"/> otherwise.
/// </summary>
/// <typeparam name="TViewModel">Specific type of ViewModel to resolve.</typeparam>
/// <typeparam name="TVisual">View to be resolved.</typeparam>
public class StaticVisualResolver<TViewModel, TVisual> : IVisualResolver<TVisual> where TViewModel : IViewModel where TVisual : new()
{
    TVisual? IVisualResolver<TVisual>.Resolve(IViewModel viewModel)
    {
        return viewModel is TViewModel ? new TVisual() : default;
    }
}