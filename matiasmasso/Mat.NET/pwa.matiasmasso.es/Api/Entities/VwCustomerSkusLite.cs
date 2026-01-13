using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer product range, light weight
    /// </summary>
    public partial class VwCustomerSkusLite
    {
        public Guid SkuGuid { get; set; }
        public Guid Customer { get; set; }
        public Guid? Ccx { get; set; }
        public int ExclusionCod { get; set; }
    }
}
