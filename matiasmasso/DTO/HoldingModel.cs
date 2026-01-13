using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoldingModel:BaseGuid
    {
        public string? Nom { get; set; }

        public enum Wellknowns
        {
            None,
            ElCorteIngles
        }

        public static HoldingModel? Wellknown(Wellknowns id)
        {
            HoldingModel? retval = null;
            switch (id)
            {
                case Wellknowns.ElCorteIngles:
                    retval = new HoldingModel{Guid =new Guid("8B07C540-1DA6-48E2-B137-563BFDD4218B") };
                    break;
            }
            return retval;
        }
    }
}
