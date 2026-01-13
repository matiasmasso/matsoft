using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer sales reports received through Edi
    /// </summary>
    public partial class EdiversaSalesReportHeader
    {
        public EdiversaSalesReportHeader()
        {
            EdiversaSalesReportItems = new HashSet<EdiversaSalesReportItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Document num
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Document date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Customer; foreign key for CliGral table
        /// </summary>
        public Guid? Customer { get; set; }
        /// <summary>
        /// ISO 4217 Currency code
        /// </summary>
        public string? Cur { get; set; }

        public virtual CliGral? CustomerNavigation { get; set; }
        public virtual ICollection<EdiversaSalesReportItem> EdiversaSalesReportItems { get; set; }
    }
}
