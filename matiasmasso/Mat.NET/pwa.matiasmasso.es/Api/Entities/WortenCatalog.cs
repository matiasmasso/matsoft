using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class WortenCatalog
    {
        public Guid? Marketplace { get; set; }
        public Guid? Sku { get; set; }
        public string? Id { get; set; }
    }
}
