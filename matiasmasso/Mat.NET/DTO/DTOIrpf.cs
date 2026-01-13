using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOIrpf
    {
        public DTOEmp Emp { get; set; }
        public DTOYearMonth YearMonth { get; set; }
        public List<Item> Items { get; set; }
        public List<DTOPgcSaldo> Saldos { get; set; }

        public DTOIrpf() : base()
        {
            Items = new List<Item>();
            Saldos = new List<DTOPgcSaldo>();
        }

        public static DTOIrpf Factory(DTOEmp oEmp, int year, int month)
        {
            DTOIrpf retval = new DTOIrpf();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.YearMonth = new DTOYearMonth(year, (DTOYearMonth.Months)month);
            }
            return retval;
        }

        public DateTime Fch()
        {
            return YearMonth.LastFch();
        }

        public class Item
        {
            public DTOCcb Ccb { get; set; }
            public DTOAmt Base { get; set; }
        }
    }
}
