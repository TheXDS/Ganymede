using System.Reflection;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using Pst = TheXDS.Ganymede.Resources.Strings.ProteusCommon;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements an entity selection dialog ViewModel.
/// </summary>
public abstract class CrudObjectSelectorViewModelBase : AwaitableDialogViewModel
{
    /// <summary>
    /// Describes a selectable property to perform searches over.
    /// </summary>
    /// <param name="Property">Actual property.</param>
    /// <param name="DisplayName">display name for the property.</param>
    public record class SearchPropertyItem(PropertyInfo Property, string DisplayName);

    private Model? entity;
    private IEnumerable<Model>? _Entities;
    private PropertyInfo? _SelectedSearchProperty;
    private string? _SearchQuery;

    /// <summary>
    /// Gets a reference to the description of the model to be selected.
    /// </summary>
    public ICrudDescription Description { get; }

    /// <summary>
    /// Gets a reference to the command used to invoke a search. The search
    /// query should be this command's parameter.
    /// </summary>
    public ICommand SearchCommand { get; }

    /// <summary>
    /// Gets a reference to the command used to close the current search.
    /// </summary>
    public ICommand CloseSearchCommand { get; }

    /// <summary>
    /// Gets a collection of properties available to perform search.
    /// </summary>
    public IEnumerable<SearchPropertyItem> Properties => Description.PropertyDescriptions.Where(IsValidSearchProperty).Select(p => new SearchPropertyItem(p.Key, p.Value.Description.Label));

    /// <summary>
    /// Gets or sets the current search query.
    /// </summary>
    public string? SearchQuery
    {
        get => _SearchQuery;
        set => Change(ref _SearchQuery, value);
    }

    /// <summary>
    /// Gets or sets the selected property used for search.
    /// </summary>
    public PropertyInfo? SelectedSearchProperty
    {
        get => _SelectedSearchProperty;
        set => Change(ref _SelectedSearchProperty, value);
    }

    /// <summary>
    /// Gets or sets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model? Entity
    {
        get => entity;
        set => Change(ref entity, value);
    }

    /// <summary>
    /// Gets the active collection of items to select the entity from.
    /// </summary>
    public IEnumerable<Model>? Entities
    {
        get => _Entities;
        private set => Change(ref _Entities, value);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudObjectSelectorViewModelBase"/> class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="description"/> is null.
    /// </exception>
    protected CrudObjectSelectorViewModelBase(ICrudDescription description)
    {
        Icon = "👆";
        IconBgColor = System.Drawing.Color.SlateGray;
        Title = Pst.SelectItem;
        Message = Pst.SelectItemHelp;
        var cb = new CommandBuilder<CrudObjectSelectorViewModelBase>(this);
        SearchCommand = cb.BuildObserving(OnSearch).CanExecuteIfNotNull(p => SelectedSearchProperty).Build();
        CloseSearchCommand = new SimpleCommand(OnCloseSearch);
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Interactions.Add(new(cb.BuildObserving(CloseDialog).CanExecuteIfNotNull(p => p.Entity).Build(), Pst.Select));
        Interactions.Add(new(new SimpleCommand(OnCancel), St.Cancel));
    }

    /// <summary>
    /// When overriden in a derived class, implements the actual search logic.
    /// </summary>
    /// <param name="query">Search query.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> that can be used to await the async operation,
    /// which returns the search results for the specified query.
    /// </returns>
    protected abstract Task<IEnumerable<Model>> PerformSearchAsync(string? query);

    /// <inheritdoc/>
    protected override async Task OnCreated()
    {
        await OnCloseSearch();
    }

    private async Task OnCloseSearch()
    {
        IsBusy = true;
        SearchQuery = null;
        Entities = await PerformSearchAsync(null);
        IsBusy = false;
    }

    private async Task OnSearch(object? searchTerm)
    {
        if (searchTerm is not string query) return;
        IsBusy = true;
        Entities = await PerformSearchAsync(query);
        IsBusy = false;
    }

    private void OnCancel()
    {
        Entity = null;
        CloseDialog();
    }

    private static bool IsValidSearchProperty(KeyValuePair<PropertyInfo, DescriptionEntry> prop)
    {
        var type = prop.Key.PropertyType;
        return type == typeof(string) || type.IsNumericType() || type == typeof(Guid);
    }
}
