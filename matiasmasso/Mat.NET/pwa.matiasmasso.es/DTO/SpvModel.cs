using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SpvModel : BaseGuid
    {
        public int Id { get; set; }
        public DateTime FchAvis { get; set; }
        public Guid? Customer { get; set; }
        public Guid? Product { get; set; }

        public SpvModel() : base() { }
        public SpvModel(Guid guid) : base(guid) { }


        public string PageUrl() => string.Format("/Spv/{0}", Guid.ToString());
        public override string ToString() => string.Format("Reparació {0} del {1:dd/MM/yy}", Guid.ToString(), FchAvis);

    }

}
