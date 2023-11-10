using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

[Flags]
public enum ClientBehaviorFlags
{
    None,
    TaxExempt,

    /// <summary>
    /// Indicates that every client's address will receive their own invoice.
    /// </summary>
    IndependentCostCenters
}

public enum ProductFlags
{
    None,
    NotManufacturable,
    NotPackageable,
    NotInvoiceable,
    ExtraProduct
}

public class Embroidery : Model<int>
{
    public EmbroideryLocation Location { get; set; }
    public string Description { get; set; }
}

public class EmbroideryLocation : Model<int>
{
    public GarmentType Type { get; set; }
    public string LocationDescription { get; set; }
}

public class Fabric : Model<int>
{
    public string Name { get; set; }
    public FabricColor Color { get; set; }
}

public class FabricColor : Model<int>
{
    public string Description { get; set; }
    public int RgbDisplayColor { get; set; }
}

public class GarmentType : Model<int>
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public virtual ICollection<AdjustmentToGarmentType> Adjustments { get; set; } = new List<AdjustmentToGarmentType>();
}

public class GarmentStyle : Model<int>
{
    public GarmentType Type { get; set; }
    public string StyleCode { get; set; }
}

public class AdjustmentToGarmentType : Model<int>
{
    public GarmentType Type { get; set; }
    public AdjustmentDefinition Definition { get; set; }

}

public class AdjustmentDefinition : Model<int>
{
    public virtual ICollection<AdjustmentToGarmentType> Garments { get; set; } = new List<AdjustmentToGarmentType>();
    public string Title { get; set; }
    public string Format { get; set; }
    public bool NeedsMeasurement { get; set; }
    
}

public class AdjustmentItem : Model<int>
{

}


public class Client : Model<int>
{
    public string Name { get; set; }
    public string PathName { get; set; }
    public string ShortName { get; set; }
    public string CodePrefix { get; set; }
    public string ISOCurrency { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ClientBehaviorFlags Flags { get; set; }
    public virtual ICollection<ProductGroup> ProductGroups { get; set; } = new List<ProductGroup>();
    public virtual string Nit { get; set; }

}

public class ProductGroup : Model<int>
{
    public string Name { get; set; }

}






public class Product : Model<long>
{
    public string Name { get; set; }
    public GarmentType Type { get; set; }
    public virtual ICollection<Embroidery> Embroideries { get; set; } = new List<Embroidery>();
    public Fabric Fabric { get; set; }
    public GarmentStyle Style { get; set; }
    public decimal Price { get; set; }
}

public class Address : Model<int>
{
    public string Description { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public bool IsCorporateAddress { get; set; }
    public string? ContactName { get; set; }
    public string? ContactAddress { get; set; }
    public string? Phone { get; set; }
}
