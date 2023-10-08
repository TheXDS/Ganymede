using System.Linq.Expressions;
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
    IModelConfigurator<T> SaveProlog(Action<T> prolog);

    /// <summary>
    /// Initiates the configuration of the model properties.
    /// </summary>
    /// <param name="config">Property configuration instance to use.</param>
    /// <returns>This same instance.</returns>
    IModelConfigurator<T> ConfigureProperties(Action<IPropertyConfigurator<T>> config);
}