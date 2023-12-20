using TheXDS.Ganymede.CrudGen;
using TheXDS.Ivie.Models;
using TheXDS.Triton.Models;

namespace TheXDS.Ivie.CrudGen;

public class EmployeeDescriptor : CrudDescriptor<Employee>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Employee> config)
    {
        config.ConfigureProperties(m =>
        {
            m.Property(p => p.DisplayName);
        });
    }
}

public class CustomerDescriptor : CrudDescriptor<Customer>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Customer> config)
    {
        config.ConfigureProperties(m =>
        {
            m.Property(p => p.Name);
        });
    }
}

/// <summary>
/// Describes entities of type <see cref="Product"/>.
/// </summary>
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
        config.LabelResource<Resources.Strings.CrudGen.LoginCredential>();
        config.ConfigureProperties(DescribeProps);
        config.ListViewProperties(p => p.Username, p => p.Enabled);
    }

    private void DescribeProps(IPropertyConfigurator<LoginCredential> configurator)
    {
        configurator.Property(p => p.Username).Label("Username");
        configurator.Property(p => p.PasswordHash).Label("Password").Password().Argon2();
        configurator.Property(p => p.Enabled).Label("Enabled");
    }
}
