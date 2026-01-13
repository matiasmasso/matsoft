using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Log of apps launched by users
/// </summary>
public partial class AppUsrLog
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Log date
    /// </summary>
    public DateTime Fch { get; set; }

    public DateTime? FchTo { get; set; }

    /// <summary>
    /// App id, foreign key for App table
    /// </summary>
    public int App { get; set; }

    /// <summary>
    /// App version
    /// </summary>
    public string? AppVersion { get; set; }

    /// <summary>
    /// user Operative system
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// user device model
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// user device id
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// User, foreign key to Email table
    /// </summary>
    public Guid? Usr { get; set; }

    public virtual App AppNavigation { get; set; } = null!;

    public virtual Email? UsrNavigation { get; set; }
}
