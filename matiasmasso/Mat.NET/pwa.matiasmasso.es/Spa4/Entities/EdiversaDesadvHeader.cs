using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Delivery notes sent/received by Edi
    /// </summary>
    public partial class EdiversaDesadvHeader
    {
        public EdiversaDesadvHeader()
        {
            EdiversaDesadvItems = new HashSet<EdiversaDesadvItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Document number
        /// </summary>
        public string? Bgm { get; set; }
        /// <summary>
        /// Document date
        /// </summary>
        public DateTime? FchDoc { get; set; }
        /// <summary>
        /// Shipping date
        /// </summary>
        public DateTime? FchShip { get; set; }
        /// <summary>
        /// Order confirmation
        /// </summary>
        public string? Rff { get; set; }
        /// <summary>
        /// EAN 13 code for buyer GLN
        /// </summary>
        public string? NadBy { get; set; }
        /// <summary>
        /// EAN 13 code for supplier GLN
        /// </summary>
        public string? NadSu { get; set; }
        /// <summary>
        /// EAN 13 code for delivery point GLN
        /// </summary>
        public string? NadDp { get; set; }
        /// <summary>
        /// Supplier; foreign key for CliGral table
        /// </summary>
        public Guid? Proveidor { get; set; }
        /// <summary>
        /// Delivery destination (usually our warehouse); foreign key for CliGral table
        /// </summary>
        public Guid? Entrega { get; set; }
        /// <summary>
        /// Purchase order; foreign key for Pdc table
        /// </summary>
        public Guid? PurchaseOrder { get; set; }

        public virtual CliGral? EntregaNavigation { get; set; }
        public virtual Edi Gu { get; set; } = null!;
        public virtual CliGral? ProveidorNavigation { get; set; }
        public virtual Pdc? PurchaseOrderNavigation { get; set; }
        public virtual ICollection<EdiversaDesadvItem> EdiversaDesadvItems { get; set; }
    }
}
