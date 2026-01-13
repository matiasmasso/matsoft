using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class EdiInvrptItem
    {
        public Guid Guid { get; set; }
        public Guid Parent { get; set; }
        public int Lin { get; set; }
        public Guid Sku { get; set; }
        public int Qty { get; set; }

        public virtual EdiInvrptHeader ParentNavigation { get; set; } = null!;
        public virtual Art SkuNavigation { get; set; } = null!;
    }
}
