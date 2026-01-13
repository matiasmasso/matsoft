using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Zona locations (towns, cities...)
/// </summary>
public partial class Location
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to Zona table
    /// </summary>
    public Guid Zona { get; set; }

    /// <summary>
    /// Location name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Foreign key to Comarca table
    /// </summary>
    public Guid? Comarca { get; set; }

    public virtual ICollection<Bn2> Bn2s { get; set; } = new List<Bn2>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();

    public virtual ICollection<Zip> Zips { get; set; } = new List<Zip>();

    public virtual Zona ZonaNavigation { get; set; } = null!;
}
