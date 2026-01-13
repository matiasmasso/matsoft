using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Repair reports from workshop
/// </summary>
public partial class Spv
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
    /// Year of the report
    /// </summary>
    public short Yea { get; set; }

    /// <summary>
    /// Report Id, unique within Company/Year
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Customer; foreign key for CliGral table
    /// </summary>
    public Guid? CliGuid { get; set; }

    /// <summary>
    /// Customer destination name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Destination address
    /// </summary>
    public string? Adr { get; set; }

    /// <summary>
    /// Destination zip code id; foreign key to Zip table
    /// </summary>
    public Guid? Zip { get; set; }

    /// <summary>
    /// Date we ask the customer to pick upo the goods to repair
    /// </summary>
    public DateTime FchAvis { get; set; }

    /// <summary>
    /// Customer contact person
    /// </summary>
    public string? Contacto { get; set; }

    /// <summary>
    /// Customer contact phone
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// Customer reference, if any
    /// </summary>
    public string Sref { get; set; } = null!;

    /// <summary>
    /// Product; foreign key for either the brand Tpa table, category Stp table or product sku table
    /// </summary>
    public Guid? ProductGuid { get; set; }

    /// <summary>
    /// Product serial number, if applicable
    /// </summary>
    public string? Serial { get; set; }

    /// <summary>
    /// Product manufacture date, if available
    /// </summary>
    public string? ManufactureDate { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Recipient to deliver the labels to pickup the goods
    /// </summary>
    public string? LabelEmailedTo { get; set; }

    /// <summary>
    /// Date the workshop has read the job order
    /// </summary>
    public DateTime? FchRead { get; set; }

    /// <summary>
    /// Date the workshop received the product to repair
    /// </summary>
    public DateTime? FchRecepcio { get; set; }

    /// <summary>
    /// Date the workshop delivered the repaired product
    /// </summary>
    public DateTime? FchSortida { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Job value
    /// </summary>
    public decimal ValJob { get; set; }

    /// <summary>
    /// Transport value
    /// </summary>
    public decimal ValPorts { get; set; }

    /// <summary>
    /// Value of any spares replaced
    /// </summary>
    public decimal ValSpares { get; set; }

    /// <summary>
    /// Value of packaging
    /// </summary>
    public decimal ValEmbalatje { get; set; }

    /// <summary>
    /// True if the customer requests to be repaired under warranty
    /// </summary>
    public byte SolicitaGarantia { get; set; }

    /// <summary>
    /// True if the defect is covered by the warranty
    /// </summary>
    public byte Garantia { get; set; }

    /// <summary>
    /// Technician comments
    /// </summary>
    public string? ObsTecnic { get; set; }

    /// <summary>
    /// User who created this entry; foreign key for Email table
    /// </summary>
    public Guid? UsrRegisterGuid { get; set; }

    /// <summary>
    /// Technician who took care of this job; foreign key for Email table
    /// </summary>
    public Guid? UsrTecnicGuid { get; set; }

    /// <summary>
    /// Incidence; foreign key to Incidencies table
    /// </summary>
    public Guid? Incidencia { get; set; }

    /// <summary>
    /// Product reception; foreign key for SpvIn table
    /// </summary>
    public Guid? SpvIn { get; set; }

    /// <summary>
    /// Delivery; foreign key for Alb table
    /// </summary>
    public Guid? AlbGuid { get; set; }

    /// <summary>
    /// User who removed this entry from pending to receive if this is the case; foreign key for Email table
    /// </summary>
    public Guid? UsrOutOfSpvInGuid { get; set; }

    /// <summary>
    /// Date in which this entry was removed from pending to receive
    /// </summary>
    public DateTime? FchOutOfSpvIn { get; set; }

    /// <summary>
    /// Comments why this entry was removed from pending to receive
    /// </summary>
    public string? ObsOutOfSpvIn { get; set; }

    /// <summary>
    /// User who removed this entry from pending to repair if this is the case; foreign key for Email table
    /// </summary>
    public Guid? UsrOutOfSpvOutGuid { get; set; }

    /// <summary>
    /// Date in which this entry was removed from pending to repair
    /// </summary>
    public DateTime? FchOutOfSpvOut { get; set; }

    /// <summary>
    /// Comments why this entry was removed from pending to repair
    /// </summary>
    public string? ObsOutOfSpvOut { get; set; }

    public virtual Alb? Alb { get; set; }

    public virtual CliGral? Cli { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual Incidency? IncidenciaNavigation { get; set; }

    public virtual ICollection<Incidency> Incidencies { get; set; } = new List<Incidency>();

    public virtual SpvIn? SpvInNavigation { get; set; }

    public virtual Email? UsrOutOfSpvIn { get; set; }

    public virtual Email? UsrOutOfSpvOut { get; set; }

    public virtual Email? UsrRegister { get; set; }

    public virtual Email? UsrTecnic { get; set; }

    public virtual Zip? ZipNavigation { get; set; }
}
