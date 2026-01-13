using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwSellout2
    {
        public int Emp { get; set; }
        public short Cod { get; set; }
        public Guid Guid { get; set; }
        public int Qty { get; set; }
        public decimal? Eur { get; set; }
        public int ErrCod { get; set; }
        public Guid PdcGuid { get; set; }
        public int Pdc { get; set; }
        public DateTime? FchCreated { get; set; }
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
        public string? BrandNomEsp { get; set; }
        public int BrandOrd { get; set; }
        public Guid CategoryGuid { get; set; }
        public string? CategoryNomEsp { get; set; }
        public string? CategoryNomCat { get; set; }
        public string? CategoryNomEng { get; set; }
        public string? CategoryNomPor { get; set; }
        public int CategoryCodi { get; set; }
        public short CategoryOrd { get; set; }
        public Guid SkuGuid { get; set; }
        public string? SkuNomEsp { get; set; }
        public string? SkuNomCat { get; set; }
        public string? SkuNomEng { get; set; }
        public string? SkuNomPor { get; set; }
        public string SkuRef { get; set; } = null!;
        public string SkuPrvNom { get; set; } = null!;
        public Guid? ProveidorGuid { get; set; }
        public string? ProveidorNom { get; set; }
        public Guid? CnapGuid { get; set; }
        public string? CnapId { get; set; }
        public string? CnapNom { get; set; }
        public string? Ean13 { get; set; }
        public bool IsBundle { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerNom { get; set; } = null!;
        public string? CustomerNomCom { get; set; }
        public Guid? ContactClass { get; set; }
        public string ContactClassNom { get; set; } = null!;
        public Guid? ChannelGuid { get; set; }
        public string? ChannelNom { get; set; }
        public string? ChannelNomCat { get; set; }
        public string? ChannelNomEng { get; set; }
        public string? ChannelNomPor { get; set; }
        public int ChannelOrd { get; set; }
        public Guid? RepGuid { get; set; }
        public string? RepNom { get; set; }
        public Guid? Ccx { get; set; }
        public string? ClientRef { get; set; }
    }
}
