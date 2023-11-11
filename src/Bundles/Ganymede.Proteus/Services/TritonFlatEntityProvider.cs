using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services.Base;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels.CustomDialogs;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements an entity provider bound to a Triton service that fetches data
/// for a single model or a collection of related sibling models that do not
/// belong to a tree structure.
/// </summary>
public class TritonFlatEntityProvider : ViewModelBase, IEntityProvider
{
    private readonly ITritonService _dataService;
    private readonly ObservableCollectionWrap<Model> _results = new();
    private int _page = 1;
    private int _itemsPerPage = 100;
    private int totalItems;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="TritonFlatEntityProvider"/> class.
    /// </summary>
    /// <param name="dataService">
    /// Data service to use when fetching data.
    /// </param>
    /// <param name="models">
    /// Models for which to fetch entities.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="dataService"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Thrown if <paramref name="models"/> does not contain any items.
    /// </exception>
    public TritonFlatEntityProvider(ITritonService dataService, params ICrudDescription[] models)
    {
        RegisterPropertyChangeBroadcast(nameof(ItemsPerPage), nameof(TotalPages));
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));        
        
        Models = models.OrNull()?.ToArray() ?? throw new EmptyCollectionException(models);
        foreach (var model in Models)
        {
            Filters.Add(model, new Filter());
        }
        var b = new CommandBuilder<TritonFlatEntityProvider>(this);

        FirstPageCommand = b.BuildSimple(OnFirst);
        LastPageCommand = b.BuildSimple(OnLast);
        NextPageCommand = b.BuildObserving(OnNextPage).ListensTo(p => p.Results).CanExecute(CanGoNext).Build();
        PreviousPageCommand = b.BuildObserving(OnPreviousPage).ListensTo(p => p.Results).CanExecute(CanGoPrevious).Build();
        RefreshCommand = b.BuildBusyOperation(OnRefresh);
        EditFiltersCommand = b.BuildSimple(OnEditFilters);
        ClearFiltersCommand = b.BuildSimple(OnClearFilters);
    }

    /// <inheritdoc/>
    public ICommand FirstPageCommand { get; }

    /// <inheritdoc/>
    public ICommand LastPageCommand { get; }

    /// <inheritdoc/>
    public ICommand NextPageCommand { get; }

    /// <inheritdoc/>
    public ICommand PreviousPageCommand { get; }

    /// <inheritdoc/>
    public ICommand RefreshCommand { get; }

    /// <inheritdoc/>
    public ICommand EditFiltersCommand { get; }

    /// <inheritdoc/>
    public ICommand ClearFiltersCommand { get; }

    /// <inheritdoc/>
    public IDictionary<ICrudDescription, Filter> Filters { get; } = new Dictionary<ICrudDescription, Filter>();

    /// <inheritdoc/>
    public int FiltersCount => Filters.Sum(p => p.Value.Items.Count) + Filters.Count(p => p.Value.Exclude);

    /// <inheritdoc/>
    public IEnumerable<Model> Results => _results;

    /// <summary>
    /// Gets a reference to a dialog service that can be used to block the UI
    /// while the service is busy fetching data.
    /// </summary>
    public IDialogService? DialogService { get; set; }

    /// <inheritdoc/>
    public int TotalItems
    {
        get => totalItems;
        private set => Change(ref totalItems, value);
    }

    /// <inheritdoc/>
    public int Page
    {
        get => _page;
        set
        {
            var total = ((IEntityProvider)this).TotalPages;
            if (value < 1 || (total > 0 && value > total)) throw new ArgumentOutOfRangeException(nameof(value));
            if (Change(ref _page, value)) FetchDataAsync();
        }
    }

    /// <inheritdoc/>
    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (Change(ref _itemsPerPage, value))
            {
                _page = 1;
                Notify(nameof(Page));
                FetchDataAsync();
            }
        }
    }

    /// <inheritdoc/>
    public int TotalPages => ItemsPerPage > 0 ? (int)Math.Ceiling(TotalItems / (float)ItemsPerPage) : 1;

    /// <inheritdoc/>
    public ICrudDescription[] Models { get; }

    bool IViewModel.IsBusy { get => IsBusy; set => IsBusy = value; }

    /// <inheritdoc/>
    public Task FetchDataAsync()
    {
        return DialogService?.RunOperation(OnRefresh) ?? OnRefresh(null);
    }

    private async Task OnRefresh(IProgress<ProgressReport>? status)
    {
        status?.Report(St.FetchingData);
        await using var t = _dataService.GetReadTransaction();
        List<Model> tempResults = new();
        int totalItems = 0;
        foreach (var j in Models)
        {
            var f = Filters[j];
            if (f.Exclude) continue;
            var query = BuildQuery(j.Model, t, f);
            totalItems += query.Count();
            tempResults.AddRange(await query.Skip((Page - 1) * (ItemsPerPage / Models.Length)).Take(ItemsPerPage).ToListAsync());
        }
        TotalItems = totalItems;
        _results.Substitute(tempResults);
        Notify(nameof(Results));
    }
    
    private async Task OnEditFilters()
    {
        var vm = new FilterEditorDialogViewModel(Filters);
        await (DialogService?.CustomDialog(vm) ?? Task.CompletedTask);
        Notify(nameof(FiltersCount));
        await FetchDataAsync();
    }

    private Task OnClearFilters()
    {
        foreach( var j in Filters.Values)
        {
            j.Exclude = false;
            j.AggregateWithOr = false;
            j.Items.Clear();
        }

        Notify(nameof(FiltersCount));
        return FetchDataAsync();
    }

    private Task OnFirst()
    {
        Page = 1;
        return FetchDataAsync();
    }

    private Task OnNextPage()
    {
        Page++;
        return FetchDataAsync();
    }

    private Task OnPreviousPage()
    {
        Page--;
        return FetchDataAsync();
    }

    private Task OnLast()
    {
        Page = ((IEntityProvider)this).TotalPages;
        return FetchDataAsync();
    }

    private bool CanGoNext()
    {
        return Page < ((IEntityProvider)this).TotalPages;
    }

    private bool CanGoPrevious()
    {
        return Page > 1;
    }

    private static IQueryable<Model> BuildQuery(Type model, ICrudReadTransaction t, Filter filter)
    {
        var expression = ToLambda(model, filter.Items.Where(IsValid).ToArray(), filter.AggregateWithOr ? Expression.OrElse : Expression.AndAlso);
        return (IQueryable<Model>)typeof(TritonFlatEntityProvider)
            .GetMethod(nameof(BuildQueryGeneric), BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(model)
            .Invoke(null, new object?[] { t, expression })!;
    }

    private static bool IsValid(FilterItem item)
    {
        return item.Property is not null && !item.Query.IsEmpty();
    }

    private static LambdaExpression? ToLambda(Type model, FilterItem[] items, Func<Expression, Expression, BinaryExpression> aggregator)
    {
        if (!items.Any()) return null;
        var entExp = Expression.Parameter(model);
        return Expression.Lambda(ToFunc(model), items.Aggregate((Expression?)null,
            (p, q) => p is not null ? aggregator.Invoke(p, GetFilter(entExp, q)) : GetFilter(entExp, q))!,
            entExp);
    }

    private static IQueryable<T> BuildQueryGeneric<T>(ICrudReadTransaction t, LambdaExpression? expression)
        where T : Model
    {
        var q = (IQueryable<T>)t.All<T>();
        if (expression is not null) q = q.Where((Expression<Func<T, bool>>)expression);
        return q;
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
        return Expression.Call(expression, ReflectionHelpers.GetMethod<string, Func<string>>(p => p.ToLower));
    }

    private static Expression GetFromEntity(Expression source, PropertyInfo property)
    {
        return Expression.Property(source, property.GetMethod!);
    }

    private static Expression GetFilter(ParameterExpression entExp, FilterItem query)
    {
        var prop = query.Property!.Property;
        return prop.PropertyType.IsClass
            ? Expression.AndAlso(Expression.NotEqual(GetFromEntity(entExp, prop), Expression.Constant(null)), Contains(entExp, prop, query.Query!))
            : Contains(entExp, prop, query.Query!);
    }

    private static Expression Contains(ParameterExpression entExp, PropertyInfo prop, string query)
    {
        return Expression.Call(
            ToLower(ToStringExp(GetFromEntity(entExp, prop), prop.PropertyType)),
            ReflectionHelpers.GetMethod<string, Func<string, bool>>(p => p.Contains),
            Expression.Constant(query.ToLower()));
    }
}
