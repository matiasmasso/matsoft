using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Multi purpose codes
/// </summary>
public partial class Cod
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Parent code; foreign key to self Cod table
    /// </summary>
    public Guid? Parent { get; set; }

    /// <summary>
    /// Id to be used by enumerables depending on each parent
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public int? Ord { get; set; }

    /// <summary>
    /// User who created this entry
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Cod> InverseParentNavigation { get; set; } = new List<Cod>();

    public virtual Cod? ParentNavigation { get; set; }

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();

    public virtual Email? UsrCreatedNavigation { get; set; }
}
