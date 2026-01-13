using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwCca1
    {
        public Guid Guid { get; set; }
        public int Emp { get; set; }
        public short Yea { get; set; }
        public int Cca { get; set; }
        public DateTime? Fch { get; set; }
        public string? Txt { get; set; }
        public string? Hash { get; set; }
        public decimal? Eur { get; set; }
        public string? UsrNom { get; set; }
    }
}
