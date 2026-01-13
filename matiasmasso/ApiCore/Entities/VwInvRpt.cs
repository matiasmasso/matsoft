using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwInvRpt
{
    public Guid Guid { get; set; }

    public string? DocNum { get; set; }

    public Guid? CustomerGuid { get; set; }

    public DateTime Fch { get; set; }

    public string? CustomerNom { get; set; }

    public Guid BrandGuid { get; set; }

    public Guid CategoryGuid { get; set; }

    public int CategoryCodi { get; set; }

    public Guid SkuGuid { get; set; }

    public string? BrandNomEsp { get; set; }

    public string? CategoryNomEsp { get; set; }

    public string? SkuNomEsp { get; set; }

    public int Qty { get; set; }

    public decimal? Retail { get; set; }
}
