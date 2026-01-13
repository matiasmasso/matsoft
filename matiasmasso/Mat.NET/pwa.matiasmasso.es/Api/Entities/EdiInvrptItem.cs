using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class EdiInvrptItem
    {
        public Guid Parent { get; set; }
        public int Lin { get; set; }
        public string? Ean { get; set; }
        public string? RefSupplier { get; set; }
        public string? RefCustomer { get; set; }
        public int Qty { get; set; }

        public virtual EdiInvrptHeader ParentNavigation { get; set; } = null!;
    }
}
