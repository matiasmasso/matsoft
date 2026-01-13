using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwPdc
{
    public Guid PdcGuid { get; set; }

    public DateOnly Fch { get; set; }

    public DateTime? FchCreated { get; set; }

    public string Pdd { get; set; } = null!;

    public Guid CliGuid { get; set; }

    public short PdcCod { get; set; }

    public int PncQty { get; set; }

    public int PncPn2 { get; set; }

    public Guid SkuGuid { get; set; }

    public decimal PncEur { get; set; }

    public decimal PncDto { get; set; }

    public Guid PncGuid { get; set; }

    public decimal Dt2 { get; set; }
}
