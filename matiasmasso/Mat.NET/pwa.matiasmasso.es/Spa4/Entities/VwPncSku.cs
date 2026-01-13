using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Pending purchase orders
    /// </summary>
    public partial class VwPncSku
    {
        public Guid PncGuid { get; set; }
        public Guid PdcGuid { get; set; }
        public int Lin { get; set; }
        public Guid SkuGuid { get; set; }
        public string SkuNomLlarg { get; set; } = null!;
        public Guid CategoryGuid { get; set; }
        public Guid BrandGuid { get; set; }
        public Guid? RepGuid { get; set; }
        public decimal? Com { get; set; }
        public int Qty { get; set; }
        public decimal Eur { get; set; }
        public decimal Dto { get; set; }
    }
}
