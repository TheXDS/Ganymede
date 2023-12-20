using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Defines a set of members to be implemented by a type that can resolve
/// visual containers for instances of the <see cref="IViewModel"/>.
/// </summary>
/// <typeparam name="TVisual">
/// Type of visual container resolution to implement.
/// </typeparam>
public interface IVisualResolver<out TVisual>
{
    /// <summary>
    /// Resolves a visual that can host the specified <see cref="IViewModel"/>.
    /// </summary>
    /// <param name="viewModel">
    /// <see cref="IViewModel"/> for which to resolve a visual container.
    /// </param>
    /// <returns>
    /// A visual container that can host the specified
    /// <see cref="IViewModel"/>, or <see langword="null"/> if this instance is
    /// unable to resolve a suitable visual container.
    /// </returns>
    TVisual? Resolve(IViewModel viewModel);
}
