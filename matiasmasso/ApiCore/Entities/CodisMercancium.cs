using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Customs codes for products
/// </summary>
public partial class CodisMercancium
{
    /// <summary>
    /// Customs code
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Description of products within this code
    /// </summary>
    public string? Dsc { get; set; }

    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();

    public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; } = new List<IntrastatPartidum>();

    public virtual ICollection<Stp> Stps { get; set; } = new List<Stp>();

    public virtual ICollection<Tpa> Tpas { get; set; } = new List<Tpa>();
}
