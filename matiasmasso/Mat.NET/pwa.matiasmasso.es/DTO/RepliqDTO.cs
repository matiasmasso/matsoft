using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepliqDTO
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public DateOnly Fch { get; set; }
        public decimal? BaseFras { get; set; }
        public decimal? ComisioEur { get; set; }
        public decimal? IVAPct { get; set; }
        public decimal? IrpfPct { get; set; }
        public Guid? CcaGuid { get; set; }

        public decimal Cash()
        {
            decimal retval = 0;
            if(ComisioEur != null)
            {
                retval = (decimal)ComisioEur;
            if (IVAPct != null)
                retval += (decimal)ComisioEur * (decimal)IVAPct / 100;
            if (IrpfPct != null)
                retval -= (decimal)ComisioEur * (decimal)IrpfPct / 100;
            }
            return retval;
        }
    }
}
