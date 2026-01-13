using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Delivery platforms available per customer
/// </summary>
public partial class CustomerPlatform
{
    /// <summary>
    /// Platform; foreign key for CliGral
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Customer owner of the platform; foreign key for CliGral
    /// </summary>
    public Guid Customer { get; set; }

    public virtual CliGral CustomerNavigation { get; set; } = null!;

    public virtual ICollection<CustomerPlatformDestination> CustomerPlatformDestinations { get; set; } = new List<CustomerPlatformDestination>();

    public virtual CliGral Gu { get; set; } = null!;
}
