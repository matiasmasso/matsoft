using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Any subscription and unsubscription event is logged in this table
/// </summary>
public partial class SscLog
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Subscriptor; foreign key for Email table
    /// </summary>
    public Guid Mail { get; set; }

    /// <summary>
    /// Subscription; foreign key for Ssc table
    /// </summary>
    public Guid SscGuid { get; set; }

    /// <summary>
    /// 1. Subscribed, 0.Unsubscribed
    /// </summary>
    public bool Action { get; set; }

    /// <summary>
    /// Event date
    /// </summary>
    public DateTime Fch { get; set; }

    public virtual Email MailNavigation { get; set; } = null!;

    public virtual Ssc Ssc { get; set; } = null!;
}
