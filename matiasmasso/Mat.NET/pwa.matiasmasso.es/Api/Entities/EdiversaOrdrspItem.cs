using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Order confirmation lines
    /// </summary>
    public partial class EdiversaOrdrspItem
    {
        /// <summary>
        /// Purchase order line. Foreign key to EdiversaOrderItem table
        /// </summary>
        public Guid OrderItem { get; set; }
        /// <summary>
        /// Document; foreign key to parent table EdiversaorderSp table
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Units confirmed
        /// </summary>
        public int Qty { get; set; }

        public virtual EdiversaOrderItem OrderItemNavigation { get; set; } = null!;
        public virtual EdiversaOrdrspHeader ParentNavigation { get; set; } = null!;
    }
}
