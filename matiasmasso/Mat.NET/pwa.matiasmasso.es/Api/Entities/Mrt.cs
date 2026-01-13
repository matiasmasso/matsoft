using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Amortisable items
    /// </summary>
    public partial class Mrt
    {
        public Mrt()
        {
            Mr2s = new HashSet<Mr2>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company, foreign key for Emp table
        /// </summary>
        public short Emp { get; set; }
        /// <summary>
        /// Acquisition date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Accounting account
        /// </summary>
        public Guid Cta { get; set; }
        /// <summary>
        /// Acquisition price, in foreign currency
        /// </summary>
        public decimal Pts { get; set; }
        /// <summary>
        /// Acquisition price in Euro
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Item description
        /// </summary>
        public string Dsc { get; set; } = null!;
        /// <summary>
        /// Accounting entry of acquisition
        /// </summary>
        public Guid? AltaCca { get; set; }
        /// <summary>
        /// Accounting entry of removal from inventory
        /// </summary>
        public Guid? BaixaCca { get; set; }
        /// <summary>
        /// Amortisation rate
        /// </summary>
        public decimal Tipus { get; set; }

        public virtual Cca? AltaCcaNavigation { get; set; }
        public virtual Cca? BaixaCcaNavigation { get; set; }
        public virtual PgcCtum CtaNavigation { get; set; } = null!;
        public virtual ICollection<Mr2> Mr2s { get; set; }
    }
}
