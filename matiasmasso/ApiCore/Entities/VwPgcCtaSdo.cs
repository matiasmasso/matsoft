using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwPgcCtaSdo
{
    public Guid? ContactGuid { get; set; }

    public int? Yea { get; set; }

    public Guid CtaGuid { get; set; }

    public string CtaId { get; set; } = null!;

    public int CtaCod { get; set; }

    public byte CtaAct { get; set; }

    public string Esp { get; set; } = null!;

    public string? Cat { get; set; }

    public string? Eng { get; set; }

    public decimal? Deb { get; set; }

    public decimal? Hab { get; set; }
}
