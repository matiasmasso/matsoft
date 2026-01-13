using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORepeticio : DTOContact
    {
        public int Qty { get; set; }
        public int Orders { get; set; }
        public decimal Eur { get; set; }

        public DTORepeticio() : base()
        {
        }

        public DTORepeticio(Guid oGuid) : base(oGuid)
        {
        }

        public static List<DTORepeticio> All(List<DTORepeticio> items, int iOrderCount)
        {
            List<DTORepeticio> retval = (List<DTORepeticio>)items.Where(x => x.Orders == iOrderCount);
            return retval;
        }
    }
}
