using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class E11Customer
{
    public Guid Guid { get; set; }

    public string Nif { get; set; } = null!;

    public string RaoSocial { get; set; } = null!;
}
