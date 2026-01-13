using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact list
    /// </summary>
    public partial class VwContact
    {
        public Guid ContactGuid { get; set; }
        public string RaoSocial { get; set; } = null!;
        public string? NomCom { get; set; }
        public string Nif { get; set; } = null!;
        public Guid? ContactClass { get; set; }
        public string? ContactClassNom { get; set; }
        public Guid? ChannelGuid { get; set; }
        public string? ChannelNom { get; set; }
        public Guid SrcGuid { get; set; }
        public string? Adr { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public Guid? ZipGuid { get; set; }
        public string ZipCod { get; set; } = null!;
        public Guid LocationGuid { get; set; }
        public string LocationNom { get; set; } = null!;
        public Guid ZonaGuid { get; set; }
        public string ZonaNom { get; set; } = null!;
        public Guid? ProvinciaGuid { get; set; }
        public string? ProvinciaNom { get; set; }
        public short ExportCod { get; set; }
        public bool SplitByComarcas { get; set; }
        public Guid CountryGuid { get; set; }
        public string CountryIso { get; set; } = null!;
        public string? PrefixeTelefonic { get; set; }
        public bool Cee { get; set; }
        public string CountryEsp { get; set; } = null!;
        public string CountryCat { get; set; } = null!;
        public string CountryEng { get; set; } = null!;
        public string CountryPor { get; set; } = null!;
        public byte Obsoleto { get; set; }
        public string? RaoSocialyNomCom { get; set; }
    }
}
