using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GuidInt
    {
        public Guid Guid { get; set; }
        public int? Value { get; set; }

        public GuidInt() { }
        public GuidInt(Guid guid, int? value = null)
        {
            Guid = guid;
            if (value != null) Value = (int)value;
        }
    }
}
