using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwUsrArtCustRef
    {
        public Guid Sku { get; set; }
        public string Ref { get; set; } = null!;
        public Guid Email { get; set; }
    }
}
