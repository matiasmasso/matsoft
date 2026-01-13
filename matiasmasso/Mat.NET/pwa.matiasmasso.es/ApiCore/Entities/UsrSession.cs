using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// User sessions on our corporate Apps and websites
/// </summary>
public partial class UsrSession
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Init session date and time
    /// </summary>
    public DateTime FchFrom { get; set; }

    /// <summary>
    /// End session date and time
    /// </summary>
    public DateTime? FchTo { get; set; }

    /// <summary>
    /// ISO 639-1 user language code
    /// </summary>
    public string Lang { get; set; } = null!;

    /// <summary>
    /// Enumerable DTOApp.AppTypes
    /// </summary>
    public int? AppType { get; set; }

    /// <summary>
    /// System.Globalization.CultureInfo.name
    /// </summary>
    public string? Culture { get; set; }

    /// <summary>
    /// Session user; foreign key for Email table
    /// </summary>
    public Guid? Usuari { get; set; }

    /// <summary>
    /// Application version the user has initiated session in
    /// </summary>
    public string? AppVersion { get; set; }

    public virtual Email? UsuariNavigation { get; set; }
}
