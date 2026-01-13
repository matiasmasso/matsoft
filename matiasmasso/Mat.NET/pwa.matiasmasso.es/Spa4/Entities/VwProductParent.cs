using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// In product  hierarchy (brand/category/sku), relates children with parents
    /// </summary>
    public partial class VwProductParent
    {
        public Guid Parent { get; set; }
        public Guid Child { get; set; }
        public int ParentCod { get; set; }
        public int ChildCod { get; set; }
    }
}
