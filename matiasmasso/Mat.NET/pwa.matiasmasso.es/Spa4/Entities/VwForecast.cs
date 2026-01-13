using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product units expected to be sold each month
    /// </summary>
    public partial class VwForecast
    {
        public Guid? Customer { get; set; }
        public Guid Sku { get; set; }
        public int Yea { get; set; }
        public int Mes { get; set; }
        public int Qty { get; set; }
        public DateTime FchCreated { get; set; }
        public Guid? UsrCreated { get; set; }
    }
}
