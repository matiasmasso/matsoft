using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Countries
/// </summary>
public partial class Country
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// ISO 3166 country code
    /// </summary>
    public string Iso { get; set; } = null!;

    /// <summary>
    /// Country name in Spanish
    /// </summary>
    public string NomEsp { get; set; } = null!;

    /// <summary>
    /// Country name in Catalan
    /// </summary>
    public string NomCat { get; set; } = null!;

    /// <summary>
    /// Country name in English
    /// </summary>
    public string NomEng { get; set; } = null!;

    /// <summary>
    /// Country name in Portuguese
    /// </summary>
    public string NomPor { get; set; } = null!;

    /// <summary>
    /// Country phone prefix
    /// </summary>
    public string? PrefixeTelefonic { get; set; }

    /// <summary>
    /// whether the country belongs to UE
    /// </summary>
    public bool Cee { get; set; }

    /// <summary>
    /// Export code for Customs: National (1), European (2) others (3)
    /// </summary>
    public short ExportCod { get; set; }

    /// <summary>
    /// Country flag icon
    /// </summary>
    public byte[]? Flag { get; set; }

    /// <summary>
    /// ISO 639-2 language code (3 letters) to address to residents
    /// </summary>
    public string? LangIso { get; set; }

    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();

    public virtual ICollection<CliTel> CliTels { get; set; } = new List<CliTel>();

    public virtual ICollection<IntrastatPartidum> IntrastatPartidumCountryNavigations { get; set; } = new List<IntrastatPartidum>();

    public virtual ICollection<IntrastatPartidum> IntrastatPartidumMadeInNavigations { get; set; } = new List<IntrastatPartidum>();

    public virtual ICollection<RecallCli> RecallClis { get; set; } = new List<RecallCli>();

    public virtual ICollection<Regio> Regios { get; set; } = new List<Regio>();

    public virtual ICollection<Sorteo> Sorteos { get; set; } = new List<Sorteo>();

    public virtual ICollection<Stp> Stps { get; set; } = new List<Stp>();

    public virtual ICollection<Tpa> Tpas { get; set; } = new List<Tpa>();

    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}
