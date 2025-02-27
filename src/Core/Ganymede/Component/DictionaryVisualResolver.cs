﻿using System.Diagnostics.CodeAnalysis;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that requires explicit
/// View/ViewModel registration for proper Visual resolution.
/// </summary>
/// <typeparam name="T">
/// Type of visual to be registered and resolved by this class.
/// </typeparam>
public class DictionaryVisualResolver<T> : IVisualResolver<T>, IViewModelToViewRegistry<T>
{
    /// <summary>
    /// Gets a reference to the underlying dictionary that contains
    /// ViewModel-to-View registrations.
    /// </summary>
    protected readonly Dictionary<Type, Type> Registry = [];

    /// <inheritdoc/>
    public virtual T? Resolve(IViewModel viewModel)
    {
        return Registry.TryGetValue(viewModel.GetType(), out var visual)
            ? UiThread.Invoke(() => (T)Activator.CreateInstance(visual)!)
            : default!;
    }

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
    public void Register<TViewModel, TVisual>() where TViewModel : IViewModel where TVisual : T, new()
    {
        Registry.Add(typeof(TViewModel), typeof(TVisual));
    }

    /// <summary>
    /// Creates and initializes a new instance of the required visual.
    /// </summary>
    /// <param name="visualType">
    /// ViewModel that the visual type was resolved for.
    /// </param>
    /// <param name="viewModel">
    /// Resolved visual type to initialize.
    /// </param>
    /// <returns>
    /// A new instance of the resolved visual type.
    /// </returns>
    protected virtual T CreateVisual([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]Type visualType, IViewModel viewModel)
    {
        return (T)Activator.CreateInstance(visualType)!;
    }
}