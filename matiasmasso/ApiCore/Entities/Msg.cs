using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Messages sent by App users
/// </summary>
public partial class Msg
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Message sequential number
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Message body
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User semding the message
    /// </summary>
    public Guid UsrCreated { get; set; }

    public int Src { get; set; }

    public virtual Email UsrCreatedNavigation { get; set; } = null!;
}
