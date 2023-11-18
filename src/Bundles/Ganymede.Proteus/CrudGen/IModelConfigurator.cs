using System.Linq.Expressions;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a set of members to be implemented by a type that esposes model
/// description functions for CRUD generation.
/// </summary>
/// <typeparam name="T">
/// Type of <see cref="Model"/> for which to describe its properties.
/// </typeparam>
public interface IModelConfigurator<T> where T : Model
{
    /// <summary>
    /// Sets the category that this model belongs to.
    /// </summary>
    /// <param name="category">Category to be set on this descriptor.</param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> Category(CrudCategory category);

    /// <summary>
    /// Sets a resource class to use when resolving labels.
    /// </summary>
    /// <typeparam name="TRes">Type of resource class.</typeparam>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> LabelResource<TRes>() where TRes : class;

    /// <summary>
    /// Sets a friendly name for the model.
    /// </summary>
    /// <param name="modelName">Name to use when presenting the model.</param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> FriendlyName(string modelName);

    /// <summary>
    /// Sets a property binding path to use as the friendly name for this
    /// described model.
    /// </summary>
    /// <param name="propertyPath">
    /// Property path to use as a friendly name.
    /// </param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> FriendlyNameBindingPath(string propertyPath);

    /// <summary>
    /// Sets a property binding to use as the friendly name for this described
    /// model.
    /// </summary>
    /// <param name="propertySelector">
    /// Expression used to select the desired friendly name property. It must
    /// be a property directly defined in the model.
    /// </param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> FriendlyNameBindingPath(Expression<Func<T, object?>> propertySelector) => FriendlyNameBindingPath(ReflectionHelpers.GetProperty(propertySelector).Name);

    /// <summary>
    /// Configures a method to invoke whenever the entity gets saved.
    /// </summary>
    /// <param name="prolog">
    /// Method to execute. This will run right before the save operation is
    /// performed.
    /// </param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> AddSaveProlog(Action<T> prolog);

    /// <summary>
    /// Initiates the configuration of the model properties.
    /// </summary>
    /// <param name="config">Property configuration instance to use.</param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> ConfigureProperties(Action<IPropertyConfigurator<T>> config);

    /// <summary>
    /// Specifies a ViewModel to present whenever there is no selected entity.
    /// </summary>
    /// <typeparam name="TViewModel">Type of ViewModel to present.</typeparam>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> DashboardViewModel<TViewModel>() where TViewModel : CrudViewModelBase, new();

    /// <summary>
    /// Specifies a ViewModel to present whenever there is no selected entity.
    /// </summary>
    /// <typeparam name="TViewModel">Type of ViewModel to present.</typeparam>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> DetailsViewModel<TViewModel>() where TViewModel : EntityCrudViewModelBase, new();

    /// <summary>
    /// Specifies the properties to be used to show information on a
    /// ListView-style UI visual element.
    /// </summary>
    /// <param name="propertySelectors">
    /// Expressions that allow for property selection.
    /// </param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> ListViewProperties(params Expression<Func<T, object?>>[] propertySelectors);
}