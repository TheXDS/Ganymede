using Microsoft.EntityFrameworkCore;
using TheXDS.Ivie.Models;
using TheXDS.Triton.SecurityEssentials.Ef.Services.Base;
using TheXDS.Triton.Services;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ivie.Services;

/// <summary>
/// Defines the Tritón service used to interface with a EF core
/// <see cref="DbContext"/>.
/// </summary>
public class IvieService : UserServiceBase<IvieContext>, IIvieService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IvieService"/> class.
    /// </summary>
    public IvieService()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IvieService"/> class.
    /// </summary>
    /// <param name="factory">
    /// Transaction factory to be injected into the Tritón service.
    /// </param>
    public IvieService(ITransactionFactory factory) : base(factory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IvieService"/> class.
    /// </summary>
    /// <param name="transactionConfiguration">
    /// Transaction configuration instance to be injected into the Tritón
    /// service.
    /// </param>
    public IvieService(IMiddlewareConfigurator transactionConfiguration) : base(transactionConfiguration)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IvieService"/> class.
    /// </summary>
    /// <param name="transactionConfiguration">
    /// Transaction configuration instance to be injected into the Tritón
    /// service.
    /// </param>
    /// <param name="factory">
    /// Transaction factory to be injected into the Tritón service.
    /// </param>
    public IvieService(IMiddlewareConfigurator transactionConfiguration, EfCoreTransFactory<IvieContext> factory) : base(transactionConfiguration, factory)
    {
    }

    /// <inheritdoc/>
    public async Task<string> GetEmployeeDisplayNameAsync(Guid userId)
    {
        using var t = GetReadTransaction();
        return (await t.All<Employee>().Where(e => e.LoginCredential != null).FirstOrDefaultAsync(e => e.LoginCredential!.Id == userId))?.DisplayName ?? "User";
    }
}