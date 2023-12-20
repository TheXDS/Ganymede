using TheXDS.Ganymede.CrudGen;
using TheXDS.Triton.Models;

namespace TheXDS.Ivie.CrudGen;

/// <summary>
/// Base class for all descriptors used to describe security objects.
/// </summary>
/// <typeparam name="T">Type of the described model.</typeparam>
public abstract class SecurityObjectDescriptorBase<T> : CrudDescriptor<T> where T : SecurityObject
{
    /// <inheritdoc/>
    protected override sealed void OnDescribeModel(IModelConfigurator<T> config)
    {
        DescribeModel(config);
        config.ConfigureProperties(DescribeProps);
    }

    /// <summary>
    /// Describes the model.
    /// </summary>
    /// <param name="config">
    /// Model configuration instance to use when configuring the model general
    /// settings.
    /// </param>
    protected abstract void DescribeModel(IModelConfigurator<T> config);

    private void DescribeProps(IPropertyConfigurator<T> configurator)
    {
        configurator.Property(p => p.Membership).WidgetSize(WidgetSize.Medium).Creatable();
        configurator.Property(p => p.Descriptors).WidgetSize(WidgetSize.Medium).Creatable();
    }
}
