using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Api.Entities;

public partial class VwAddress
{
    /// <summary>
    /// Contact identifier whom the address belongs
    /// </summary>
    public Guid SrcGuid { get; set; }

    /// <summary>
    /// Address purpose: Fiscal (1), Correspondencia (2), Entregas (3), Fra.Consumidor (4)
    /// </summary>
    public string? Adr { get; set; }

    /// <summary>
    /// Name of the street or road
    /// </summary>
    public string? AdrViaNom { get; set; }

    /// <summary>
    /// Street number or any detail to identify the location within the street
    /// </summary>
    public string? AdrNum { get; set; }

    /// <summary>
    /// Floor number or any detail to identify the location within the building
    /// </summary>
    public string? AdrPis { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    /// <summary>
    /// Geographical data to compute distances from other points
    /// </summary>
    public Geometry? Geo { get; set; }

    /// <summary>
    /// Foreign key to Zip table (postal codes)
    /// </summary>
    public Guid? ZipGuid { get; set; }

    /// <summary>
    /// Postal code
    /// </summary>
    public string ZipCod { get; set; } = null!;

    /// <summary>
    /// Foreign key to Location table
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
    /// Zona name
    /// </summary>
    public string ZonaNom { get; set; } = null!;

    /// <summary>
    /// Code for Custom purposes: 1:National, 2:CEE, 3:Rest of the world
    /// </summary>
    public short ExportCod { get; set; }

    /// <summary>
    /// Foreign key to Provincia table
    /// </summary>
    public Guid? ProvinciaGuid { get; set; }

    /// <summary>
    /// Name of the province
    /// </summary>
    public string? ProvinciaNom { get; set; }

    public string? ProvinciaIntrastat { get; set; }

    /// <summary>
    /// Wether Zones may be split into Comarcas
    /// </summary>
    public bool SplitByComarcas { get; set; }

    /// <summary>
    /// Foreign key to Region table
    /// </summary>
    public Guid? RegioGuid { get; set; }

    /// <summary>
    /// Name of the Region or Comunidad Autónoma
    /// </summary>
    public string? RegioNom { get; set; }

    /// <summary>
    /// Foreign key to Country table
    /// </summary>
    public Guid CountryGuid { get; set; }

    /// <summary>
    /// ISO 3166 country code (2 letters)
    /// </summary>
    public string CountryIso { get; set; } = null!;

    /// <summary>
    /// International Dilaing Code
    /// </summary>
    public string? PrefixeTelefonic { get; set; }

    /// <summary>
    /// True if the location belongs to European Community
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
    /// Always value 1 meaning official corporate address
    /// </summary>
    public int Cod { get; set; }
}
