using TheXDS.Triton.Models;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Describes entities of type <see cref="UserGroupMembership"/>.
/// </summary>
public class UserGroupMembershipDescriptor : CrudDescriptor<UserGroupMembership>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<UserGroupMembership> config)
    {
        config.ConfigureProperties(DescribeProps);
    }

    private void DescribeProps(IPropertyConfigurator<UserGroupMembership> configurator)
    {
        configurator.Property(p => p.Group).Selectable();
        configurator.Property(p => p.SecurityObject).Selectable();
    }
}
