using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// El Corte Ingles sales reports received via Edi
    /// </summary>
    public partial class VwEciSalesReport
    {
        public int? Qty { get; set; }
        public decimal? Eur { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public Guid CountryGuid { get; set; }
        public string CountryEsp { get; set; } = null!;
        public string CountryCat { get; set; } = null!;
        public string CountryEng { get; set; } = null!;
        public string CountryPor { get; set; } = null!;
        public Guid ZonaGuid { get; set; }
        public string ZonaNom { get; set; } = null!;
        public Guid LocationGuid { get; set; }
        public string LocationNom { get; set; } = null!;
        public Guid BrandGuid { get; set; }
        public string? BrandNom { get; set; }
        public string? BrandNomEsp { get; set; }
        public int BrandOrd { get; set; }
        public Guid CategoryGuid { get; set; }
        public string? CategoryNom { get; set; }
        public string? CategoryNomEsp { get; set; }
        public string? CategoryNomCat { get; set; }
        public string? CategoryNomEng { get; set; }
        public string? CategoryNomPor { get; set; }
        public short CategoryOrd { get; set; }
        public Guid SkuGuid { get; set; }
        public string? SkuNom { get; set; }
        public string? SkuNomCat { get; set; }
        public string? SkuNomEng { get; set; }
        public string? SkuNomPor { get; set; }
        public string SkuRef { get; set; } = null!;
        public string SkuPrvNom { get; set; } = null!;
        public Guid? ProveidorGuid { get; set; }
        public string ProveidorNom { get; set; } = null!;
        public Guid? CnapGuid { get; set; }
        public string? CnapId { get; set; }
        public string? CnapNom { get; set; }
        public string? Ean13 { get; set; }
        public Guid? CustomerGuid { get; set; }
        public Guid? Ccx { get; set; }
        public string? CustomerRef { get; set; }
    }
}
