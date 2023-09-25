using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Defines a set of members to be implemented by a type that can resolve
/// visual containers for instances of the <see cref="ViewModel"/> class.
/// </summary>
/// <typeparam name="TVisual">
/// Type of visual container resolution to implement.
/// </typeparam>
public interface IVisualResolver<TVisual>
{
    /// <summary>
    /// Resolves a visual that can host the specified <see cref="ViewModel"/>.
    /// </summary>
    /// <param name="viewModel">
    /// <see cref="ViewModel"/> for which to resolve a visual container.
    /// </param>
    /// <returns>
    /// A visual container that can host the specified <see cref="ViewModel"/>,
    /// or <see langword="null"/> if this instance is unable to resolve a
    /// suitable visual container.
    /// </returns>
    TVisual? Resolve(ViewModelBase viewModel);
}
