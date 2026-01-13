using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employees workday
/// </summary>
public partial class StaffSchedItem
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key for parent StaffSched table
    /// </summary>
    public Guid StaffSched { get; set; }

    /// <summary>
    /// Enumerable DTOStaffSched.Item.Cods (0.Standard, 1.Intensive)
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// Day of the week, starting with 0.Sunday
    /// </summary>
    public int Weekday { get; set; }

    /// <summary>
    /// Hour when the workday starts
    /// </summary>
    public int FromHour { get; set; }

    /// <summary>
    /// Minute when the workday starts
    /// </summary>
    public int FromMinutes { get; set; }

    /// <summary>
    /// Hour when the workday ends
    /// </summary>
    public int ToHour { get; set; }

    /// <summary>
    /// Minute when the workday ends
    /// </summary>
    public int ToMinutes { get; set; }

    public virtual StaffSched StaffSchedNavigation { get; set; } = null!;
}
