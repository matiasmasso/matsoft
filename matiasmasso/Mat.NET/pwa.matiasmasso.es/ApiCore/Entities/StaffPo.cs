using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employee position
/// </summary>
public partial class StaffPo
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Position name, Spanish language
    /// </summary>
    public string? NomEsp { get; set; }

    /// <summary>
    /// Position name, Catalan language
    /// </summary>
    public string? NomCat { get; set; }

    /// <summary>
    /// Position name, English language
    /// </summary>
    public string? NomEng { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public int? Ord { get; set; }

    public virtual ICollection<CliStaff> CliStaffs { get; set; } = new List<CliStaff>();
}
