using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services.Base;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements an entity provider bound to a Triton service.
/// </summary>
public class TritonEntityProvider : ViewModelBase, IEntityProvider
{
    private readonly ITritonService _dataService;
    private readonly Type _model;
    private readonly ObservableCollectionWrap<Model> _results = new();
    private int _page = 1;
    private int _itemsPerPage = 100;
    private int totalItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="TritonEntityProvider"/>
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
    public TritonEntityProvider(ITritonService dataService, Type model)
    {
        RegisterPropertyChangeBroadcast(nameof(ItemsPerPage), nameof(TotalPages));

        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        _model = model ?? throw new ArgumentNullException(nameof(model));
        var b = new CommandBuilder<TritonEntityProvider>(this);

        FirstPageCommand = b.BuildSimple(OnFirst);
        LastPageCommand = b.BuildSimple(OnLast);
        NextPageCommand = b.BuildObserving(OnNextPage).ListensTo(p => p.Results).CanExecute(CanGoNext).Build();
        PreviousPageCommand = b.BuildObserving(OnPreviousPage).ListensTo(p => p.Results).CanExecute(CanGoPrevious).Build();
        RefreshCommand = b.BuildBusyOperation(OnRefresh);
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
    public ICollection<FilterItem> Filters { get; } = new ObservableCollectionWrap<FilterItem>(new List<FilterItem>());

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
        var query = BuildQuery(_model, t, Filters.Select(ToLambda));
        TotalItems = query.Count();
        _results.Substitute(await query.Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToListAsync());
        Notify(nameof(Results));
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

    private LambdaExpression ToLambda(FilterItem item)
    {
        return Expression.Lambda(ToFunc(_model), GetFilterOnly(_model, out var entExp, item), entExp);
    }

    private static IQueryable<Model> BuildQuery(Type model, ICrudReadTransaction t, IEnumerable<LambdaExpression> expressions)
    {
        IQueryable<Model> o = t.All(model);
        return expressions.Aggregate(o, (p, q) => p.Where((Expression<Func<Model, bool>>)q));
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
