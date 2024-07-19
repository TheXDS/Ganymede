using System.Windows;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Base class for statically typed visual resolvers.
/// </summary>
/// <typeparam name="TViewModel">
/// Type of ViewModel to resolve.
/// </typeparam>
/// <typeparam name="TVisual">
/// Type of visual element to be returned.
/// </typeparam>
/// <remarks>
/// If you need to register many strictly-mapped types, consider using a <see cref="DictionaryVisualResolver{T}"/> instead.
/// </remarks>
/// <seealso cref="DictionaryVisualResolver{T}"/>
public abstract class TypedVisualResolverBase<TViewModel, TVisual> : IVisualResolver<TVisual> where TViewModel : IViewModel where TVisual : FrameworkElement, new()
{
    /// <inheritdoc/>
    public TVisual? Resolve(IViewModel viewModel)
    {
        return (viewModel is TViewModel vm) ? UiThread.Invoke(() =>
        {
            var page = new TVisual() { DataContext = vm };
            return page;
        }) : null;
    }
}
