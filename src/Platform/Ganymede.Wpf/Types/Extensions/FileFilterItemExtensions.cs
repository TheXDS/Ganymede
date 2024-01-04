using System.Collections.Generic;
using System.Linq;
using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Types.Extensions;

/// <summary>
/// Contains a set of extensions for the <see cref="FileFilterItem"/> type and
/// any collection of type <see cref="FileFilterItem"/>.
/// </summary>
public static class FileFilterItemExtensions
{
    /// <summary>
    /// Converts a collection of file filters into a string that can be used
    /// with the standard file dialogs provided by Microsoft Windows.
    /// </summary>
    /// <param name="items">
    /// Collection of items to create the filter string from.
    /// </param>
    /// <returns>
    /// A string that can be used as a file filter with the standard file
    /// dialogs provided by Microsoft Windows.
    /// </returns>
    public static string ToWin32Filter(this IEnumerable<FileFilterItem> items)
    {
        return string.Join("|", items.Select(p => $"{p.Name}|{string.Join(";", p.Extensions)}"));
    }
}
