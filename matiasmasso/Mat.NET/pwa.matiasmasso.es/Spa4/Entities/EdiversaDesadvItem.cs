using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Delivery note items
    /// </summary>
    public partial class EdiversaDesadvItem
    {
        /// <summary>
        /// Foreign key for parent table EdiversaDesadvHeader
        /// </summary>
        public Guid Desadv { get; set; }
        /// <summary>
        /// Line number
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Product EAN 13 code
        /// </summary>
        public string? Ean { get; set; }
        /// <summary>
        /// Product code
        /// </summary>
        public string? Ref { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// Units delivered
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// Product; foreign key for Art table
        /// </summary>
        public Guid? Sku { get; set; }

        public virtual EdiversaDesadvHeader DesadvNavigation { get; set; } = null!;
        public virtual Art? SkuNavigation { get; set; }
    }
}
