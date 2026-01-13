using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product price lists, both active and outdated
    /// </summary>
    public partial class VwRetailPrice
    {
        public Guid Art { get; set; }
        public DateTime Fch { get; set; }
        public DateTime? FchEnd { get; set; }
        public decimal? Retail { get; set; }
        public Guid? Customer { get; set; }
    }
}
