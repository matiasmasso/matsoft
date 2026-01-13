using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Products, either if product brand, product category or product sku
    /// </summary>
    public partial class VwProductGuid
    {
        public Guid Guid { get; set; }
        public Guid Brand { get; set; }
        public Guid? Dept { get; set; }
        public Guid? Category { get; set; }
        public Guid? Sku { get; set; }
        public int Cod { get; set; }
        public bool Obsoleto { get; set; }
    }
}
