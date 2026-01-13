using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Agent product range
    /// </summary>
    public partial class VwRepProduct
    {
        /// <summary>
        /// Product sku. Foreign key to Art table
        /// </summary>
        public Guid SkuGuid { get; set; }
        /// <summary>
        /// Commercial agent. Foreign key to CliGral table
        /// </summary>
        public Guid Rep { get; set; }
    }
}
