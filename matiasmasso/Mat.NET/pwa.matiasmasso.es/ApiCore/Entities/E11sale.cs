using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class E11sale
{
    public Guid CliGuid { get; set; }

    public string Nif { get; set; } = null!;

    public string RaoSocial { get; set; } = null!;

    public double? Y2022 { get; set; }

    public double? Y2023 { get; set; }
}
