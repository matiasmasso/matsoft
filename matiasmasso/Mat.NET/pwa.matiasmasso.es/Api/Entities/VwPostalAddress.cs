using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact address details
    /// </summary>
    public partial class VwPostalAddress
    {
        /// <summary>
        /// Target; usually foreign key for CliGral table
        /// </summary>
        public Guid SrcGuid { get; set; }
        public string? Adr { get; set; }
        public string ZipCod { get; set; } = null!;
        public string LocationNom { get; set; } = null!;
        public string? ProvinciaNom { get; set; }
        public string CountryIso { get; set; } = null!;
        public bool Cee { get; set; }
        public short ExportCod { get; set; }
        public string CountryEsp { get; set; } = null!;
        public string CountryCat { get; set; } = null!;
        public string CountryEng { get; set; } = null!;
        public string CountryPor { get; set; } = null!;
        public Guid? ZipGuid { get; set; }
        public Guid LocationGuid { get; set; }
        public Guid ZonaGuid { get; set; }
        public Guid? ProvinciaGuid { get; set; }
        public Guid CountryGuid { get; set; }
        public string ZonaNom { get; set; } = null!;
        public bool SplitByComarcas { get; set; }
    }
}
