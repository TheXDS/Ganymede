using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Services;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements an entity selection dialog ViewModel bound to a data source.
/// </summary>
public class DataCrudSelectorViewModel : CrudObjectSelectorViewModelBase
{
    private readonly DataboundEntityProvider _queryService;

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
        _queryService = new(dataService, Description.Model);
    }

    /// <inheritdoc/>
    protected override async Task<IEnumerable<Model>> PerformSearchAsync(string? query)
    {
        _queryService.Filters.Clear();
        if (query is not null && SelectedSearchProperty is not null)
        {
            _queryService.Filters.Add(new(SelectedSearchProperty, query));
        }
        await _queryService.FetchDataAsync();
        return _queryService.Results;
    }
}
