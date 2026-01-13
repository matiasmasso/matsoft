using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerProductModel
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
        public string? Ref { get; set; }
    }
}
