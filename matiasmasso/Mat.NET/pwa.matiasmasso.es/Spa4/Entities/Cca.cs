using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Account entries
    /// </summary>
    public partial class Cca
    {
        public Cca()
        {
            Ccbs = new HashSet<Ccb>();
            Csas = new HashSet<Csa>();
            EdiRemadvHeaders = new HashSet<EdiRemadvHeader>();
            Fras = new HashSet<Fra>();
            Impagats = new HashSet<Impagat>();
            Mr2s = new HashSet<Mr2>();
            MrtAltaCcaNavigations = new HashSet<Mrt>();
            MrtBaixaCcaNavigations = new HashSet<Mrt>();
            XecCcaPresentacioNavigations = new HashSet<Xec>();
            XecCcaRebutNavigations = new HashSet<Xec>();
            XecCcaVtoNavigations = new HashSet<Xec>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Date year
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Account entry number; unique for each combination of Company and year
        /// </summary>
        public int Cca1 { get; set; }
        /// <summary>
        /// Concept, free text
        /// </summary>
        public string? Txt { get; set; }
        /// <summary>
        /// Entry date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Sort number (for example invoice number on invoices which help to sort invoice entries of same day in the right order)
        /// </summary>
        public int? Cdn { get; set; }
        /// <summary>
        /// Enumerable DTOCca.CcdEnum for entry purpose, used on sorting together with Cdn field
        /// </summary>
        public int? Ccd { get; set; }
        /// <summary>
        /// Project when applicable; foreign key to Projecte
        /// </summary>
        public Guid? Projecte { get; set; }
        /// <summary>
        /// Reference when applicable; foreign key to different tables depending on type of entry
        /// </summary>
        public Guid? Ref { get; set; }
        /// <summary>
        /// Hash of support document when applicable, foreign key for Docfile table
        /// </summary>
        public string? Hash { get; set; }
        /// <summary>
        /// Date and time the entry was registered
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Date and time the entry was last updated
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who registered the entry; foreign key for Email table
        /// </summary>
        public Guid? UsrCreatedGuid { get; set; }
        /// <summary>
        /// User who updated this entry for last time, foreign key for Email table
        /// </summary>
        public Guid? UsrLastEditedGuid { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual DocFile? HashNavigation { get; set; }
        public virtual Projecte? ProjecteNavigation { get; set; }
        public virtual Email? UsrCreatedGu { get; set; }
        public virtual Email? UsrLastEditedGu { get; set; }
        public virtual BancTransferPool BancTransferPool { get; set; } = null!;
        public virtual BookFra BookFra { get; set; } = null!;
        public virtual Nomina Nomina { get; set; } = null!;
        public virtual ICollection<Ccb> Ccbs { get; set; }
        public virtual ICollection<Csa> Csas { get; set; }
        public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaders { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<Impagat> Impagats { get; set; }
        public virtual ICollection<Mr2> Mr2s { get; set; }
        public virtual ICollection<Mrt> MrtAltaCcaNavigations { get; set; }
        public virtual ICollection<Mrt> MrtBaixaCcaNavigations { get; set; }
        public virtual ICollection<Xec> XecCcaPresentacioNavigations { get; set; }
        public virtual ICollection<Xec> XecCcaRebutNavigations { get; set; }
        public virtual ICollection<Xec> XecCcaVtoNavigations { get; set; }
    }
}
