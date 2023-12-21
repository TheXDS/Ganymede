using ReactiveUI;

namespace TheXDS.Ganymede.Component.Locators;

/// <summary>
/// Locates views by naming convention, resolving XClassViewModel to a view of
/// type XClassView.
/// </summary>
public class ConventionViewLocator : TypeScaningViewLocator, IViewLocator
{
    /// <inheritdoc/>
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        string name = GetName(viewModel);
        return FindView(p => p.Name == name);
    }
        
    private static string GetName<T>(T viewModel)
    {
        return (viewModel ?? throw new ArgumentNullException(nameof(viewModel)))
            .GetType().Name.Replace("ViewModel", "View");
    }
}