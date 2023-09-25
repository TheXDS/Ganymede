using ReactiveUI;

namespace TheXDS.Ganymede.Component.Locators;

/// <summary>
/// Base class for all type-scanning implementations of <see cref="IViewLocator"/>.
/// </summary>
public abstract class TypeScaningViewLocator
{
    /// <summary>
    /// Finds a view using the specified predicate function.
    /// </summary>
    /// <param name="condition">
    /// Condition that must be fulfilled by a type to be intanced and returned
    /// as an object of type <see cref="IViewFor"/>.
    /// </param>
    /// <returns>
    /// A new instance of the view type found using the specified predicate, or
    /// <see langword="null"/> if no types match the predicate, or if the type
    /// found could not be cast to <see cref="IViewFor"/>.
    /// </returns>
    protected IViewFor? FindView(Func<Type, bool> condition)
    {
        var vmType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(p => p.GetExportedTypes())
            .FirstOrDefault(condition);
        return vmType is not null ? Activator.CreateInstance(vmType) as IViewFor : null;
    }
}