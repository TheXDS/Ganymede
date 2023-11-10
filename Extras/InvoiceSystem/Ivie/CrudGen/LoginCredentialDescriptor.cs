using TheXDS.Ganymede.Models;
using TheXDS.Triton.Models;

namespace TheXDS.Ganymede.CrudGen;

public class EmployeeDescriptor : CrudDescriptor<Employee>
{
    protected override void OnDescribeModel(IModelConfigurator<Employee> config)
    {
        config.ConfigureProperties(m => {
            m.Property(p => p.DisplayName);

        });
    }
}

public class CustomerDescriptor : CrudDescriptor<Customer>
{
    protected override void OnDescribeModel(IModelConfigurator<Customer> config)
    {
        config.ConfigureProperties(m => {
            m.Property(p => p.Name);
        });
    }
}

public class ProductDescriptor : CrudDescriptor<Product>
{
    protected override void OnDescribeModel(IModelConfigurator<Product> config)
    {
        config.ConfigureProperties(OnDescribeProperties);
    }

    private void OnDescribeProperties(IPropertyConfigurator<Product> configurator)
    {
        configurator.Property(p => p.Description);
    }
}

public class LoginCredentialDescriptor : SecurityObjectDescriptorBase<LoginCredential>
{
    protected override void OnDescribeModel(IModelConfigurator<LoginCredential> config)
    {
        base.OnDescribeModel(config);
        config.ConfigureProperties(DescribeProps);
    }

    private void DescribeProps(IPropertyConfigurator<LoginCredential> configurator)
    {
        configurator.Property(p => p.Username);
        configurator.Property(p => p.PasswordHash).Password().Pbkdf2();
        configurator.Property(p => p.Enabled);
    }
}

public class UserGroupMembershipDescriptor : CrudDescriptor<UserGroupMembership>
{
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

public class UserGroupDescriptor : SecurityObjectDescriptorBase<UserGroup>
{
    protected override void OnDescribeModel(IModelConfigurator<UserGroup> config)
    {
        base.OnDescribeModel(config);
        config.ConfigureProperties(DescribeProps);
    }

    private void DescribeProps(IPropertyConfigurator<UserGroup> configurator)
    {
        configurator.Property(p => p.DisplayName);
    }
}

public abstract class SecurityObjectDescriptorBase<T> : CrudDescriptor<T> where T: SecurityObject
{
    protected override void OnDescribeModel(IModelConfigurator<T> config)
    {
        config.ConfigureProperties(DescribeProps);
    }

    private void DescribeProps(IPropertyConfigurator<T> configurator)
    {
        configurator.Property(p => p.Membership).Creatable();
        configurator.Property(p => p.Descriptors).Creatable();
    }
}

public class SecurityDescriptorDescriptor : CrudDescriptor<SecurityDescriptor>
{
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