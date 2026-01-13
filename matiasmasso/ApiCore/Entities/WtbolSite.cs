using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Wtbol project is the algorithm that recommends a limited number of online retailers to quickly convert sales when visitors browse our products in our website or the manufacturer website. Hatch is an external service manufacturers are subscribed to apply same functionality on their websites. Hatch gets retailers data from our Api. WtbolSite table stores the properties of affiliated online retailers
/// </summary>
public partial class WtbolSite
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key for CliGral table
    /// </summary>
    public Guid Customer { get; set; }

    /// <summary>
    /// Retailer name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Retailer website
    /// </summary>
    public string Web { get; set; } = null!;

    /// <summary>
    /// Id assigned by Hatch to refer to this affiliated
    /// </summary>
    public string? MerchantId { get; set; }

    /// <summary>
    /// Contact person name
    /// </summary>
    public string? ContactNom { get; set; }

    /// <summary>
    /// Contact person email
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// Contact person phone
    /// </summary>
    public string? ContactTel { get; set; }

    /// <summary>
    /// True if currently active
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Retailer logo, 150x48 pixels
    /// </summary>
    public byte[]? Logo { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User who created this entry, foreignn key for Email table
    /// </summary>
    public Guid UsrCreated { get; set; }

    /// <summary>
    /// Date this entry was edited for last time
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    /// <summary>
    /// User who edited this entry for last time
    /// </summary>
    public Guid UsrLastEdited { get; set; }

    public virtual CliGral CustomerNavigation { get; set; } = null!;

    public virtual Email UsrCreatedNavigation { get; set; } = null!;

    public virtual Email UsrLastEditedNavigation { get; set; } = null!;

    public virtual ICollection<WtbolBasket> WtbolBaskets { get; set; } = new List<WtbolBasket>();

    public virtual ICollection<WtbolCtr> WtbolCtrs { get; set; } = new List<WtbolCtr>();

    public virtual ICollection<WtbolLandingPage> WtbolLandingPages { get; set; } = new List<WtbolLandingPage>();

    public virtual ICollection<WtbolSerpItem> WtbolSerpItems { get; set; } = new List<WtbolSerpItem>();

    public virtual ICollection<WtbolStock> WtbolStocks { get; set; } = new List<WtbolStock>();
}
