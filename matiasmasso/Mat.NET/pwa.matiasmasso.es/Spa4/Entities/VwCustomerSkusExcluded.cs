using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customer excluded product range
    /// </summary>
    public partial class VwCustomerSkusExcluded
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
        public int Cod { get; set; }
    }
}
