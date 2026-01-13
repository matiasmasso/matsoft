using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwAreaNom
{
    /// <summary>
    /// 1:Country, 2:Zona, 3:Location, 4:Zip
    /// </summary>
    public int AreaCod { get; set; }

    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key for Country table
    /// </summary>
    public Guid CountryGuid { get; set; }

    /// <summary>
    /// ISO 3166 country code
    /// </summary>
    public string CountryIso { get; set; } = null!;

    /// <summary>
    /// Spanish country name
    /// </summary>
    public string CountryNomEsp { get; set; } = null!;

    /// <summary>
    /// Catalan country name
    /// </summary>
    public string CountryNomCat { get; set; } = null!;

    /// <summary>
    /// English country name
    /// </summary>
    public string CountryNomEng { get; set; } = null!;

    /// <summary>
    /// Portuguese country name
    /// </summary>
    public string CountryNomPor { get; set; } = null!;

    /// <summary>
    /// Foreign key to Zona table
    /// </summary>
    public Guid? ZonaGuid { get; set; }

    /// <summary>
    /// Zona name
    /// </summary>
    public string? ZonaNom { get; set; }

    /// <summary>
    /// Foreign key to Location table
    /// </summary>
    public Guid? LocationGuid { get; set; }

    /// <summary>
    /// Name of the city or town
    /// </summary>
    public string? LocationNom { get; set; }

    /// <summary>
    /// Foreign key to Zip table
    /// </summary>
    public Guid? ZipGuid { get; set; }

    /// <summary>
    /// Postal code
    /// </summary>
    public string? ZipCod { get; set; }

    /// <summary>
    /// Foreign key to Provincia table
    /// </summary>
    public Guid? Provincia { get; set; }

    /// <summary>
    /// Name of the province
    /// </summary>
    public string? ProvinciaNom { get; set; }
}
