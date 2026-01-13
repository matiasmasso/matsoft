using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Warehouse reception of customer returns
    /// </summary>
    public partial class CliReturn
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Customer; foreign key for CliGral table
        /// </summary>
        public Guid Cli { get; set; }
        /// <summary>
        /// Warehouse; foreign key for CliGral table
        /// </summary>
        public Guid Mgz { get; set; }
        /// <summary>
        /// Reception date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Warehouse reception reference
        /// </summary>
        public string? RefMgz { get; set; }
        /// <summary>
        /// Number of packages
        /// </summary>
        public int Bultos { get; set; }
        /// <summary>
        /// Packing list; foreign key for Alb table
        /// </summary>
        public Guid? Entrada { get; set; }
        /// <summary>
        /// Return authorisation code
        /// </summary>
        public string? Auth { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// User who created this entry; foreign key for Email table
        /// </summary>
        public Guid UsrCreated { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who edited this entry for last time
        /// </summary>
        public Guid UsrLastEdited { get; set; }
        /// <summary>
        /// Date this entry was edited last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }

        public virtual CliGral CliNavigation { get; set; } = null!;
        public virtual Alb? EntradaNavigation { get; set; }
        public virtual CliGral MgzNavigation { get; set; } = null!;
        public virtual Email UsrCreatedNavigation { get; set; } = null!;
        public virtual Email UsrLastEditedNavigation { get; set; } = null!;
    }
}
