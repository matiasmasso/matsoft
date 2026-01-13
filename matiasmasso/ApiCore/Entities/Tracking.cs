using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Events tracking for reports to customers for example about after sales service  status
/// </summary>
public partial class Tracking
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Target object, for example an after sales service tracking would be a foreign key for Incidencies table
    /// </summary>
    public Guid Target { get; set; }

    /// <summary>
    /// Foreign key for Cod table describing the event tracked
    /// </summary>
    public Guid Cod { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Event date
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User triggering the event
    /// </summary>
    public Guid UsrCreated { get; set; }

    public virtual Cod CodNavigation { get; set; } = null!;

    public virtual Email UsrCreatedNavigation { get; set; } = null!;
}
