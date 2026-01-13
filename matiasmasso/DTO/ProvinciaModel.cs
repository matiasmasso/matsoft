using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProvinciaModel:BaseGuid,IModel
    {
        public Guid? Region { get; set; }
        public string? Nom { get; set; }
        public string? Mod347 { get; set; }
        public string? Intrastat { get; set; }


        public ProvinciaModel() : base() { }
        public ProvinciaModel(Guid guid) : base(guid) { }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

        public string Caption() => Nom ?? "?";
        public override string ToString() => Caption();
    }
}
