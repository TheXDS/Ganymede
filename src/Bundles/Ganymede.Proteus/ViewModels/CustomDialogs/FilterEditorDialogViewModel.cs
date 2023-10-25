using System.Drawing;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.Services;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.ViewModels.CustomDialogs;

/// <summary>
/// Implements a custom dialog that allows the user to edit a collection of
/// <see cref="FilterItem"/>.
/// </summary>
public class FilterEditorDialogViewModel : AwaitableDialogViewModel
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="FilterEditorDialogViewModel"/> class.
    /// </summary>
    /// <param name="filterCollection">
    /// Collection of filters to be managed.
    /// </param>
    /// <param name="properties">
    /// Collection of properties to add filters for.
    /// </param>
    public FilterEditorDialogViewModel(ICollection<FilterItem> filterCollection, IEnumerable<IPropertyDescription> properties)
    {
        FilterCollection = filterCollection;
        Properties = properties;
        ClearFiltersCommand = new SimpleCommand(filterCollection.Clear);
        AddFilterCommand = new SimpleCommand(() => filterCollection.Add(new FilterItem()));
        RemoveFilterCommand = new SimpleCommand(OnRemove);
        Interactions.Add(new(new SimpleCommand(OnClose), Common.Ok));
        Title = "Filter";
        IconBgColor = Color.Chocolate;
        Icon = "⛉";
    }

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
    /// Gets a reference to the filter collection being managed by this dialog.
    /// </summary>
    public ICollection<FilterItem> FilterCollection { get; }

    /// <summary>
    /// Enumerates all the properties that can be added to the filter collection.
    /// </summary>
    public IEnumerable<IPropertyDescription> Properties { get; }

    private void OnClose()
    {
        FilterCollection.RemoveAll(p => p.Property is null || p.Query.IsEmpty());
        CloseDialog();
    }

    private void OnRemove(object? commandParameter)
    {
        if (commandParameter is FilterItem item)
        {
            FilterCollection.Remove(item);
        }
    }
}
