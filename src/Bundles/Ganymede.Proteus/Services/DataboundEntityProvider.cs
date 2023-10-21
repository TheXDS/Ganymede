using System.Linq.Expressions;
using System.Reflection;
using TheXDS.Ganymede.Services.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements an entity provider bound to a Triton service.
/// </summary>
public class DataboundEntityProvider : IEntityProvider
{
    private readonly ITritonService _dataService;
    private readonly Type _model;
    private int _page = 1;
    private int _itemsPerPage = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataboundEntityProvider"/>
    /// class.
    /// </summary>
    /// <param name="dataService">
    /// Data service to use when fetching data.
    /// </param>
    /// <param name="model">
    /// Model for which to fetch entities.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if either <paramref name="dataService"/> or
    /// <paramref name="model"/> are <see langword="null"/>.
    /// </exception>
    public DataboundEntityProvider(ITritonService dataService, Type model)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        _model = model ?? throw new ArgumentNullException(nameof(model));
    }

    /// <inheritdoc/>
    public IEnumerable<Model> Results { get; private set; } = Enumerable.Empty<Model>();

    /// <inheritdoc/>
    public int TotalItems { get; private set; }

    /// <inheritdoc/>
    public int Page
    {
        get => _page;
        set
        {
            var total = ((IEntityProvider)this).TotalPages;
            if (value < 1 || (total > 0 && value > total)) throw new ArgumentOutOfRangeException(nameof(value));
            _page = value;
        }
    }

    /// <inheritdoc/>
    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _itemsPerPage = value;
        }
    }

    /// <inheritdoc/>
    public ICollection<FilterItem> Filters { get; } = new List<FilterItem>();

    /// <inheritdoc/>
    public async Task FetchDataAsync()
    {
        Page = 1;
        await using var t = _dataService.GetReadTransaction();
        var query = BuildQuery(_model, t, Filters.Select(ToLambda)).Cast<Model>();
        TotalItems = query.Count();
        Results = await query.Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToListAsync();
    }

    private LambdaExpression ToLambda(FilterItem item)
    {
        return Expression.Lambda(ToFunc(_model), GetFilterOnly(_model, out var entExp, item), entExp);
    }

    private static IQueryable BuildQuery(Type model, ICrudReadTransaction t, IEnumerable<LambdaExpression> expressions)
    {
        object o = GetTritonAllMethod(t, model).Invoke(t, Array.Empty<object>())!;
        return (IQueryable)GetQueryMethod(model).Invoke(null, new object[] { o, expressions })!;
    }

    private static IQueryable<T> BuildQueryGeneric<T>(IQueryable<T> all, IEnumerable<LambdaExpression> expressions) where T : Model
    {
        return expressions.Aggregate(all, (p, q) => p.Where((Expression<Func<T, bool>>)q));
    }

    private static MethodInfo GetQueryMethod(Type model)
    {
        return typeof(DataboundEntityProvider).GetMethod(nameof(BuildQueryGeneric), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(model);
    }

    private static MethodInfo GetTritonAllMethod(ICrudReadTransaction t, Type model)
    {
        return t.GetType().GetMethod("All", 1, BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null)!.MakeGenericMethod(model);
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

    private static Expression GetFromEntity(Expression source, PropertyInfo property)
    {
        return Expression.Property(source, property.GetMethod!);
    }

    private static Expression GetFilterOnly(Type model, out ParameterExpression entExp, FilterItem query)
    {
        entExp = Expression.Parameter(model);
        return Expression.Call(
                ToLower(
                    ToStringExp(
                        GetFromEntity(entExp, query.Property),
                        query.Property!.PropertyType)),
                typeof(string).GetMethod("Contains", new Type[] { typeof(string) })!,
                Expression.Constant(query.Query.ToLower()));
    }
}
