using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Stock value per product reported by e-commerce customers
    /// </summary>
    public partial class VwWtbolInventory
    {
        public Guid Site { get; set; }
        public decimal? Inventory { get; set; }
    }
}
