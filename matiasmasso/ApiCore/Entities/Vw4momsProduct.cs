using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsProduct
{
    public Guid BrandGuid { get; set; }

    public Guid CategoryGuid { get; set; }

    public int CategoryCodi { get; set; }

    public string SkuRef { get; set; } = null!;

    public int CategoryOrd { get; set; }

    public string? CategoryNomEsp { get; set; }

    public string? CategoryNomCat { get; set; }

    public string? CategoryNomEng { get; set; }

    public string? CategoryNomPor { get; set; }

    public Guid SkuGuid { get; set; }

    public string? SkuNomEsp { get; set; }

    public string? SkuNomCat { get; set; }

    public string? SkuNomEng { get; set; }

    public string? SkuNomPor { get; set; }

    public string? SkuNomLlargEsp { get; set; }

    public string? SkuNomLlargCat { get; set; }

    public string? SkuNomLlargEng { get; set; }

    public string? SkuNomLlargPor { get; set; }

    public string? CategoryExcerptEsp { get; set; }

    public string? CategoryExcerptCat { get; set; }

    public string? CategoryExcerptEng { get; set; }

    public string? CategoryExcerptPor { get; set; }

    public string? CategoryContentEsp { get; set; }

    public string? CategoryContentCat { get; set; }

    public string? CategoryContentEng { get; set; }

    public string? CategoryContentPor { get; set; }

    public string? SkuExcerptEsp { get; set; }

    public string? SkuExcerptCat { get; set; }

    public string? SkuExcerptEng { get; set; }

    public string? SkuExcerptPor { get; set; }

    public string? SkuContentEsp { get; set; }

    public string? SkuContentCat { get; set; }

    public string? SkuContentEng { get; set; }

    public string? SkuContentPor { get; set; }
}
