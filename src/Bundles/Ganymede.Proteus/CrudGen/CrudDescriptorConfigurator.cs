using System.Reflection;
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

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CrudDescriptorConfigurator{T}"/> class.
    /// </summary>
    public CrudDescriptorConfigurator()
    {
        _properties = new();
        _propertyConfigurator = new PropertyDescriptorConfigurator<T>(_properties);
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
    public string FriendlyName { get; private set; } = SplitByUppercase(typeof(T).Name);

    /// <inheritdoc/>
    public Action<Model>? SaveProlog { get; private set; }

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

    IModelConfigurator<T> IModelConfigurator<T>.SaveProlog(Action<T> prolog)
    {
        SaveProlog = m => prolog.Invoke((T)m);
        return this;
    }

    IModelConfigurator<T> IModelConfigurator<T>.ConfigureProperties(Action<IPropertyConfigurator<T>> config)
    {
        config.Invoke(_propertyConfigurator);
        return this;
    }

    private static string SplitByUppercase(string name)
    {
        var sb = new System.Text.StringBuilder();
        foreach (char c in name)
        {
            if (char.IsUpper(c))
            {
                sb.Append(' ');
            }
            sb.Append(c);
        }
        return sb.ToString().TrimStart();
    }
}
