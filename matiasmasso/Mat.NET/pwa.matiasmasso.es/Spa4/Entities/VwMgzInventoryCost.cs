using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwMgzInventoryCost
    {
        public Guid? MgzGuid { get; set; }
        public Guid ArtGuid { get; set; }
        public Guid AlbGuid { get; set; }
        public int Alb { get; set; }
        public DateTime Fch { get; set; }
        public int Qty { get; set; }
        public decimal? Eur { get; set; }
        public float? Dto { get; set; }
    }
}
