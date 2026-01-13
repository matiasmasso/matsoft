using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class E11cli
{
    public Guid CliGuid { get; set; }

    public string Nif { get; set; } = null!;

    public string RaoSocial { get; set; } = null!;

    public string ZonaNom { get; set; } = null!;

    public string LocationNom { get; set; } = null!;

    public string ZipCod { get; set; } = null!;

    public string? Adr { get; set; }

    public string? TelNum { get; set; }

    public string? Email { get; set; }
}
