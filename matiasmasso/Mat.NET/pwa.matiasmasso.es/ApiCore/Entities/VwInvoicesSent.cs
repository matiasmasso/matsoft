using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwInvoicesSent
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public short Yea { get; set; }

    public int Fra { get; set; }

    public int Serie { get; set; }

    public DateTime Fch { get; set; }

    public Guid CliGuid { get; set; }

    public string RaoSocial { get; set; } = null!;

    public decimal EurLiq { get; set; }

    public string Fpg { get; set; } = null!;

    public string TipoFactura { get; set; } = null!;

    public string? SiiL9 { get; set; }

    public string? RegimenEspecialOtrascendencia { get; set; }

    public int? Concepte { get; set; }

    public int SiiResult { get; set; }

    public short PrintMode { get; set; }
}
