using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Postsales incidence reports
/// </summary>
public partial class Incidency
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company; foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Sequential number per company
    /// </summary>
    public int Id { get; set; }

    public string? Asin { get; set; }

    /// <summary>
    /// Enumerable DTOIncidencia.Srcs (1.Product, 2.Transport)
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// Date of acknowledgement
    /// </summary>
    public DateTime Fch { get; set; }

    /// <summary>
    /// Customer; foreign key for CliGral table
    /// </summary>
    public Guid? ContactGuid { get; set; }

    /// <summary>
    /// Enumerable DTOIncidencia.ContactTypes (1.Professional, 2.Consumer)
    /// </summary>
    public int ContactType { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Cause code; foreign key for IncidenciesCods table
    /// </summary>
    public Guid? CodiApertura { get; set; }

    /// <summary>
    /// Closure code; foreign key for IncidenciesCods table
    /// </summary>
    public Guid? CodiTancament { get; set; }

    /// <summary>
    /// Date of closure
    /// </summary>
    public DateTime? FchClose { get; set; }

    /// <summary>
    /// Name of the person who notified the incidence
    /// </summary>
    public string? Person { get; set; }

    /// <summary>
    /// Email of the person who notified the incidence
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Phone number of the person who notified the incidence
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// Customer reference, if any
    /// </summary>
    public string? SRef { get; set; }

    /// <summary>
    /// Product object of the claim; foreign key to either the brand Tpa table, the category Stp table or the product Sku table
    /// </summary>
    public Guid? ProductGuid { get; set; }

    /// <summary>
    /// Serial number of the product, if applicable
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Manufacture date of the product, if any
    /// </summary>
    public string? ManufactureDate { get; set; }

    /// <summary>
    /// Enumerable DTOIncidencia.Procedencias (1.Purchased from my shop, 2.Purchased from other shops, 3.Not sold yet, it comes from my exposition or my warehouse)
    /// </summary>
    public int Procedencia { get; set; }

    /// <summary>
    /// Acquisition date
    /// </summary>
    public DateOnly? FchCompra { get; set; }

    public string? BoughtFrom { get; set; }

    /// <summary>
    /// Foreign key to Spv table, if the product is sent to repair
    /// </summary>
    public Guid? SpvGuid { get; set; }

    /// <summary>
    /// User who created this entry; foreign key to Email table
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// User who edited this entry for last time
    /// </summary>
    public Guid? UsrLastEdited { get; set; }

    /// <summary>
    /// Date this entry was edited for last time
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    public virtual IncidenciesCod? CodiAperturaNavigation { get; set; }

    public virtual IncidenciesCod? CodiTancamentNavigation { get; set; }

    public virtual ICollection<IncidenciaDocFile> IncidenciaDocFiles { get; set; } = new List<IncidenciaDocFile>();

    public virtual ICollection<SatRecall> SatRecalls { get; set; } = new List<SatRecall>();

    public virtual Spv? Spv { get; set; }

    public virtual ICollection<Spv> Spvs { get; set; } = new List<Spv>();

    public virtual Email? UsrCreatedNavigation { get; set; }

    public virtual Email? UsrLastEditedNavigation { get; set; }
}
