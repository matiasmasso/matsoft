using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Schedules used by Matsched Windows Service to run automated tasks
/// </summary>
public partial class TaskSchedule
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Parent Task table
    /// </summary>
    public Guid Task { get; set; }

    /// <summary>
    /// If false this scheduled will be skipped
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// If mode is set to DTOTaskSchedule.Modes.GivenTime, the task will run each active date at time set at Hour and Minutes fields
    /// </summary>
    public int? Hour { get; set; }

    /// <summary>
    /// If mode is set to DTOTaskSchedule.Modes.GivenTime, the task will run each active date at time set at Hour and Minutes fields
    /// </summary>
    public int? Minute { get; set; }

    /// <summary>
    /// If mode is set to DTOTaskSchedules.Modes.Interval, the task will run every number of minutes set at this field
    /// </summary>
    public int? Interval { get; set; }

    /// <summary>
    /// Seven digits one or zero to enable or disable weekdays starting with Sunday
    /// </summary>
    public string Weekdays { get; set; } = null!;

    /// <summary>
    /// Enumerable DTOTaskSchedules.Modes (0.GivenTime, 1.Interval)
    /// </summary>
    public int Mode { get; set; }

    /// <summary>
    /// ISO8601 Duration time interval
    /// </summary>
    public string? TimeInterval { get; set; }

    public virtual Task TaskNavigation { get; set; } = null!;
}
