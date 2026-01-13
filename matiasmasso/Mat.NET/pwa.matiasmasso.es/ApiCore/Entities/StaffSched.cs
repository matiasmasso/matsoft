using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employees schedule
/// </summary>
public partial class StaffSched
{
    /// <summary>
    /// Primary key; parent for StaffSchedItems table
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company; foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Employee; foreign key for CliGral table
    /// </summary>
    public Guid? Staff { get; set; }

    /// <summary>
    /// Schedule start date
    /// </summary>
    public DateTime FchFrom { get; set; }

    /// <summary>
    /// Schedule termination date
    /// </summary>
    public DateTime? FchTo { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual CliStaff? StaffNavigation { get; set; }

    public virtual ICollection<StaffSchedItem> StaffSchedItems { get; set; } = new List<StaffSchedItem>();
}
