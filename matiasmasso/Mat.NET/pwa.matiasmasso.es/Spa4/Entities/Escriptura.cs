using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Notarial deeds
    /// </summary>
    public partial class Escriptura
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Notary; foreign key for CliGral table
        /// </summary>
        public Guid? Notari { get; set; }
        /// <summary>
        /// Document number
        /// </summary>
        public int NumProtocol { get; set; }
        /// <summary>
        /// Effective date
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        public DateTime? FchTo { get; set; }
        /// <summary>
        /// Commercial Register; foreign key for CliGral table
        /// </summary>
        public Guid? RegistreMercantil { get; set; }
        /// <summary>
        /// Commercial Register Book
        /// </summary>
        public int Tomo { get; set; }
        /// <summary>
        /// Commercial Register Page
        /// </summary>
        public int Folio { get; set; }
        /// <summary>
        /// Commercial Register Sheet number
        /// </summary>
        public string Hoja { get; set; } = null!;
        /// <summary>
        /// Commercial Register Record number
        /// </summary>
        public int Inscripcio { get; set; }
        /// <summary>
        /// Purpose. Enumerable DTOEscriptura.Codis
        /// </summary>
        public int Codi { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Document. Foreign key for Docfile table
        /// </summary>
        public string? Hash { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual DocFile? HashNavigation { get; set; }
        public virtual CliGral? NotariNavigation { get; set; }
        public virtual CliGral? RegistreMercantilNavigation { get; set; }
    }
}
