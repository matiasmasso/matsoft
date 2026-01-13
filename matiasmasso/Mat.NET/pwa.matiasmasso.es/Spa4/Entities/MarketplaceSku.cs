using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class MarketplaceSku
    {
        public Guid Marketplace { get; set; }
        public Guid Sku { get; set; }
        public string? Id { get; set; }
        public bool Active { get; set; }
    }
}
