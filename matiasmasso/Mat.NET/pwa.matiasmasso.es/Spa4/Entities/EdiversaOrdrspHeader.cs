using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Order confirmations sent or received through Edi
    /// </summary>
    public partial class EdiversaOrdrspHeader
    {
        public EdiversaOrdrspHeader()
        {
            EdiversaOrdrspItems = new HashSet<EdiversaOrdrspItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Purchase order which this document is confirming
        /// </summary>
        public Guid EdiversaOrder { get; set; }
        /// <summary>
        /// Document date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Edi EdiversaOrderNavigation { get; set; } = null!;
        public virtual Edi Gu { get; set; } = null!;
        public virtual ICollection<EdiversaOrdrspItem> EdiversaOrdrspItems { get; set; }
    }
}
