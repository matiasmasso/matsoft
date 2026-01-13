using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Currency exchange rates
    /// </summary>
    public partial class VwCurExchange
    {
        public string Tag { get; set; } = null!;
        public DateTime Fch { get; set; }
        public decimal Rate { get; set; }
    }
}
