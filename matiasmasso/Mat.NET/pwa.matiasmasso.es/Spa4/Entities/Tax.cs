using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Taxes
    /// </summary>
    public partial class Tax
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DTOTax.Codis (VAT, Irpf...)
        /// </summary>
        public int Codi { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Tax rate
        /// </summary>
        public decimal Tipus { get; set; }
    }
}
