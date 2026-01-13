using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer result product range
    /// </summary>
    public partial class VwCustomerSku
    {
        public Guid Customer { get; set; }
        public int Emp { get; set; }
        public int BrandOrd { get; set; }
        public string? BrandNom { get; set; }
        public string? BrandNomEsp { get; set; }
        public int CodDist { get; set; }
        public int CategoryCodi { get; set; }
        public short CategoryOrd { get; set; }
        public string? CategoryNom { get; set; }
        public string? CategoryNomEsp { get; set; }
        public string? CategoryNomCat { get; set; }
        public string? CategoryNomEng { get; set; }
        public string? CategoryNomPor { get; set; }
        public int SkuId { get; set; }
        public string? SkuNom { get; set; }
        public string? SkuNomEsp { get; set; }
        public string? SkuNomCat { get; set; }
        public string? SkuNomEng { get; set; }
        public string? SkuNomPor { get; set; }
        public string? SkuNomLlarg { get; set; }
        public string? SkuNomLlargEsp { get; set; }
        public string? SkuNomLlargCat { get; set; }
        public string? SkuNomLlargEng { get; set; }
        public string? SkuNomLlargPor { get; set; }
        public string SkuRef { get; set; } = null!;
        public string SkuPrvNom { get; set; } = null!;
        public string? Ean13 { get; set; }
        public string? PackageEan { get; set; }
        public Guid BrandGuid { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
        public int EnabledxConsumer { get; set; }
        public bool HeredaDimensions { get; set; }
        public short SkuMoq { get; set; }
        public bool SkuForzarMoq { get; set; }
        public decimal SkuKg { get; set; }
        public double SkuDimensionL { get; set; }
        public double SkuDimensionW { get; set; }
        public double SkuDimensionH { get; set; }
        public int CategoryMoq { get; set; }
        public bool CategoryForzarMoq { get; set; }
        public decimal CategoryKg { get; set; }
        public double? CategoryDimensionL { get; set; }
        public double? CategoryDimensionW { get; set; }
        public double? CategoryDimensionH { get; set; }
        public string? CodiMercancia { get; set; }
        public int SkuImageExists { get; set; }
        public int? CodExclusio { get; set; }
        public Guid? MadeIn { get; set; }
        public DateTime? SkuHideUntil { get; set; }
        public DateTime? CategoryHideUntil { get; set; }
        public int Obsoleto { get; set; }
    }
}
