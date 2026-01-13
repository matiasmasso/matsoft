using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSalesQuery
    {
        public DTOUser User { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string SearchTerm { get; set; }
        public List<Filter> Filters { get; set; }
        public List<Item> Items { get; set; }

        public enum Modes
        {
            Diari,
            Ranking
        }

        public enum Levels
        {
            Years,
            Months,
            Days,
            Orders,
            Search
        }

        public DTOSalesQuery() : base()
        {
            Filters = new List<Filter>();
            Items = new List<Item>();
        }

        public Levels level()
        {
            var retval = Levels.Years;
            if (Day > 0)
                retval = Levels.Orders;
            else if (Month > 0)
                retval = Levels.Days;
            else if (Year > 0)
                retval = Levels.Months;
            else if (SearchTerm.isNotEmpty())
                retval = Levels.Search;
            return retval;
        }

        public Filter AddFilter(Filter.Cods cod, Guid guid, string caption)
        {
            var retval = Filter.Factory(cod, guid, caption);
            Filters.Add(retval);
            return retval;
        }

        public bool IsFilteredBy(Filter.Cods cod)
        {
            return Filters.Any(x => x.Cod == cod);
        }

        public bool IsFilteredByAny(Filter.Cods[] cods)
        {
            return Filters.Any(x => cods.Any(y => x.Cod == y));
        }

        public List<Filter> GetFilters(Filter.Cods cod)
        {
            return Filters.Where(x => x.Cod == cod).ToList();
        }


        public class Item
        {
            public string Caption { get; set; }
            public decimal Value { get; set; }
            public string Tag { get; set; }

            public static Item Factory(string caption, decimal value, string tag)
            {
                Item retval = new Item();
                {
                    var withBlock = retval;
                    withBlock.Caption = caption;
                    withBlock.Value = value;
                    withBlock.Tag = tag;
                }
                return retval;
            }
        }

        public class Filter
        {
            public Cods Cod { get; set; }
            public Guid Guid { get; set; }
            public string Caption { get; set; }

            public enum Cods
            {
                None,
                Customer,
                Ccx,
                Holding,
                Proveidor,
                Rep,
                Brand,
                Category,
                Sku,
                Channel,
                Country,
                Zona,
                Location
            }

            public static Filter Factory(Filter.Cods cod, Guid guid, string caption)
            {
                Filter retval = new Filter();
                {
                    var withBlock = retval;
                    withBlock.Cod = cod;
                    withBlock.Guid = guid;
                    withBlock.Caption = caption;
                }
                return retval;
            }
        }
    }
}
