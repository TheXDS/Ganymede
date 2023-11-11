using System.Reflection;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Services;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.ViewModels.CustomDialogs;

/// <summary>
/// Represents a Filter editor item for a specific model.
/// </summary>
public class FilterEditorItem
{

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterEditorItem"/> class.
    /// </summary>
    /// <param name="description">Description to bind to this instance.</param>
    /// <param name="filter">
    /// Filter collection to manage for the specific model description.
    /// </param>
    public FilterEditorItem(ICrudDescription description, Filter filter)
    {
        Description = description;
        Filter = filter;
        Properties = GetFilterableProperties(description);
        ClearFiltersCommand = new SimpleCommand(Filter.Items.Clear);
        AddFilterCommand = new SimpleCommand(() => Filter.Items.Add(new FilterItem()));
        RemoveFilterCommand = new SimpleCommand(OnRemove);
    }

    /// <summary>
    /// Gets a reference to the model description this instance represents.
    /// </summary>
    public ICrudDescription Description { get; }

    /// <summary>
    /// Gets a collection of model properties for which filters can be created.
    /// </summary>
    public IEnumerable<IPropertyDescription> Properties { get; }

    /// <summary>
    /// Gets a reference to the command used to clear the filter collection.
    /// </summary>
    public ICommand ClearFiltersCommand { get; }
    
    /// <summary>
    /// Gets a reference to the command used to add a new empty filter to the
    /// collection.
    /// </summary>
    public ICommand AddFilterCommand { get; }

    /// <summary>
    /// Gets a reference to the command used to remove a filter from the
    /// collection.
    /// </summary>
    public ICommand RemoveFilterCommand { get; }

    /// <summary>
    /// Gets a reference to the <see cref="Services.Filter"/> instance being
    /// managed by this instance.
    /// </summary>
    public Filter Filter { get; }

    private void OnRemove(object? commandParameter)
    {
        if (commandParameter is FilterItem item)
        {
            Filter.Items.Remove(item);
        }
    }

    private static IPropertyDescription[] GetFilterableProperties(ICrudDescription description)
    {
        return description.PropertyDescriptions.Where(IsValidSearchProperty).Select(p => p.Value.Description).ToArray();
    }

    private static bool IsValidSearchProperty(KeyValuePair<PropertyInfo, DescriptionEntry> prop)
    {
        var type = prop.Key.PropertyType;
        return type == typeof(string) || type.IsNumericType() || type == typeof(Guid);
    }
}
