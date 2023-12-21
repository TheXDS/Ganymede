using ReactiveUI;

namespace TheXDS.Ganymede.Component.Locators;

/// <summary>
/// Locates Views for a ViewModel by scanning types assignables to the
/// <see cref="IViewFor{T}"/> interface, where the type parameter matches the
/// ViewModel type. 
/// </summary>
public class ReflectionAppViewLocator : TypeScaningViewLocator, IViewLocator
{
    /// <inheritdoc/>
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        Type r = typeof(IViewFor<>).MakeGenericType(viewModel?.GetType() ?? throw new ArgumentNullException(nameof(viewModel)));
        return FindView(q => r.IsAssignableFrom(q));
    }
}