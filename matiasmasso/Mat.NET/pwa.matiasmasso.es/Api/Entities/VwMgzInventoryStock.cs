using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwMgzInventoryStock
    {
        public Guid? MgzGuid { get; set; }
        public Guid Brand { get; set; }
        public Guid? Category { get; set; }
        public Guid ArtGuid { get; set; }
        public DateTime Fch { get; set; }
        public int? Expr1 { get; set; }
    }
}
