using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Amortisation quotes, accounting depreciations
    /// </summary>
    public partial class Mr2
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Amortisation item, foreign key for Mrt table
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Date of this entry
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Amortisation rate
        /// </summary>
        public decimal Tipus { get; set; }
        /// <summary>
        /// Amortisation amount, in foreign currency
        /// </summary>
        public decimal Pts { get; set; }
        /// <summary>
        /// Amortisation amount, in Euro
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Accounting entry
        /// </summary>
        public Guid? Cca { get; set; }
        /// <summary>
        /// Enumerable DTOAmortizationItem.Cods: 0.Amortització, 1.Baixa
        /// </summary>
        public short Cod { get; set; }

        public virtual Cca? CcaNavigation { get; set; }
        public virtual Mrt ParentNavigation { get; set; } = null!;
    }
}
