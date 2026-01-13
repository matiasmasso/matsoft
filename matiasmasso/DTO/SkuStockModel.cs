using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SkuStockModel
    {
        public Guid Sku { get; set; }
        public Guid Mgz { get; set; }
        public int Stock { get; set; }
        public int Available(SkuPncModel? pnc)
        {
            var clients = pnc?.Clients ?? 0;
            var retval = Math.Max(0, Stock - clients);
            return retval;
        }
    }
}
