using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductRank
    {
        public Periods Period { get; set; }
        public List<Item> Items { get; set; }
        public List<DTOGuidNom> Zonas { get; set; }

        public List<DTOGuidNom> Brands { get; set; }

        public Units Unit { get; set; }

        public DTOLang Lang { get; set; }

        public enum Periods
        {
            Year,
            Quarter,
            Month
        }

        public enum Units
        {
            Units,
            Eur
        }

        public DTOProductRank(Periods oPeriod) : base()
        {
            Period = oPeriod;
            Items = new List<Item>();
        }

        public DTOYearMonth YearMonthFrom()
        {
            var retval = DTOYearMonth.Current();
            switch (Period)
            {
                case DTOProductRank.Periods.Year:
                    {
                        retval = DTOYearMonth.Previous(12);
                        break;
                    }

                case DTOProductRank.Periods.Quarter:
                    {
                        retval = DTOYearMonth.Previous(3);
                        break;
                    }

                case DTOProductRank.Periods.Month:
                    {
                        retval = DTOYearMonth.Previous();
                        break;
                    }
            }
            return retval;
        }


        public decimal Share(Item item)
        {
            decimal retval = 0;
            var tot = Items.Sum(x => x.Value);
            if (tot > 0)
                retval = item.Value / tot;
            return retval;
        }

        public class Item
        {
            public DTOProductCategory Product { get; set; }
            public decimal Value { get; set; }
        }
    }
}
