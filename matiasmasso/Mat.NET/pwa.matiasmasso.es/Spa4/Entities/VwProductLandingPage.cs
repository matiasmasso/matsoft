using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Landing page per product, with all variants and languages a consumer is expected  to call a product with
    /// </summary>
    public partial class VwProductLandingPage
    {
        public Guid Guid { get; set; }
        public string? Url { get; set; }
    }
}
