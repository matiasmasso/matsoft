using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Agent customers with specific commission terms 
/// </summary>
public partial class RepCliCom
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Commercial agent, foreign key to CliRep table
    /// </summary>
    public Guid Rep { get; set; }

    /// <summary>
    /// Customer, foreign key to CliGral table
    /// </summary>
    public Guid Cli { get; set; }

    /// <summary>
    /// Enumerable DTORepCliCom.Cods: 0.Standard, 1.Reduced, 2.Excluded
    /// </summary>
    public int ComCod { get; set; }

    /// <summary>
    /// Date of agreement
    /// </summary>
    public DateTime Fch { get; set; }

    /// <summary>
    /// Comments to explain the reason of special conditions
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// User who registered this entry. Foreign key to Email table
    /// </summary>
    public Guid UsrCreated { get; set; }

    /// <summary>
    /// Date of entry creation
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual CliClient CliNavigation { get; set; } = null!;

    public virtual CliRep RepNavigation { get; set; } = null!;

    public virtual Email UsrCreatedNavigation { get; set; } = null!;
}
