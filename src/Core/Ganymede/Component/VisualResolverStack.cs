using System.Collections.ObjectModel;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a collection of visual resolvers that will sequentially try to
/// resolve a visual for the specified ViewModel.
/// </summary>
/// <typeparam name="TVisual">
/// Type of visual container resolution to implement.
/// </typeparam>
public class VisualResolverStack<TVisual> : Collection<IVisualResolver<TVisual>>, IVisualResolver<TVisual>
{
    /// <inheritdoc/>
    public TVisual? Resolve(ViewModelBase viewModel)
    {
        return this.Select(p => p.Resolve(viewModel)).FirstOrDefault(p => p is not null);
    }
}