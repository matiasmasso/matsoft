using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Currency Exchange rates
    /// </summary>
    public partial class CurExchangeRate
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// ISO 4217 Currency code
        /// </summary>
        public string Iso { get; set; } = null!;
        /// <summary>
        /// Exchange rate of this currency on this date
        /// </summary>
        public decimal Rate { get; set; }
    }
}
