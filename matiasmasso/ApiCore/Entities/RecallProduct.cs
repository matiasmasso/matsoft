using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Products recalled from customer
/// </summary>
public partial class RecallProduct
{
    /// <summary>
    /// Foreign key to parent table RecallCli
    /// </summary>
    public Guid RecallCli { get; set; }

    /// <summary>
    /// Product serial number
    /// </summary>
    public string SerialNumber { get; set; } = null!;

    /// <summary>
    /// Product; foreign key for either brand Tpa table, category Stp table or product Art table
    /// </summary>
    public Guid Sku { get; set; }

    public virtual RecallCli RecallCliNavigation { get; set; } = null!;
}
