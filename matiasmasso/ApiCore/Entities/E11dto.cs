using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class E11dto
{
    public Guid CliGuid { get; set; }

    public string Nif { get; set; } = null!;

    public string RaoSocial { get; set; } = null!;

    public decimal? Dto { get; set; }
}
