using Microsoft.EntityFrameworkCore;
using System.Security;
using TheXDS.Triton.Models;
using TheXDS.Triton.SecurityEssentials.Ef.Models;
using TheXDS.Triton.SecurityEssentials.Ef.Services.Base;
using TheXDS.Triton.Services;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.Models.Context;

public class IvieContext : UserDbContextBase
{
    public IvieContext() : base()
    {
    }

    public IvieContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cai> Cais {  get; set; }
    public DbSet<CaiRange> CaiRanges { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
}

public class IvieService : UserServiceBase<IvieContext>
{
    public IvieService()
    {
    }

    public IvieService(ITransactionFactory factory) : base(factory)
    {
    }

    public IvieService(IMiddlewareConfigurator transactionConfiguration) : base(transactionConfiguration)
    {
    }

    public IvieService(IMiddlewareConfigurator transactionConfiguration, EfCoreTransFactory<IvieContext> factory) : base(transactionConfiguration, factory)
    {
    }

    public async Task<string> GetEmployeeDisplayNameAsync(Guid userId)
    {
        using var t = GetReadTransaction();
        return (await t.All<Employee>().Where(e => e.LoginCredential != null).FirstOrDefaultAsync(e => e.LoginCredential!.Id == userId))?.DisplayName ?? "User";
    }
}