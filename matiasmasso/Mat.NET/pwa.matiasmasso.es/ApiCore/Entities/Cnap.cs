using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product classification by function. Clasificacion Normalizada de Artículos de Puericultura
/// </summary>
public partial class Cnap
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Parent record; foreign key to self table
    /// </summary>
    public Guid? Parent { get; set; }

    /// <summary>
    /// Classification Id, it takes its first digits from parent and adds a last digit
    /// </summary>
    public string Id { get; set; } = null!;

    public string? Tags { get; set; }

    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();

    public virtual ICollection<Cnap> InverseParentNavigation { get; set; } = new List<Cnap>();

    public virtual ICollection<Noticium> Noticia { get; set; } = new List<Noticium>();

    public virtual Cnap? ParentNavigation { get; set; }

    public virtual ICollection<Stp> Stps { get; set; } = new List<Stp>();

    public virtual ICollection<Tpa> Tpas { get; set; } = new List<Tpa>();

    public virtual ICollection<Dept> Depts { get; set; } = new List<Dept>();
}
