using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// unpayments plus definitive insolvencies
    /// </summary>
    public partial class VwImpagatsOinsolvent
    {
        public Guid Customer { get; set; }
        public int? Cod { get; set; }
    }
}
