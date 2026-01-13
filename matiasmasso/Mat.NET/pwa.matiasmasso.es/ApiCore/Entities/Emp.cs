using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Company
/// </summary>
public partial class Emp
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Emp1 { get; set; }

    /// <summary>
    /// Company name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Company friendly name
    /// </summary>
    public string Abr { get; set; } = null!;

    /// <summary>
    /// Company properties; foreign key for CliGral table
    /// </summary>
    public Guid? Org { get; set; }

    public DateOnly? FchFrom { get; set; }

    public DateOnly? FchTo { get; set; }

    /// <summary>
    /// Warehouse; fore¡gn key for CliGral table
    /// </summary>
    public Guid? Mgz { get; set; }

    /// <summary>
    /// Workshop; foreign key for CliGral table
    /// </summary>
    public Guid? Taller { get; set; }

    /// <summary>
    /// Company activity code (Clasificación Nacional de Actividades Económicas)
    /// </summary>
    public int? Cnae { get; set; }

    /// <summary>
    /// Commercial Register details, free text. Used on Edi
    /// </summary>
    public string? DadesRegistrals { get; set; }

    /// <summary>
    /// Internet domain
    /// </summary>
    public string? Domini { get; set; }

    /// <summary>
    /// Corporate email address
    /// </summary>
    public string? MsgFrom { get; set; }

    /// <summary>
    /// Mailbox user name
    /// </summary>
    public string? MailboxUsr { get; set; }

    /// <summary>
    /// Maibox password
    /// </summary>
    public string? MailboxPwd { get; set; }

    /// <summary>
    /// Mailbox Smtp server
    /// </summary>
    public string MailboxSmtp { get; set; } = null!;

    /// <summary>
    /// Mailbox Smtp port
    /// </summary>
    public int MailboxPort { get; set; }

    public string? Ip { get; set; }

    public Guid Guid { get; set; }

    public byte[]? CertData { get; set; }

    public int? CertMime { get; set; }

    public byte[]? CertImage { get; set; }

    public int? CertImageMime { get; set; }

    public string? CertPwd { get; set; }

    public DateOnly? CertFchTo { get; set; }

    public int? Ord { get; set; }

    public virtual ICollection<Aeat> Aeats { get; set; } = new List<Aeat>();

    public virtual ICollection<Alb> Albs { get; set; } = new List<Alb>();

    public virtual ICollection<Cca> Ccas { get; set; } = new List<Cca>();

    public virtual ICollection<CliGral> CliGrals { get; set; } = new List<CliGral>();

    public virtual ICollection<Crr> Crrs { get; set; } = new List<Crr>();

    public virtual ICollection<DeliveryTracking> DeliveryTrackings { get; set; } = new List<DeliveryTracking>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Fra> Fras { get; set; } = new List<Fra>();

    public virtual ICollection<Holding> Holdings { get; set; } = new List<Holding>();

    public virtual ICollection<Immoble> Immobles { get; set; } = new List<Immoble>();

    public virtual ICollection<ImportHdr> ImportHdrs { get; set; } = new List<ImportHdr>();

    public virtual ICollection<Intrastat> Intrastats { get; set; } = new List<Intrastat>();

    public virtual ICollection<Noticium> Noticia { get; set; } = new List<Noticium>();

    public virtual ICollection<Pdc> Pdcs { get; set; } = new List<Pdc>();

    public virtual ICollection<SpvIn> SpvIns { get; set; } = new List<SpvIn>();

    public virtual ICollection<Spv> Spvs { get; set; } = new List<Spv>();

    public virtual ICollection<Ssc> Sscs { get; set; } = new List<Ssc>();

    public virtual ICollection<StaffSched> StaffScheds { get; set; } = new List<StaffSched>();

    public virtual ICollection<Tpa> Tpas { get; set; } = new List<Tpa>();

    public virtual ICollection<Transm> Transms { get; set; } = new List<Transm>();

    public virtual ICollection<VehicleFlotum> VehicleFlota { get; set; } = new List<VehicleFlotum>();

    public virtual ICollection<Yea> Yeas { get; set; } = new List<Yea>();

    public virtual ICollection<Nav> Navs { get; set; } = new List<Nav>();
}
