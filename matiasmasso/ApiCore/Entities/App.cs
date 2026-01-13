using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Corporate Apps from Matsoft
/// </summary>
public partial class App
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// App name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Last version available
    /// </summary>
    public string? LastVersion { get; set; }

    /// <summary>
    /// Minim version a user is allowed to run
    /// </summary>
    public string? MinVersion { get; set; }

    public virtual ICollection<AppUsrLog> AppUsrLogs { get; set; } = new List<AppUsrLog>();
}
