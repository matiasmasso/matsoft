using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Customer recall of products
/// </summary>
public partial class RecallCli
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to parent table Recall
    /// </summary>
    public Guid Recall { get; set; }

    /// <summary>
    /// Customer contact person name
    /// </summary>
    public string? ContactNom { get; set; }

    /// <summary>
    /// Customer contact person phone number
    /// </summary>
    public string? ContactTel { get; set; }

    /// <summary>
    /// Customer contact person email address
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// Customer; foreign key to CliGral table
    /// </summary>
    public Guid? Customer { get; set; }

    /// <summary>
    /// Customer pickup address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Customer pickup zip code
    /// </summary>
    public string? Zip { get; set; }

    /// <summary>
    /// Customer pickup location
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Customer pickup country
    /// </summary>
    public Guid? Country { get; set; }

    /// <summary>
    /// Date received at the warehouse
    /// </summary>
    public DateTime? FchVivace { get; set; }

    /// <summary>
    /// Warehouse port log number
    /// </summary>
    public string? RegMuelle { get; set; }

    /// <summary>
    /// Foreign key to Pdc table
    /// </summary>
    public Guid? PurchaseOrder { get; set; }

    /// <summary>
    /// Foreign key to Alb table
    /// </summary>
    public Guid? Delivery { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime? FchCreated { get; set; }

    /// <summary>
    /// User who created this entry
    /// </summary>
    public Guid? UsrCreated { get; set; }

    public virtual Country? CountryNavigation { get; set; }

    public virtual Alb? DeliveryNavigation { get; set; }

    public virtual Pdc? PurchaseOrderNavigation { get; set; }

    public virtual Recall RecallNavigation { get; set; } = null!;

    public virtual ICollection<RecallProduct> RecallProducts { get; set; } = new List<RecallProduct>();

    public virtual Email? UsrCreatedNavigation { get; set; }
}
