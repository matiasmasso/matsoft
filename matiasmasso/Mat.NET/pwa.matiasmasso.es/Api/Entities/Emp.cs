using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Company
    /// </summary>
    public partial class Emp
    {
        public Emp()
        {
            Aeats = new HashSet<Aeat>();
            Albs = new HashSet<Alb>();
            Ccas = new HashSet<Cca>();
            CliGrals = new HashSet<CliGral>();
            Crrs = new HashSet<Crr>();
            DeliveryTrackings = new HashSet<DeliveryTracking>();
            Emails = new HashSet<Email>();
            Fras = new HashSet<Fra>();
            Holdings = new HashSet<Holding>();
            Immobles = new HashSet<Immoble>();
            ImportHdrs = new HashSet<ImportHdr>();
            Intrastats = new HashSet<Intrastat>();
            Noticia = new HashSet<Noticium>();
            Pdcs = new HashSet<Pdc>();
            SpvIns = new HashSet<SpvIn>();
            Spvs = new HashSet<Spv>();
            Sscs = new HashSet<Ssc>();
            StaffScheds = new HashSet<StaffSched>();
            Tpas = new HashSet<Tpa>();
            Transms = new HashSet<Transm>();
            VehicleFlota = new HashSet<VehicleFlotum>();
            Yeas = new HashSet<Yea>();
        }

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

        public virtual ICollection<Aeat> Aeats { get; set; }
        public virtual ICollection<Alb> Albs { get; set; }
        public virtual ICollection<Cca> Ccas { get; set; }
        public virtual ICollection<CliGral> CliGrals { get; set; }
        public virtual ICollection<Crr> Crrs { get; set; }
        public virtual ICollection<DeliveryTracking> DeliveryTrackings { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<Holding> Holdings { get; set; }
        public virtual ICollection<Immoble> Immobles { get; set; }
        public virtual ICollection<ImportHdr> ImportHdrs { get; set; }
        public virtual ICollection<Intrastat> Intrastats { get; set; }
        public virtual ICollection<Noticium> Noticia { get; set; }
        public virtual ICollection<Pdc> Pdcs { get; set; }
        public virtual ICollection<SpvIn> SpvIns { get; set; }
        public virtual ICollection<Spv> Spvs { get; set; }
        public virtual ICollection<Ssc> Sscs { get; set; }
        public virtual ICollection<StaffSched> StaffScheds { get; set; }
        public virtual ICollection<Tpa> Tpas { get; set; }
        public virtual ICollection<Transm> Transms { get; set; }
        public virtual ICollection<VehicleFlotum> VehicleFlota { get; set; }
        public virtual ICollection<Yea> Yeas { get; set; }
    }
}
