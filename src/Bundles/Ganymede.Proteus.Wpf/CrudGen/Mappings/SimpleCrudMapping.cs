using System;
using System.Reflection;
using System.Windows;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Base class for simple <see cref="FrameworkElement"/> mappings that
/// automatically bind the control's value property, requiring minimal
/// configuration by any derivate types.
/// </summary>
/// <typeparam name="TProp">Property type to map.</typeparam>
/// <typeparam name="TControl">
/// Type of <see cref="FrameworkElement"/> to generate.
/// </typeparam>
public class SimpleCrudMapping<TProp, TControl> : SimpleCrudMappingBase<TProp, TControl>
    where TControl : FrameworkElement, new()
{
    private readonly DependencyProperty valueProperty;
    private readonly Action<TControl, PropertyInfo, IPropertyDescription>? configureControlCallback;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="SimpleCrudMapping{TProp, TControl}"/> class, specifying the
    /// <see cref="DependencyProperty"/> associated with the control's value.
    /// </summary>
    /// <param name="valueProperty">
    /// <see cref="DependencyProperty"/> associated with the control's value.
    /// </param>
    /// <param name="configureControlCallback">
    /// Callback used to configure the generated control.
    /// </param>
    public SimpleCrudMapping(DependencyProperty valueProperty, Action<TControl, PropertyInfo, IPropertyDescription>? configureControlCallback = null)
    {
        this.valueProperty = valueProperty;
        this.configureControlCallback = configureControlCallback;
    }

    /// <inheritdoc/>
    public override FrameworkElement CreateControl(IPropertyDescription description)
    {
        var c = base.CreateControl(description);
        c.SetBinding(valueProperty, $"{nameof(CrudViewModelBase.Entity)}.{description.Property.Name}");
        return c;
    }

    /// <summary>
    /// When overridden in a derivate class, allows to further configure the
    /// generated <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="control">
    /// <see cref="FrameworkElement"/> to configure.
    /// </param>
    /// <param name="description">CRUD property descriptions to use.</param>
    protected override void ConfigureControl(TControl control, IPropertyDescription description)
    {
        configureControlCallback?.Invoke(control, description.Property, description);
    }
}