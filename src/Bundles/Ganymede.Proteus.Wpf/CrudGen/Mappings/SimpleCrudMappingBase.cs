using System.Reflection;
using System.Windows;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.CrudGen.Descriptions;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Base class that defines a simple CRUD mapping to a
/// <see cref="FrameworkElement"/>.
/// </summary>
/// <typeparam name="TControl">
/// Type of <see cref="FrameworkElement"/> to generate.
/// </typeparam>
public abstract class SimpleCrudMappingBase<TControl> : CrudMappingBase, ICrudMapping
    where TControl : FrameworkElement, new()
{
    /// <inheritdoc/>
    public abstract bool CanMap(IPropertyDescription description);

    /// <inheritdoc/>
    public virtual FrameworkElement CreateControl(IPropertyDescription description)
    {
        var c = new TControl();
        ConfigureControl(c, description);
        return c;
    }

    /// <summary>
    /// Configures the generated control.
    /// </summary>
    /// <param name="control">Control to configure.</param>
    /// <param name="description">
    /// CRUD property descriptions for the property.
    /// </param>
    protected abstract void ConfigureControl(TControl control, IPropertyDescription description);
}
