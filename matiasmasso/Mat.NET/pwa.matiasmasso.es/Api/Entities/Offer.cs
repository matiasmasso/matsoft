using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Offer
    {
        public Guid Parent { get; set; }
        public Guid Sku { get; set; }
        public decimal Price { get; set; }
        public DateTime FchCreated { get; set; }
    }
}
