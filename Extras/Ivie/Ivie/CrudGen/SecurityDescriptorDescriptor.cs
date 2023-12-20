using TheXDS.Ganymede.CrudGen;
using TheXDS.Triton.Models;

namespace TheXDS.Ivie.CrudGen;

/// <summary>
/// Describes entities of type <see cref="SecurityDescriptor"/>.
/// </summary>
public class SecurityDescriptorDescriptor : CrudDescriptor<SecurityDescriptor>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<SecurityDescriptor> config)
    {
        config.ConfigureProperties(DescribeProps);
    }

    private void DescribeProps(IPropertyConfigurator<SecurityDescriptor> configurator)
    {
        configurator.Property(p => p.ContextId).Icon("⚙️");
        configurator.Property(p => p.Granted);
        configurator.Property(p => p.Revoked);
    }
}