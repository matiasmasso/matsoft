using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class HpLastPurchase
{
    public Guid AlbGuid { get; set; }

    public Guid ArtGuid { get; set; }

    public int Alb { get; set; }

    public DateTime Fch { get; set; }

    public string Nom { get; set; } = null!;

    public Guid? CliGuid { get; set; }

    public decimal? Eur { get; set; }

    public decimal Dto { get; set; }
}
