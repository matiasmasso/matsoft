using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contact comments and visit reports
    /// </summary>
    public partial class Mem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key for CliGral table
        /// </summary>
        public Guid? Contact { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Comments, visit reports, etc
        /// </summary>
        public string? Mem1 { get; set; }
        /// <summary>
        /// Enumerable DTOMem.Cods
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Entry creation date
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who created this entry
        /// </summary>
        public Guid? UsrCreated { get; set; }

        public virtual CliGral? ContactNavigation { get; set; }
        public virtual Email? UsrCreatedNavigation { get; set; }
    }
}
