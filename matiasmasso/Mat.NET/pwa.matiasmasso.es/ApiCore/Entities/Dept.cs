using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product departments, to group brand categories into functional groups more intuitive for consumers
/// </summary>
public partial class Dept
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Product brand. foreign key to Tpa table
    /// </summary>
    public Guid Brand { get; set; }

    /// <summary>
    /// Department sort order
    /// </summary>
    public int Ord { get; set; }

    /// <summary>
    /// Image ilustrating the department on websites
    /// </summary>
    public byte[]? Banner { get; set; }

    /// <summary>
    /// Record creation date
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Date this record was edited for last time
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    /// <summary>
    /// Outdated if true
    /// </summary>
    public bool Obsoleto { get; set; }

    public virtual Tpa BrandNavigation { get; set; } = null!;

    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();

    public virtual ICollection<Cnap> Cnaps { get; set; } = new List<Cnap>();
}
