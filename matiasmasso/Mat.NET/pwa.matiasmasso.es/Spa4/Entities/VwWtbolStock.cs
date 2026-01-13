using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Stock units available per product reported by customer e-commerces
    /// </summary>
    public partial class VwWtbolStock
    {
        public Guid Site { get; set; }
        public Guid Sku { get; set; }
        public int Stock { get; set; }
        public decimal? Price { get; set; }
        public DateTime FchCreated { get; set; }
    }
}
