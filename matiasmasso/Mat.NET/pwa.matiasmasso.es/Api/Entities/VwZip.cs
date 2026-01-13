using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Joins Zip table with the area tables it belongs to (Country, Region, Provincia, Zona, Location)
    /// </summary>
    public partial class VwZip
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid? ZipGuid { get; set; }
        /// <summary>
        /// Postal code
        /// </summary>
        public string? ZipCod { get; set; }
        /// <summary>
        /// Foreign key to Location table
        /// </summary>
        public Guid? LocationGuid { get; set; }
        /// <summary>
        /// Name of the city or town
        /// </summary>
        public string? LocationNom { get; set; }
        /// <summary>
        /// Foreign key to Zona table
        /// </summary>
        public Guid? ZonaGuid { get; set; }
        /// <summary>
        /// Name of the Zona where the address belongs
        /// </summary>
        public string? ZonaNom { get; set; }
        /// <summary>
        /// For Customs purposes: 1:National, 2:EEC; 3:rest of the world
        /// </summary>
        public short? ExportCod { get; set; }
        /// <summary>
        /// Fotreign key to Provincia table, if any
        /// </summary>
        public Guid? ProvinciaGuid { get; set; }
        /// <summary>
        /// Name of the province, if any
        /// </summary>
        public string? ProvinciaNom { get; set; }
        public string? ProvinciaIntrastat { get; set; }
        /// <summary>
        /// Foreign key to Regio table, if any
        /// </summary>
        public Guid? RegioGuid { get; set; }
        /// <summary>
        /// Name of the region or Comunidad Autónoma where the province belongs, if any
        /// </summary>
        public string? RegioNom { get; set; }
        /// <summary>
        /// True if the Zona may be split into Comarcas
        /// </summary>
        public bool? SplitByComarcas { get; set; }
        /// <summary>
        /// Foreign key to Country table
        /// </summary>
        public Guid CountryGuid { get; set; }
        /// <summary>
        /// ISO 3166 country code
        /// </summary>
        public string CountryIso { get; set; } = null!;
        /// <summary>
        /// International call code
        /// </summary>
        public string? PrefixeTelefonic { get; set; }
        /// <summary>
        /// True if the country is member of EEC
        /// </summary>
        public bool Cee { get; set; }
        /// <summary>
        /// Spanish country name
        /// </summary>
        public string CountryEsp { get; set; } = null!;
        /// <summary>
        /// Catalan country name
        /// </summary>
        public string CountryCat { get; set; } = null!;
        /// <summary>
        /// English country name
        /// </summary>
        public string CountryEng { get; set; } = null!;
        /// <summary>
        /// Portuguese country name
        /// </summary>
        public string CountryPor { get; set; } = null!;
        /// <summary>
        /// Language to display to country residents
        /// </summary>
        public string? CountryLang { get; set; }
    }
}
