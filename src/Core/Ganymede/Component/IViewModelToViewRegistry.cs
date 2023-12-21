using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Defines a set of members to be implemented by a type that allows for
/// ViewModel to Visual type registrations.
/// </summary>
/// <typeparam name="T">
/// Base type for the visual elements to be registered.
/// </typeparam>
public interface IViewModelToViewRegistry<T>
{

    /// <summary>
    /// Registers a ViewModel type to resolve to the specified visual type.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to map to the specified visual.
    /// </typeparam>
    /// <typeparam name="TVisual">
    /// Type to register the specified <typeparamref name="TViewModel"/> to
    /// resolve to.
    /// </typeparam>
    void Register<TViewModel, TVisual>()
        where TViewModel : IViewModel
        where TVisual : T, new();
}