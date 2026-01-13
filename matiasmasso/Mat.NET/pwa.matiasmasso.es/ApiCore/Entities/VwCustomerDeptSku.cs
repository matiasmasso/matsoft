using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerDeptSku
{
    public string Cod { get; set; } = null!;

    public string? Nom { get; set; }

    public string SkuRef { get; set; } = null!;

    public string? SkuNomEsp { get; set; }

    public string? SkuNomLlargEsp { get; set; }

    public Guid SkuGuid { get; set; }

    public string? BrandNomEsp { get; set; }

    public string? ExcerptEsp { get; set; }

    public string? Ean13 { get; set; }

    public decimal SkuKg { get; set; }

    public decimal? Retail { get; set; }

    public double SkuDimensionL { get; set; }

    public double SkuDimensionW { get; set; }

    public double SkuDimensionH { get; set; }

    public Guid? MgzGuid { get; set; }

    public int? Stock { get; set; }

    public int SkuId { get; set; }

    public Guid Customer { get; set; }

    public bool SkuNoEcom { get; set; }

    public Guid CategoryGuid { get; set; }
}
