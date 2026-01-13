using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw2023sale
{
    public Guid ArtGuid { get; set; }

    public int SkuId { get; set; }

    public string? SkuNomLlarg { get; set; }

    public int? Qty { get; set; }

    public double? Eur { get; set; }
}
