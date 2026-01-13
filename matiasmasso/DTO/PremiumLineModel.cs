using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PremiumLineModel:BaseGuid
    {
        public string? Nom { get; set; }
        public DateTime? Fch { get; set; }
        public int? Codi { get; set; }

        public List<Guid> Products { get; set; }

        public PremiumLineModel() : base() { }
        public PremiumLineModel(Guid guid) : base(guid) { }
    }
}
