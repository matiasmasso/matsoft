using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Api.Entities;

/// <summary>
/// Contact addresses
/// </summary>
public partial class CliAdr
{
    /// <summary>
    /// Primary key, usually linked to a Contact field from another view.
    /// </summary>
    public Guid SrcGuid { get; set; }

    /// <summary>
    /// Several addresses may be registered for same contact; this code specifies if it is the official corporate address, postal address or delivery address
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// The address within the location, the way it should be written locally
    /// </summary>
    public string? Adr { get; set; }

    /// <summary>
    /// Wether it is a street, road, avenue, square...
    /// </summary>
    public Guid? AdrViaType { get; set; }

    /// <summary>
    /// The name of the street
    /// </summary>
    public string? AdrViaNom { get; set; }

    /// <summary>
    /// The number within the street, the Km point within the road...
    /// </summary>
    public string? AdrNum { get; set; }

    /// <summary>
    /// The flat or apartment number, or any data not relevant to locate the address in a map
    /// </summary>
    public string? AdrPis { get; set; }

    /// <summary>
    /// Foreign key for Postal Code table
    /// </summary>
    public Guid? Zip { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    /// <summary>
    /// Code to register where did the address come from
    /// </summary>
    public int? GeoFont { get; set; }

    /// <summary>
    /// Geography data type for distance calculations and nearest neighbours
    /// </summary>
    public Geometry? Geo { get; set; }

    /// <summary>
    /// Who registered this address coordinates (foreign key for email addresses)
    /// </summary>
    public Guid? GeoUserLastEdited { get; set; }

    /// <summary>
    /// Date when last user edited the address coordinates
    /// </summary>
    public DateTime? GeoFchLastEdited { get; set; }

    public virtual Zip? ZipNavigation { get; set; }
}
