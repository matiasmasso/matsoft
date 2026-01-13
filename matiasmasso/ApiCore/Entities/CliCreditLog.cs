using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Customer credit updates
/// </summary>
public partial class CliCreditLog
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Customer; foreign key for CliGral table
    /// </summary>
    public Guid CliGuid { get; set; }

    /// <summary>
    /// Credit authorised
    /// </summary>
    public decimal? Eur { get; set; }

    /// <summary>
    /// Enumerable DTOCliCreditLog.Cods (1.cancelled due to excess of time with no orders)
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// Comments justifying why the customer has a new credit amount
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User who created this entry
    /// </summary>
    public Guid? UserCreated { get; set; }

    /// <summary>
    /// Date this entry was edited for last time
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    /// <summary>
    /// User who edited this entry for last time
    /// </summary>
    public Guid? UserLastEdited { get; set; }

    public virtual CliGral Cli { get; set; } = null!;

    public virtual Email? UserCreatedNavigation { get; set; }

    public virtual Email? UserLastEditedNavigation { get; set; }
}
