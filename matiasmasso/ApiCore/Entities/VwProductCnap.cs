using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwProductCnap
{
    public Guid Guid { get; set; }

    public Guid? Parent { get; set; }

    public string Id { get; set; } = null!;

    public Guid? BrandGuid { get; set; }

    public string? BrandNom { get; set; }

    public Guid? CategoryGuid { get; set; }

    public string? CategoryNom { get; set; }

    public string? SkuNom { get; set; }

    public string? Tags { get; set; }

    public string? NomShortEsp { get; set; }

    public string? NomShortCat { get; set; }

    public string? NomShortEng { get; set; }

    public string? NomShortPor { get; set; }

    public string? NomLongEsp { get; set; }

    public string? NomLongCat { get; set; }

    public string? NomLongEng { get; set; }

    public string? NomLongPor { get; set; }
}
