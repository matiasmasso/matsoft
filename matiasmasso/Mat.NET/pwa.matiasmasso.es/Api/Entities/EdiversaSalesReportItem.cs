using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer sales report items
    /// </summary>
    public partial class EdiversaSalesReportItem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key for parent table EdiversaSalesReportHeader
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Customer; foreign key for CliGral table
        /// </summary>
        public Guid? Customer { get; set; }
        /// <summary>
        /// Sales date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        public string? Dept { get; set; }
        /// <summary>
        /// Sales center
        /// </summary>
        public string? Centro { get; set; }
        /// <summary>
        /// Product; foreign key for Art table
        /// </summary>
        public Guid? Sku { get; set; }
        /// <summary>
        /// Units sold
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Units returned
        /// </summary>
        public int QtyBack { get; set; }
        /// <summary>
        /// Total sales on this date of this product on this center and department
        /// </summary>
        public decimal? Eur { get; set; }

        public virtual CliGral? CustomerNavigation { get; set; }
        public virtual EdiversaSalesReportHeader ParentNavigation { get; set; } = null!;
        public virtual Art? SkuNavigation { get; set; }
    }
}
