using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwLocation
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid LocationGuid { get; set; }

    /// <summary>
    /// Name of the city or town
    /// </summary>
    public string LocationNom { get; set; } = null!;

    /// <summary>
    /// Foreign key to Zona table
    /// </summary>
    public Guid ZonaGuid { get; set; }

    /// <summary>
    /// Name of the zone within the country
    /// </summary>
    public string ZonaNom { get; set; } = null!;

    /// <summary>
    /// For Customs purposes: 1:National, 2:EEC; 3:Rest of the world
    /// </summary>
    public short ExportCod { get; set; }

    /// <summary>
    /// Foreign key to Provincia table, if any
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
    /// Name of the region or Comunidad Autónoma
    /// </summary>
    public string? RegioNom { get; set; }

    /// <summary>
    /// True if the zone may be split by comarcas
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
    /// True if the country belongs to EEC
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
}
