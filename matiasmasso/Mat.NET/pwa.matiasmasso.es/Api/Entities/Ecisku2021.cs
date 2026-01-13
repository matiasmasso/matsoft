using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Ecisku2021
    {
        public string Eci { get; set; } = null!;
        public int? SkuId { get; set; }
        public string Depto { get; set; } = null!;
        public string? Ean { get; set; }
        public Guid? Sku { get; set; }
        public Guid? Dept { get; set; }
    }
}
