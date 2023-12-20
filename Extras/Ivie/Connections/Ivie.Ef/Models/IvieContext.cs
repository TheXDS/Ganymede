using Microsoft.EntityFrameworkCore;
using TheXDS.Triton.SecurityEssentials.Ef.Models;

namespace TheXDS.Ivie.Models;

/// <summary>
/// Defines the Ivie database context used to retrieve and store data using
/// Entity Framework.
/// </summary>
public class IvieContext : UserDbContextBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IvieContext"/> class.
    /// </summary>
    public IvieContext() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IvieContext"/> class.
    /// </summary>
    /// <param name="options">Context options to use.</param>
    public IvieContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Defines the <see cref="Employee"/> table.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Defines the <see cref="Customer"/> table.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Defines the <see cref="Product"/> table.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Defines the <see cref="Cai"/> table.
    /// </summary>
    public DbSet<Cai> Cais { get; set; }

    /// <summary>
    /// Defines the <see cref="CaiRange"/> table.
    /// </summary>
    public DbSet<CaiRange> CaiRanges { get; set; }

    /// <summary>
    /// Defines the <see cref="Invoice"/> table.
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; }

    /// <summary>
    /// Defines the <see cref="InvoiceItem"/> table.
    /// </summary>
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    /// <summary>
    /// Defines the <see cref="Payment"/> table.
    /// </summary>
    public DbSet<Payment> Payments { get; set; }

    /// <summary>
    /// Defines the <see cref="PaymentMethod"/> table.
    /// </summary>
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
}