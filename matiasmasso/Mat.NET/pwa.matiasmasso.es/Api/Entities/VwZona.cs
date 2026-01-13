using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Joins Zona table with the area tables it belongs to (Country, Region, Provincia)
    /// </summary>
    public partial class VwZona
    {
        /// <summary>
        /// Foreign key to Zona table
        /// </summary>
        public Guid ZonaGuid { get; set; }
        /// <summary>
        /// Zona name
        /// </summary>
        public string ZonaNom { get; set; } = null!;
        /// <summary>
        /// Zona export cod: 1:National, 2:EEC, 3:Rest of the world
        /// </summary>
        public short ExportCod { get; set; }
        /// <summary>
        /// Language we should display for this zona residents. ISO 639-2
        /// </summary>
        public string? ZonaLang { get; set; }
        /// <summary>
        /// Foreign key for Provincia table
        /// </summary>
        public Guid? ProvinciaGuid { get; set; }
        /// <summary>
        /// Name of the province, if any
        /// </summary>
        public string? ProvinciaNom { get; set; }
        /// <summary>
        /// Foreign key to Region table
        /// </summary>
        public Guid? RegioGuid { get; set; }
        /// <summary>
        /// Name of the region or Comunidad Autonoma, if any
        /// </summary>
        public string? RegioNom { get; set; }
        /// <summary>
        /// True if the zone may be split into Comarcas
        /// </summary>
        public bool SplitByComarcas { get; set; }
        /// <summary>
        /// Foreign key to Country table
        /// </summary>
        public Guid CountryGuid { get; set; }
        /// <summary>
        /// ISO 3166 country code (2 letters)
        /// </summary>
        public string CountryIso { get; set; } = null!;
        /// <summary>
        /// International call prefix
        /// </summary>
        public string? PrefixeTelefonic { get; set; }
        /// <summary>
        /// For Customs purposes, true if it belongs to EEC
        /// </summary>
        public bool Cee { get; set; }
        /// <summary>
        /// Language to display for the country, regardless of the zona language
        /// </summary>
        public string? CountryLang { get; set; }
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
    }
}
