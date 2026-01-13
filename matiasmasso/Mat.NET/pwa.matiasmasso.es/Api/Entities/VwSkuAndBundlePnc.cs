using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product units pending from purchase orders, including  product bundles
    /// </summary>
    public partial class VwSkuAndBundlePnc
    {
        public Guid SkuGuid { get; set; }
        public int? Clients { get; set; }
        public int? ClientsAlPot { get; set; }
        public int? ClientsEnProgramacio { get; set; }
        public int? ClientsBlockStock { get; set; }
        public int? Pn1 { get; set; }
        public int Xcod { get; set; }
    }
}
