using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BoolWrapper
    {
        public bool Value { get; set; }
        public BoolWrapper(bool value) { this.Value = value; }
    }
}
