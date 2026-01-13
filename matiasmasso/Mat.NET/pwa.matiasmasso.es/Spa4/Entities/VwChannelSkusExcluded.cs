using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product ranges excluded from distribution per channel
    /// </summary>
    public partial class VwChannelSkusExcluded
    {
        public Guid Channel { get; set; }
        public Guid Sku { get; set; }
    }
}
