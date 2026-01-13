using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SkuWithModel
    {
        public Guid Parent { get; set; }
        public List<GuidInt> Children { get; set; } = new();
    }
}
