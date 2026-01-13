using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Landing page retailers declare where they display each of our products so we can display their link under &quot;Purchase now&quot; to consumers browsing our catalog (Wtbol project)
/// </summary>
public partial class WtbolLandingPage
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Affiliated eCommerce selling our products; foreign key to WtbolSite table
    /// </summary>
    public Guid Site { get; set; }

    /// <summary>
    /// Foreign key to Art table
    /// </summary>
    public Guid Product { get; set; }

    /// <summary>
    /// Landing page addressof this priduct on affiliated eCommerce site
    /// </summary>
    public string Url { get; set; } = null!;

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User who uploaded this landing page
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// Date and time the last status was set
    /// </summary>
    public DateTime? FchStatus { get; set; }

    /// <summary>
    /// User who set last status; foreign  key for Email table
    /// </summary>
    public Guid? UsrStatus { get; set; }

    /// <summary>
    /// DTOWtboEnumerable DTOWtbolLandingPage.StatusEnum (0.Pending, 1.Approved, 2.Denied)lLandingPage
    /// </summary>
    public int Status { get; set; }

    public virtual WtbolSite SiteNavigation { get; set; } = null!;
}
