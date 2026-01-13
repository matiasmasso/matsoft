using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwSku
    {
        public Guid Guid { get; set; }
        public Guid? Category { get; set; }
        public int Emp { get; set; }
        public int Art { get; set; }
        public int Ord { get; set; }
        public string Ref { get; set; } = null!;
        public string RefPrv { get; set; } = null!;
        public string? Cbar { get; set; }
        public string? PackageEan { get; set; }
        public Guid? CnapGuid { get; set; }
        public Guid? MadeIn { get; set; }
        public string? CodiMercancia { get; set; }
        public byte IvaCod { get; set; }
        public bool NoWeb { get; set; }
        public bool NoStk { get; set; }
        public bool NoTarifa { get; set; }
        public bool HeredaDimensions { get; set; }
        public bool NoDimensions { get; set; }
        public decimal KgNet { get; set; }
        public decimal Kg { get; set; }
        public decimal M3 { get; set; }
        public double DimensionL { get; set; }
        public double DimensionW { get; set; }
        public double DimensionH { get; set; }
        public short InnerPack { get; set; }
        public short OuterPack { get; set; }
        public bool ForzarInnerPack { get; set; }
        public bool LastProduction { get; set; }
        public decimal Pmc { get; set; }
        public byte Hereda { get; set; }
        public bool ImgExists { get; set; }
        public DateTime ImgFch { get; set; }
        public bool Outlet { get; set; }
        public int OutletDto { get; set; }
        public int OutletQty { get; set; }
        public bool NoPro { get; set; }
        public bool IsBundle { get; set; }
        public decimal? BundleDto { get; set; }
        public Guid? Substitute { get; set; }
        public int? SecurityStock { get; set; }
        public DateTime? Availability { get; set; }
        public DateTime? HideUntil { get; set; }
        public bool Obsoleto { get; set; }
        public DateTime? FchObsoleto { get; set; }
        public DateTime? ObsoletoConfirmed { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchLastEdited { get; set; }
        public string? NomCurtEsp { get; set; }
        public string? NomCurtCat { get; set; }
        public string? NomCurtEng { get; set; }
        public string? NomCurtPor { get; set; }
        public string? NomLlargEsp { get; set; }
        public string? NomLlargCat { get; set; }
        public string? NomLlargEng { get; set; }
        public string? NomLlargPor { get; set; }
    }
}
