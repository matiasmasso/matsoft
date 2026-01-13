using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOOffer
    {
        public Guid Parent { get; set; }
        public Guid Sku { get; set; }
        public decimal Price { get; set; }
    }
}
