using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Resolves views by naming convention, resolving "XClassViewModel" to a view of
/// type "XClassView".
/// </summary>
/// <typeparam name="TVisual">
/// Type of visual container resolution to implement.
/// </typeparam>
public class ConventionVisualResolver<TVisual> : TypeScaningVisualResolver<TVisual>, IVisualResolver<TVisual> where TVisual : new()
{
    private static string GetName(ViewModelBase viewModel)
    {
        return (viewModel ?? throw new ArgumentNullException(nameof(viewModel)))
            .GetType().Name.Replace("ViewModel", "View");
    }

    /// <inheritdoc/>
    public TVisual? Resolve(ViewModelBase viewModel)
    {
        var name = GetName(viewModel);
        return FindView(p => p.Name == name);
    }
}
