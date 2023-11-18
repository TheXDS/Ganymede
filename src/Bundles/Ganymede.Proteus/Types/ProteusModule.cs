using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.Ganymede.CrudGen;

namespace TheXDS.Ganymede.Types;

public abstract class ProteusModule
{
    private ModuleItem[] _items;

    public IEnumerable<IGrouping<CrudCategory, ICrudDescription>> MenuItems
        => _items
        .SelectMany(p => p.Descriptions)
        .Where(p => p.Category.HasValue)
        .GroupBy(p => p.Category!.Value);
}

public class ModuleItem
{
    public ICrudDescription[] Descriptions { get; }
} 