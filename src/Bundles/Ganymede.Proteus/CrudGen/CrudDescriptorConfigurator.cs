﻿using System.Reflection;
using TheXDS.Ganymede.Helpers;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Implements the model configuration logic for CRUD generation.
/// </summary>
/// <typeparam name="T">Model to configure.</typeparam>
public class CrudDescriptorConfigurator<T> : ICrudDescription, IModelConfigurator<T> where T : Model
{
    private readonly Dictionary<PropertyInfo, DescriptionEntry> _properties;
    private readonly IPropertyConfigurator<T> _propertyConfigurator;
    private readonly List<Action<Model>> _savePrologs = new();
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudDescriptorConfigurator{T}"/> class.
    /// </summary>
    public CrudDescriptorConfigurator()
    {
        FriendlyName = ResourceType.GetLabel($"{typeof(T).Name}{(ResourceType is not null && ResourceType.Name == typeof(T).Name ? "Model" : null)}");
        _properties = new();
        _propertyConfigurator = new PropertyDescriptorConfigurator<T>(_properties, this);
    }

    /// <summary>
    /// Enumerates the described properties for the model.
    /// </summary>
    public IReadOnlyDictionary<PropertyInfo, DescriptionEntry> PropertyDescriptions => _properties;

    /// <summary>
    /// Gets a reference to the described model type.
    /// </summary>
    public Type Model => typeof(T);

    /// <inheritdoc/>
    public string? FriendlyNameBindingPath { get; private set; }

    /// <inheritdoc/>
    public string FriendlyName { get; private set; }

    /// <inheritdoc/>
    public IEnumerable<Action<Model>> SavePrologs => _savePrologs;

    /// <inheritdoc/>
    public Type? ResourceType { get; private set; }

    IModelConfigurator<T> IModelConfigurator<T>.FriendlyName(string friendlyName)
    {
        FriendlyName = friendlyName;
        return this;
    }

    IModelConfigurator<T> IModelConfigurator<T>.FriendlyNameBindingPath(string propertyPath)
    {
        FriendlyNameBindingPath = propertyPath;
        return this;
    }

    IModelConfigurator<T> IModelConfigurator<T>.AddSaveProlog(Action<T> prolog)
    {
        _savePrologs.Add(m => prolog.Invoke((T)m));
        return this;
    }

    IModelConfigurator<T> IModelConfigurator<T>.ConfigureProperties(Action<IPropertyConfigurator<T>> config)
    {
        config.Invoke(_propertyConfigurator);
        return this;
    }

    IModelConfigurator<T> IModelConfigurator<T>.LabelResource<TRes>()
    {
        ResourceType = typeof(TRes);
        return this;
    }
}