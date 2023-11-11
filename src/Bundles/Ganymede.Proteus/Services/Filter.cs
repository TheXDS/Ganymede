using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Represents a model filter, including any property query filters and general
/// configuration of the query for the specified model.
/// </summary>
public class Filter : NotifyPropertyChanged
{
    private bool _exclude;
    private bool _or;

    /// <summary>
    /// Initializes a new instance of the <see cref="Filter"/> class.
    /// </summary>
    public Filter()
    {
        RegisterPropertyChangeBroadcast(nameof(AggregateWithOr), nameof(AggregateWithAnd));
    }

    /// <summary>
    /// Gets a reference to the collection of filter items that will be applied
    /// when fetching etities of the specified model.
    /// </summary>
    public ICollection<FilterItem> Items { get; } = new ObservableCollectionWrap<FilterItem>(new List<FilterItem>());

    /// <summary>
    /// Gets or sets a value that indicates if all entities of the specified
    /// model should be excluded from the results.
    /// </summary>
    public bool Exclude
    { 
        get => _exclude;
        set => Change(ref _exclude, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates if the filters should be aggregated
    /// using an "AND" expression.
    /// </summary>
    public bool AggregateWithOr
    {
        get => _or;
        set => Change(ref _or, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates if the filters should be aggregated
    /// using an "OR" expression.
    /// </summary>
    public bool AggregateWithAnd
    {
        get => !_or;
        set => Change(ref _or, !value);
    }
}
