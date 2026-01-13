using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product pending units from purchase orders
    /// </summary>
    public partial class VwSkuPnc
    {
        public Guid SkuGuid { get; set; }
        public int? Clients { get; set; }
        public int? ClientsAlPot { get; set; }
        public int? ClientsEnProgramacio { get; set; }
        public int? ClientsBlockStock { get; set; }
        public int? Pn1 { get; set; }
    }
}
