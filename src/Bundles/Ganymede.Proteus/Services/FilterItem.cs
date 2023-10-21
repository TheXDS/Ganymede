using System.Reflection;
using TheXDS.Ganymede.Services.Base;
namespace TheXDS.Ganymede.Services;

/// <summary>
/// Includes information on query filters that can be applied to an
/// <see cref="IEntityProvider"/> when fetching data.
/// </summary>
/// <param name="Property">Property for which to filter.</param>
/// <param name="Query">Query string.</param>
public record class FilterItem(PropertyInfo Property, string Query);
