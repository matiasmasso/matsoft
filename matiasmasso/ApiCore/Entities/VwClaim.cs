using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwClaim
{
    public Guid Guid { get; set; }

    public int Cod { get; set; }

    public string? NomEsp { get; set; }

    public string? NomCat { get; set; }

    public string? NomEng { get; set; }

    public string? NomPor { get; set; }

    public string? DscEsp { get; set; }

    public string? DscCat { get; set; }

    public string? DscEng { get; set; }

    public string? DscPor { get; set; }
}
