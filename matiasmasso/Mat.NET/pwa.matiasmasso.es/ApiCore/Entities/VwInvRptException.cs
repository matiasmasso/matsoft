using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwInvRptException
{
    public Guid Guid { get; set; }

    public DateTime Fch { get; set; }

    public string? DocNum { get; set; }

    public string? RaoSocial { get; set; }

    public string? Ref { get; set; }
}
