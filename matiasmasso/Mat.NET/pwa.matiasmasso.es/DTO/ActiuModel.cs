using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ActiuModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public Guid? Cta { get; set; }

        public ActiuModel() { }
        public ActiuModel(Guid guid) : base(guid) { }

        public override string ToString()
        {
            return Nom ?? base.ToString()!;
        }

        public class Mov : BaseGuid
        {
            public EmpModel.EmpIds Emp { get; set; }
            public Guid Actiu { get; set; }
            public DateOnly Fch { get; set; }
            public Guid? Contraparte { get; set; }
            public decimal? Qty { get; set; }
            public decimal Eur { get; set; } // total compra o venda

            public Mov() { }
            public Mov(Guid guid) : base(guid) { }
        }
    }
}
