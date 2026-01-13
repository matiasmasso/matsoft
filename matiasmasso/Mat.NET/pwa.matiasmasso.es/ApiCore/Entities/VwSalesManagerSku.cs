using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwSalesManagerSku
{
    public int BrandOrd { get; set; }

    public int CategoryCodi { get; set; }

    public short CategoryOrd { get; set; }

    public string SkuNom { get; set; } = null!;

    public string BrandNom { get; set; } = null!;

    public string CategoryNom { get; set; } = null!;

    public Guid BrandGuid { get; set; }

    public Guid CategoryGuid { get; set; }

    public Guid SkuGuid { get; set; }

    public Guid SalesManager { get; set; }
}
