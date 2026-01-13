using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class JornadaLaboral
    {
        public Guid Guid { get; set; }
        public Guid Staff { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
    }
}
