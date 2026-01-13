using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RegionModel:BaseGuid,IModel
    {
        public Guid Country { get; set; }
        public string? Nom { get; set; }
        public RegionModel() : base() { }
        public RegionModel(Guid guid) : base(guid) { }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption() => Nom ?? "?";
        public override string ToString() => Caption();

    }
}
