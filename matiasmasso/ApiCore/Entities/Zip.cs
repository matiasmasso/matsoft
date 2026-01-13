using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Postal codes
/// </summary>
public partial class Zip
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to Location table
    /// </summary>
    public Guid Location { get; set; }

    /// <summary>
    /// Postal code
    /// </summary>
    public string ZipCod { get; set; } = null!;

    public virtual ICollection<Alb> Albs { get; set; } = new List<Alb>();

    public virtual ICollection<CliAdr> CliAdrs { get; set; } = new List<CliAdr>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Fra> Fras { get; set; } = new List<Fra>();

    public virtual ICollection<Immoble> Immobles { get; set; } = new List<Immoble>();

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual ICollection<SatRecall> SatRecalls { get; set; } = new List<SatRecall>();

    public virtual ICollection<Spv> Spvs { get; set; } = new List<Spv>();
}
