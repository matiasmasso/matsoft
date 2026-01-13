using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Bank balance. Every day we log the balance as appears on the bank website as a base for account conciliation
    /// </summary>
    public partial class BancSdo
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Bank account, foreign key for CliGral
        /// </summary>
        public Guid Banc { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Sdo { get; set; }
    }
}
