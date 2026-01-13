using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Bad debts
    /// </summary>
    public partial class Insolvencia
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Customer, foreign key for CliGral table
        /// </summary>
        public Guid Customer { get; set; }
        /// <summary>
        /// Date 
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Nominal { get; set; }
        public DateTime? FchRehabilitacio { get; set; }

        public virtual CliGral CustomerNavigation { get; set; } = null!;
    }
}
