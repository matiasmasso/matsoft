using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product range per customer
    /// </summary>
    public partial class VwCustomerChannelOpenSku
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
    }
}
