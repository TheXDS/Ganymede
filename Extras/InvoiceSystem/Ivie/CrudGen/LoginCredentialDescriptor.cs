using TheXDS.Ganymede.Models;
using TheXDS.Triton.Models;

namespace TheXDS.Ganymede.CrudGen;

public class EmployeeDescriptor : CrudDescriptor<Employee>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Employee> config)
    {
        config.ConfigureProperties(m => {
            m.Property(p => p.DisplayName);
        });
    }
}

public class CustomerDescriptor : CrudDescriptor<Customer>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Customer> config)
    {
        config.ConfigureProperties(m => {
            m.Property(p => p.Name);
        });
    }
}

public class ProductDescriptor : CrudDescriptor<Product>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Product> config)
    {
        config.ConfigureProperties(OnDescribeProperties);
    }

    private void OnDescribeProperties(IPropertyConfigurator<Product> configurator)
    {
        configurator.Property(p => p.Description);
    }
}

/// <summary>
/// Describes entities of type <see cref="LoginCredential"/>.
/// </summary>
public class LoginCredentialDescriptor : SecurityObjectDescriptorBase<LoginCredential>
{
    /// <inheritdoc/>
    protected override void DescribeModel(IModelConfigurator<LoginCredential> config)
    {
        config.ConfigureProperties(DescribeProps);
        config.ListViewProperties(p => p.Username, p => p.Enabled);
    }

    private void DescribeProps(IPropertyConfigurator<LoginCredential> configurator)
    {
        configurator.Property(p => p.Username);
        configurator.Property(p => p.PasswordHash).Password().Argon2();
        configurator.Property(p => p.Enabled);
    }
}
