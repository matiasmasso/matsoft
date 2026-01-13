using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwTransm
{
    public Guid Guid { get; set; }

    public short Yea { get; set; }

    public int Transm { get; set; }

    public DateTimeOffset? Fch { get; set; }

    public Guid? AlbGuid { get; set; }

    public decimal? AlbEur { get; set; }

    public string? Nom { get; set; }

    public Guid? Zip { get; set; }

    public Guid? FraGuid { get; set; }

    public int? Serie { get; set; }

    public int? Fra { get; set; }

    public int? Lins { get; set; }

    public int? Qties { get; set; }
}
