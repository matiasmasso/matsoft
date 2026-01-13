using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOKpi
    {
        public List<DTOYearMonth> YearMonths { get; set; }
        public string Caption { get; set; }
        public decimal YFactor { get; set; }
        public Ids Id { get; set; }

        public enum Formats
        {
            Eur,
            Decimal
        }

        public enum Ids
        {
            Activo_Corriente,
            Pasivo_Corriente,
            Comandes_Proveidors,
            Comandes_Clients,
            Efectes_Vençuts,
            Efectes_Impagats,
            Index_Impagats
        }



        public static DTOKpi Factory(Ids id)
        {
            DTOKpi retval = new DTOKpi();
            retval.Id = id;
            retval.YearMonths = new List<DTOYearMonth>();
            return retval;
        }

        public Formats format()
        {
            Formats retval = Formats.Eur;
            switch (Id)
            {
                case Ids.Index_Impagats:
                    {
                        retval = Formats.Decimal;
                        break;
                    }
            }
            return retval;
        }

        public decimal MaxValue()
        {
            var retval = 0;
            if (YearMonths.Count > 0)
                retval = (int)YearMonths.Max(x => x.Eur);
            return retval;
        }

        public DTOYearMonth YearMonthFrom()
        {
            return DTOYearMonth.Min(YearMonths);
        }

        public DTOYearMonth YearMonthTo()
        {
            return DTOYearMonth.Max(YearMonths);
        }

        public int MonthsCount()
        {
            return DTOYearMonth.MonthsDiff(YearMonthFrom(), YearMonthTo());
        }

        public DTOYearMonth AddYearMonth(int year, DTOYearMonth.Months month, decimal eur)
        {
            DTOYearMonth retval = new DTOYearMonth(year, month, eur);
            YearMonths.Add(retval);
            return retval;
        }
    }
}
