using System;
using System.Windows;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a single resolver entry. Can be used as a
/// <see cref="IVisualResolver{TVisual}"/> directly as well on a
/// <see cref="VisualResolverStack{TVisual}"/> that resolves Views of type
/// <see cref="FrameworkElement"/>.
/// </summary>
public struct ResolverEntry : IVisualResolver<FrameworkElement>
{
    /// <summary>
    /// Gets a reference to the type of ViewModel for which to add a View
    /// resolution registration.
    /// </summary>
    public Type ViewModel { get; set; }

    /// <summary>
    /// Gets a reference to the type of view to be resolved for the specified
    /// ViewModel type.
    /// </summary>
    public Type View { get; set; }

    readonly FrameworkElement? IVisualResolver<FrameworkElement>.Resolve(IViewModel viewModel)
    {
        return viewModel.GetType().IsAssignableTo(ViewModel) ? (FrameworkElement?)Activator.CreateInstance(View) : null;
    }
}
