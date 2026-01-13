using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// pre-defined commercial terms published by the International Chamber of Commerce (ICC)
/// </summary>
public partial class Incoterm
{
    /// <summary>
    /// Primary key, Incoterm acronym
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    public string? Nom { get; set; }

    public virtual ICollection<CliClient> CliClients { get; set; } = new List<CliClient>();

    public virtual ICollection<Fra> Fras { get; set; } = new List<Fra>();

    public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; } = new List<IntrastatPartidum>();
}
