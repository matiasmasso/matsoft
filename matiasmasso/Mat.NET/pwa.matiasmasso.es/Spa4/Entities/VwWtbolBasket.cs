using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Result of customer conversion pixels
    /// </summary>
    public partial class VwWtbolBasket
    {
        public Guid Guid { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Site { get; set; }
        public string Web { get; set; } = null!;
        public Guid Basket { get; set; }
        public int Lin { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public short Emp { get; set; }
        public int SkuId { get; set; }
        public Guid SkuGuid { get; set; }
        public string SkuNom { get; set; } = null!;
        public string SkuNomLlarg { get; set; } = null!;
        public string SkuRef { get; set; } = null!;
        public string SkuPrvNom { get; set; } = null!;
        public Guid CategoryGuid { get; set; }
        public short CategoryOrd { get; set; }
        public string CategoryNom { get; set; } = null!;
        public int CategoryCodi { get; set; }
        public Guid BrandGuid { get; set; }
        public int BrandOrd { get; set; }
        public string BrandNom { get; set; } = null!;
        public int CodDist { get; set; }
        public int CodStk { get; set; }
        public Guid? Proveidor { get; set; }
        public Guid? RestrictAtlasToPremiumLine { get; set; }
        public int? WebAtlasDeadline { get; set; }
        public bool SkuNoMgz { get; set; }
        public bool SkuNoWeb { get; set; }
        public bool SkuNoStk { get; set; }
        public string? Ean13 { get; set; }
        public string? PackageEan { get; set; }
        public bool HeredaDimensions { get; set; }
        public short SkuMoq { get; set; }
        public bool SkuForzarMoq { get; set; }
        public decimal SkuKg { get; set; }
        public decimal SkuKgNet { get; set; }
        public int SkuDimensionL { get; set; }
        public int SkuDimensionW { get; set; }
        public int SkuDimensionH { get; set; }
        public Guid? SkuCnapGuid { get; set; }
        public int CategoryMoq { get; set; }
        public bool CategoryForzarMoq { get; set; }
        public decimal CategoryKg { get; set; }
        public decimal CategoryKgNet { get; set; }
        public int? CategoryDimensionL { get; set; }
        public int? CategoryDimensionW { get; set; }
        public int? CategoryDimensionH { get; set; }
        public bool LastProduction { get; set; }
        public bool Noweb { get; set; }
        public int Obsoleto { get; set; }
        public DateTime? CategoryHideUntil { get; set; }
        public DateTime? SkuHideUntil { get; set; }
        public int EnabledxPro { get; set; }
        public int EnabledxConsumer { get; set; }
        public bool NoPro { get; set; }
        public int SkuImageExists { get; set; }
        public Guid? CnapGuid { get; set; }
        public string? CnapId { get; set; }
        public string? CnapNom { get; set; }
        public Guid? MadeIn { get; set; }
        public string? CodiMercancia { get; set; }
    }
}
