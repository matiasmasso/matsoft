using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class EciplantillaModLog
    {
        public Guid Dept { get; set; }
        public DateTime Fch { get; set; }
        public Guid Sku { get; set; }
        public int? TipoGestion { get; set; }
        public int? Stock { get; set; }
        public bool? Obsoleto { get; set; }
    }
}
