using TheXDS.Ganymede.Helpers;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Base class for all type-scanning implementations of <see cref="IVisualResolver{TVisual}"/>.
/// </summary>
public abstract class TypeScaningVisualResolver<TVisual> where TVisual : new()
{
    /// <summary>
    /// Finds a view using the specified predicate function.
    /// </summary>
    /// <param name="condition">
    /// Condition that must be fulfilled by a type to be intanced and returned
    /// as an object of type <typeparamref name="TVisual"/>.
    /// </param>
    /// <returns>
    /// A new instance of the view type found using the specified predicate, or
    /// <see langword="null"/> in case that either of the following is true:
    /// <list type="bullet">
    /// <item>No types match the predicate</item>
    /// <item>
    /// The type could not be cast to <typeparamref name="TVisual"/>
    /// </item>
    /// <item>The type did not have a parameterless constructor</item>
    /// <item>An exception was thrown by the type's constructor</item>
    /// </list>
    /// </returns>
    protected TVisual? FindView(Func<Type, bool> condition)
    {
        var vmType = PublicTypes().FirstOrDefault(condition);
        return UiThread.Invoke(() => vmType is not null && vmType.TryInstance<TVisual>(out var visual) ? visual : default);
    }
}