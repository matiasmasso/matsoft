using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwBancsSdo
    {
        public int Emp { get; set; }
        public Guid Banc { get; set; }
        public string Abr { get; set; } = null!;
        public string IbanCcc { get; set; } = null!;
        public DateTime Fch { get; set; }
        public decimal Sdo { get; set; }
    }
}
