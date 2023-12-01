using TheXDS.Triton.Models;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

public class Employee : Model<Guid>
{
    public string? DisplayName { get; set; }

    public LoginCredential? LoginCredential { get; set; }

    public override string ToString() => DisplayName ?? string.Empty;
}

public class Customer : Model<Guid>
{
    public string? Name { get; set; }
}

public class Product : Model<Guid>
{
    public string Description { get; set; }

    public decimal BasePrice { get; set; }

    public override string ToString() => Description ?? string.Empty;
}

public class Cai : TimestampModel<string>
{
    public virtual ICollection<CaiRange> Ranges { get; set; } = new List<CaiRange>();

    public DateTime? Void { get; set; }
}

public class CaiRange : Model<long>
{
    public Cai Parent { get; set; }
    public short NumLocal { get; set; }
    public byte NumDocument { get; set; }
    public short NumInvoicer { get; set; }
    public int InitialRange { get; set; }
    public int FinalRange { get; set; }
}

public class Invoice : TimestampModel<long>
{
    public CaiRange Parent { get; set; }
    public Employee Cashier { get; set; }
    public Customer? Customer { get; set; }
    public int Index { get; set; }
    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    public bool Voided { get; set; }
    public bool Printed { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class InvoiceItem : Model<long>
{
    public Product Product { get; set; }
    public Invoice Parent { get; set; }
    public int Qty { get; set; }
    public decimal StaticPrice { get; set; }
    public decimal StaticIsv { get; set; }
}

public class Payment : Model<long>
{
    public Invoice Parent { get; set; }
    public PaymentMethod Method { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentMethod : CatalogModel<Guid>
{
}
