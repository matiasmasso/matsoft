using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customers excluded from exclusive distribution product ranges
    /// </summary>
    public partial class VwPremiumLineExcludedCustomer
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
    }
}
