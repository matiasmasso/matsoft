using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwPncSku
{
    public Guid PncGuid { get; set; }

    public Guid PdcGuid { get; set; }

    public int Lin { get; set; }

    public Guid SkuGuid { get; set; }

    public string? SkuNomLlarg { get; set; }

    public Guid CategoryGuid { get; set; }

    public Guid BrandGuid { get; set; }

    public Guid? RepGuid { get; set; }

    public decimal? Com { get; set; }

    public int Qty { get; set; }

    public decimal Eur { get; set; }

    public decimal Dto { get; set; }

    public decimal Dt2 { get; set; }
}
