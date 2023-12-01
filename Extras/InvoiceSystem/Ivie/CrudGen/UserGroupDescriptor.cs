using TheXDS.Triton.Models;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Describes entities of type <see cref="UserGroup"/>.
/// </summary>
public class UserGroupDescriptor : SecurityObjectDescriptorBase<UserGroup>
{
    /// <inheritdoc/>
    protected override void DescribeModel(IModelConfigurator<UserGroup> config)
    {
        config.ConfigureProperties(DescribeProps);
        config.ListViewProperties(p => p.DisplayName);
    }

    private void DescribeProps(IPropertyConfigurator<UserGroup> configurator)
    {
        configurator.Property(p => p.DisplayName);
    }
}
