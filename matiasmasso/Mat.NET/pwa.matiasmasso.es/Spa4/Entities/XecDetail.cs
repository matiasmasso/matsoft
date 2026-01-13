using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Debtor debts paid by check
    /// </summary>
    public partial class XecDetail
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent Xec table
        /// </summary>
        public Guid Xec { get; set; }
        /// <summary>
        /// Debt details; foreign key to Pnd table
        /// </summary>
        public Guid? PndGuid { get; set; }

        public virtual Pnd? PndGu { get; set; }
        public virtual Xec XecNavigation { get; set; } = null!;
    }
}
