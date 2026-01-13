using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GuidDecimal
    {
        public Guid Guid { get; set; }
        public Decimal Value { get; set; }

        public GuidDecimal() { }
        public GuidDecimal(Guid guid , decimal? value = null) {
            Guid = guid;
            if(value != null) Value = (decimal)value;
        }
        
    }
}
