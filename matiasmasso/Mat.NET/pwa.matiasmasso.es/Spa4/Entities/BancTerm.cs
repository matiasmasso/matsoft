using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Bank conditions for remittance advances
    /// </summary>
    public partial class BancTerm
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Our bank; foreign key for CliGral table
        /// </summary>
        public Guid Banc { get; set; }
        /// <summary>
        /// Enumerable DTOBancTerm.targets (1.advanced remittance, 2.remittance at sight)
        /// </summary>
        public int Target { get; set; }
        /// <summary>
        /// Date the conditions were agreed
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// True if interest rate is indexed to Euribor
        /// </summary>
        public bool Euribor { get; set; }
        /// <summary>
        /// Interest rate on top of Euribor
        /// </summary>
        public decimal? Diferencial { get; set; }
        /// <summary>
        /// Minimum fee per remittance
        /// </summary>
        public decimal? Minim { get; set; }

        public virtual CliGral BancNavigation { get; set; } = null!;
    }
}
