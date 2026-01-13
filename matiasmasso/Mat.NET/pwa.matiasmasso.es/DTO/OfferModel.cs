using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OfferModel
    {
        public Guid Parent { get; set; }
        public Guid Sku { get; set; }
        public decimal? Price { get; set; }
    }
}
