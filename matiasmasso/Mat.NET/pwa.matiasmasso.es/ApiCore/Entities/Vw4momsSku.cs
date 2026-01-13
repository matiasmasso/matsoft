using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsSku
{
    public Guid BrandGuid { get; set; }

    public Guid CategoryGuid { get; set; }

    public Guid SkuGuid { get; set; }

    public string SkuRef { get; set; } = null!;

    public string? SkuNomEsp { get; set; }

    public string? SkuNomCat { get; set; }

    public string? SkuNomEng { get; set; }

    public string? SkuNomPor { get; set; }

    public string? SkuNomLlargEsp { get; set; }

    public string? SkuNomLlargCat { get; set; }

    public string? SkuNomLlargEng { get; set; }

    public string? SkuNomLlargPor { get; set; }

    public string? ExcerptEsp { get; set; }

    public string? ExcerptCat { get; set; }

    public string? ExcerptEng { get; set; }

    public string? ExcerptPor { get; set; }

    public string? ContentEsp { get; set; }

    public string? ContentCat { get; set; }

    public string? ContentEng { get; set; }

    public string? ContentPor { get; set; }

    public int Obsoleto { get; set; }

    public int HasImage { get; set; }
}
