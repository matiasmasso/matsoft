using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class MarketplaceSku
    {
        public Guid Marketplace { get; set; }
        public Guid Sku { get; set; }
        public string? Id { get; set; }
        public bool Active { get; set; }
    }
}
