using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product range available per distribution channel
    /// </summary>
    public partial class VwChannelOpenSku
    {
        public Guid Channel { get; set; }
        public string BrandNom { get; set; } = null!;
        public string CategoryNom { get; set; } = null!;
        public string SkuNom { get; set; } = null!;
        public Guid BrandGuid { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
    }
}
