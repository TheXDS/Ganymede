using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Services.Base;

/// <summary>
/// Defines a set of members to be implemented by a type that provides query
/// and search services for collections of entities.
/// </summary>
public interface IEntityProvider
{
    /// <summary>
    /// Gets the current query results.
    /// </summary>
    /// <remarks>
    /// This collection will be populated whenever the
    /// <see cref="RefreshResultsAsync"/> method is invoked.
    /// </remarks>
    IEnumerable<Model> Results { get; }
    
    /// <summary>
    /// Gets the total number of items that exists in the query.
    /// </summary>
    int TotalItems { get; }

    /// <summary>
    /// Gets or sets the desired page number to put into the
    /// <see cref="Results"/> collection.
    /// </summary>
    int Page { get; set; }
    
    /// <summary>
    /// Gets or sets the number of items per page to put into the
    /// <see cref="Results"/> collection.
    /// </summary>
    int ItemsPerPage { get; set; }

    /// <summary>
    /// Gets the total number of pages that can be returned into the
    /// <see cref="Results"/> collection.
    /// </summary>
    int TotalPages => ItemsPerPage > 0 ? (int)Math.Ceiling(TotalItems / (float)ItemsPerPage) : 1;

    /// <summary>
    /// Gets a collection of search filters that can be applied to the query.
    /// </summary>
    ICollection<FilterItem> Filters { get; }

    /// <summary>
    /// Creates the internal query used by this instance to generate the       
    /// results and executes it.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the async operation.
    /// </returns>
    Task FetchDataAsync();
}
