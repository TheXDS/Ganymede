using System.Reflection;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Services.Base;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Includes information on query filters that can be applied to an
/// <see cref="IEntityProvider"/> when fetching data.
/// </summary>
public class FilterItem : NotifyPropertyChanged
{
    private IPropertyDescription? property;
    private string? query;

    /// <summary>
    /// Property for which to filter.
    /// </summary>
    public IPropertyDescription? Property
    {
        get => property;
        set
        {
            if (value is not null) Change(ref property, value);
        }
    }

    /// <summary>
    /// Query string.
    /// </summary>
    public string? Query
    { 
        get => query;
        set => Change(ref query, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterItem"/> class.
    /// </summary>
    /// <param name="property">Property for which to filter.</param>
    /// <param name="query">Query string.</param>
    public FilterItem(IPropertyDescription? property, string? query)
    {
        this.property = property;
        this.query = query;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterItem"/> class.
    /// </summary>
    public FilterItem() : this(null, null)
    {
    }
}
