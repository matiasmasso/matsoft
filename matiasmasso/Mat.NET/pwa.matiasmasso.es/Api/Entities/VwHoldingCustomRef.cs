using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Cuistom references assigned by certain customers to our products
    /// </summary>
    public partial class VwHoldingCustomRef
    {
        public Guid? Holding { get; set; }
        public Guid SkuGuid { get; set; }
        public string CustomRef { get; set; } = null!;
    }
}
