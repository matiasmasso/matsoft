using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Edi received sales reports from customers
    /// </summary>
    public partial class VwEdiSalesReport
    {
        public Guid? Holding { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Customer { get; set; }
        public string? Dept { get; set; }
        public string? Centro { get; set; }
        public string? CustomerRef { get; set; }
        public Guid BrandGuid { get; set; }
        public Guid? Proveidor { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
        public string BrandNom { get; set; } = null!;
        public string CategoryNom { get; set; } = null!;
        public string SkuNom { get; set; } = null!;
        public string SkuRef { get; set; } = null!;
        public string SkuPrvNom { get; set; } = null!;
        public Guid CountryGuid { get; set; }
        public Guid ZonaGuid { get; set; }
        public Guid LocationGuid { get; set; }
        public Guid? ZipGuid { get; set; }
        public string CountryIso { get; set; } = null!;
        public string ZonaNom { get; set; } = null!;
        public string LocationNom { get; set; } = null!;
        public int Qty { get; set; }
        public int QtyBack { get; set; }
        public decimal? Eur { get; set; }
    }
}
