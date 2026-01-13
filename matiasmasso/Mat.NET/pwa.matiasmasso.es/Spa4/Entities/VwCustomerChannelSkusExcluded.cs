using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Excluded product ranges per customer
    /// </summary>
    public partial class VwCustomerChannelSkusExcluded
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
    }
}
