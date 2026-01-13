using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Postsales incidence codes
/// </summary>
public partial class IncidenciesCod
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Name in Spanish language
    /// </summary>
    public string Esp { get; set; } = null!;

    /// <summary>
    /// Name in Catalan language
    /// </summary>
    public string? Cat { get; set; }

    /// <summary>
    /// Name in English language
    /// </summary>
    public string? Eng { get; set; }

    /// <summary>
    /// Name in Portuguese language
    /// </summary>
    public string? Por { get; set; }

    /// <summary>
    /// Enumerable (0.averia 1.tancament)
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// True if it implies reposition of spares part of the product
    /// </summary>
    public bool ReposicionParcial { get; set; }

    /// <summary>
    /// True if it implies the full reposition of a new product
    /// </summary>
    public bool ReposicionTotal { get; set; }

    public virtual ICollection<Incidency> IncidencyCodiAperturaNavigations { get; set; } = new List<Incidency>();

    public virtual ICollection<Incidency> IncidencyCodiTancamentNavigations { get; set; } = new List<Incidency>();
}
