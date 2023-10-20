using System.Linq.Expressions;
using System.Reflection;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements an entity selection dialog ViewModel bound to a data source.
/// </summary>
public class DataCrudSelectorViewModel : CrudObjectSelectorViewModelBase
{
    private readonly ITritonService _dataService;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DataCrudSelectorViewModel"/> class.
    /// </summary>
    /// <param name="dataService">
    /// Data service to use when fetching entities.
    /// </param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if either <paramref name="dataService"/> or
    /// <paramref name="description"/> is null.
    /// </exception>
    public DataCrudSelectorViewModel(ITritonService dataService, ICrudDescription description) : base(description)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
    }

    /// <inheritdoc/>
    protected override async Task<IEnumerable<Model>> PerformSearchAsync(string? query)
    {
        await using var t = _dataService.GetReadTransaction();
        if (query is null) return await t.All(Description.Model).ToListAsync();
        if (SelectedSearchProperty is null) return Array.Empty<Model>();        
        var filterExpression = GetFilterOnly(Description.Model, out var entExp, query);
        var expression = Expression.Lambda(ToFunc(Description.Model), filterExpression, entExp);
        return await BuildQuery(Description.Model, t, expression).Cast<Model>().ToListAsync();
    }

    private static IQueryable BuildQuery(Type model, ICrudReadTransaction t, LambdaExpression expression)
    {
        var m = typeof(DataCrudSelectorViewModel).GetMethod(nameof(BuildQueryGeneric), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(model);
        var m2 = t.GetType().GetMethod("All", 1, BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null)!.MakeGenericMethod(model);
        object o = m2.Invoke(t, Array.Empty<object>())!;
        return (IQueryable)m.Invoke(null, new object[] { o, expression })!;
    }

    private static IQueryable<T> BuildQueryGeneric<T>(IQueryable<T> all, LambdaExpression expression) where T : Model
    {
        return all.Where((Expression<Func<T, bool>>)expression);
    }

    private static Type ToFunc(Type model)
    {
        return typeof(Func<,>).MakeGenericType(model, typeof(bool));
    }

    private static Expression ToStringExp(Expression expression, Type valType)
    {
        return Expression.Call(expression, valType.GetMethod("ToString", Type.EmptyTypes)!);
    }

    private static Expression ToLower(Expression expression)
    {
        return Expression.Call(expression, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
    }

    private Expression GetFromEntity(Expression source)
    {
        return Expression.Property(source, SelectedSearchProperty!.GetMethod!);
    }

    private Expression GetFilterOnly(Type model, out ParameterExpression entExp, string query)
    {
        entExp = Expression.Parameter(model);
        return Expression.Call(
                ToLower(
                    ToStringExp(
                        GetFromEntity(entExp),
                        SelectedSearchProperty!.PropertyType)),
                typeof(string).GetMethod("Contains", new Type[] { typeof(string) })!,
                Expression.Constant(query.ToLower()));
    }
}
