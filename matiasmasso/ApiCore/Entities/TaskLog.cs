using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Logs event results from tasks run at Matsched Windows Service
/// </summary>
public partial class TaskLog
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to parent table Task
    /// </summary>
    public Guid Task { get; set; }

    /// <summary>
    /// Event date and time
    /// </summary>
    public DateTimeOffset Fch { get; set; }

    /// <summary>
    /// Enumerable DTOTask.ResultCods (0.Running, 1.Success, 2.Empty, 3.DoneWithErrors, 4.Failed)
    /// </summary>
    public int ResultCod { get; set; }

    /// <summary>
    /// Optional text describing the result
    /// </summary>
    public string? ResultMsg { get; set; }

    public virtual Task TaskNavigation { get; set; } = null!;
}
