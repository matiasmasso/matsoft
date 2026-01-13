using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customers included on exclusive distribution product ranges
    /// </summary>
    public partial class VwPremiumLineExclusiveCustomer
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
    }
}
