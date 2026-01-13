using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsCategory
{
    public Guid Brand { get; set; }

    public Guid Guid { get; set; }

    public int Codi { get; set; }

    public bool Obsoleto { get; set; }

    public int Ord { get; set; }

    public string? NomEsp { get; set; }

    public string? NomCat { get; set; }

    public string? NomEng { get; set; }

    public string? NomPor { get; set; }

    public string? ExcerptEsp { get; set; }

    public string? ExcerptCat { get; set; }

    public string? ExcerptEng { get; set; }

    public string? ExcerptPor { get; set; }

    public string? ContentEsp { get; set; }

    public string? ContentCat { get; set; }

    public string? ContentEng { get; set; }

    public string? ContentPor { get; set; }

    public int HasImage { get; set; }
}
