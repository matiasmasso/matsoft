using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Those contacts who are Companies may need to list individual contact person names with their positions. This table is the place to store them
/// </summary>
public partial class CliContact
{
    /// <summary>
    /// Foreign key for CliGral table
    /// </summary>
    public Guid ContactGuid { get; set; }

    /// <summary>
    /// Sequential sort order id within each Contact
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Contact person name
    /// </summary>
    public string? Contact { get; set; }

    public virtual CliGral ContactNavigation { get; set; } = null!;
}
