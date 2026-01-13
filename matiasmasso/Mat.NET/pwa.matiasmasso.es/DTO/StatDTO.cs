using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StatDTO
    {
        public List<Item> Items { get; set; } = new();
        public CacheDTO? Cache { get; set; }
        public LangDTO? Lang { get; set; }
        public enum FilterCods:int
        {
            Customer,
            Marketplace
        }


        public List<DisplayItem> Expand(DisplayItem? parent = null)
        {
            var retval = new List<DisplayItem>();
            if (parent == null)
                retval = Years();
            else if (parent.Period.Count == 1)
                retval = Months(parent);
            else if (parent.Product.Count == 0)
                retval = Brands(parent);
            else if (parent.Product.Count == 1)
                retval = Categories(parent);
            else if (parent.Product.Count == 2)
                retval = Skus(parent);
            return retval;
        }

        public List<DisplayItem> Expand(StatDTO.MenuItem menuitem)
        {
           return Expand(menuitem.Target);
        }



        public List<DisplayItem> Years(DisplayItem? parent = null)
        {
            return Items.GroupBy(x => new { x.Year }).Select(y => new DisplayItem
            {
                Period = new int[] { y.Key.Year }.ToList(),

                Value = y.Key.Year.ToString(),
                Caption = y.Key.Year.ToString(),
                Qty = y.Sum(z => z.Qty),
                Eur = y.Sum(z => z.Eur)
            })
                .OrderByDescending(z => z.Value).ToList();
        }

        public List<DisplayItem> Months(DisplayItem parent)
        {
            return Items.GroupBy(x => new { x.Year, x.Month }).Select(y => new DisplayItem
            {
                Cod = DisplayItem.Cods.Month,
                Period = new int[] { y.Key.Year, y.Key.Month }.ToList(),

                Value = String.Format("{0:00}", y.Key.Month),
                Caption = LangDTO.Esp().MonthAbr(y.Key.Month),
                Qty = y.Sum(z => z.Qty),
                Eur = y.Sum(z => z.Eur)
            })
                .OrderByDescending(z => z.Value).ToList();
        }

        public List<DisplayItem> Brands(DisplayItem parent)
        {
            List<DisplayItem> retval = new();
            if (parent.Period.Count > 0)
            {
                var year = parent.Period[0];
                var query = Items.Where(x => x.Year == year).AsQueryable();
                if (parent.Period.Count > 1)
                {
                    query = query.Where(x => x.Month == parent.Period[1]);
                }

                return query.GroupBy(x => new { x.Brand }).Select(y => new DisplayItem
                {
                    Product = new Guid[] { y.Key.Brand }.ToList(),
                    Period = parent.Period,
                    Value = y.Key.Brand.ToString(),
                    Caption = Cache.Brand(y.Key.Brand).Nom.Tradueix(Lang) ?? "?",
                    Qty = y.Sum(z => z.Qty),
                    Eur = y.Sum(z => z.Eur)
                })
                    .OrderByDescending(z => z.Value).ToList();
            }
            return retval;
        }


        public List<DisplayItem> Categories(DisplayItem parent)
        {
            List<DisplayItem> retval = new();
            if (parent.Period.Count > 0)
            {
                var year = parent.Period[0];
                var brand = parent.Product[0];
                var query = Items.Where(x => x.Year == year && x.Brand == brand).AsQueryable();
                if (parent.Period.Count > 1)
                {
                    query = query.Where(x => x.Month == parent.Period[1]);
                }


                return query.GroupBy(x => new { x.Category }).Select(y => new DisplayItem
                {
                    Product = new Guid[]{brand,y.Key.Category }.ToList(),
                    Period = parent.Period,
                    Value = y.Key.Category.ToString(),
                    Caption = Cache.Category(y.Key.Category).Nom.Tradueix(Lang) ?? "?",
                    Qty = y.Sum(z => z.Qty),
                    Eur = y.Sum(z => z.Eur)
                })
                    .OrderByDescending(z => z.Value).ToList();
            }
            return retval;
        }

        public List<DisplayItem> Skus(DisplayItem parent)
        {
            List<DisplayItem> retval = new();
            if (parent.Period.Count > 0)
            {
                var year = parent.Period[0];
                var brand = parent.Product[0];
                var category = parent.Product[1];
                var query = Items.Where(x => x.Year == year && x.Category == category).AsQueryable();
                if (parent.Period.Count > 1)
                {
                    query = query.Where(x => x.Month == parent.Period[1]);
                }


                return query.GroupBy(x => new { x.Sku }).Select(y => new DisplayItem
                {
                    Product = new Guid[] { brand, category, y.Key.Sku }.ToList(),
                    Period = parent.Period,
                    Value = y.Key.Sku.ToString(),
                    Caption = Cache.Sku(y.Key.Sku).NomLlarg.Tradueix(Lang) ?? "?",
                    Qty = y.Sum(z => z.Qty),
                    Eur = y.Sum(z => z.Eur)
                })
                    .OrderByDescending(z => z.Value).ToList();
            }
            return retval;
        }



        public List<MenuItem> MenuItems(DisplayItem displayItem)
        {
            var retval = new List<MenuItem>();
            retval.Add(new MenuItem {
                Caption = Lang!.Tradueix("Marcas comerciales","Marques comercials","Commercial Brands"),
                Action = "Brands",
                Target = displayItem
            });
            if (displayItem.Period.Count == 1)
                retval.Add(new MenuItem
                {
                    Caption = Lang.Tradueix("Meses", "Mesos", "Months"),
                    Action = "Months",
                    Target = displayItem
                });
            return retval;
        }
        public class StatRequest
        {
            public int Emp { get; set; }
            public int Year { get; set; }
            public List<StatFilter> Filters { get; set; } = new();

            public void AddFilter(FilterCods cod, Guid guid)
            {
                var filter = new StatFilter();
                filter.Cod = (int)cod;
                filter.Guid = guid;
                Filters.Add(filter);
            }
        }

        public class StatFilter
        {
            public int Cod { get; set; }
            public Guid Guid { get; set; }
            public string? Nom { get; set; }
        }
        public class Item
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public Guid Brand { get; set; }
            public Guid Country { get; set; }
            public Guid Zona { get; set; }
            public Guid Location { get; set; }
            public Guid Customer { get; set; }
            public Guid Category { get; set; }
            public Guid Sku { get; set; }
            public Guid? Rep { get; set; }
            public Guid? Channel { get; set; }
            public string? Cnap { get; set; }

            public int Qty { get; set; }
            public decimal Eur { get; set; }

        }

            public class DisplayItem
            {
            public List<Guid> Product { get; set; } = new();
            public List<int> Period { get; set; } = new();
                public Cods Cod { get; set; }
                public string? Value { get; set; }
                public string Caption { get; set; } = string.Empty;
                public int Qty { get; set; }
                public decimal Eur { get; set; }

            public bool IsLoading { get; set; }
            public bool DisplayContextMenu()
            {
                return Period.Count < 2 & Product.Count==0 && !IsLoading;
            }
                public enum Cods
                {
                    Year,
                    Month,
                    Day,
                    Brand,
                    Category,
                    Sku
                }
        }

        public class MenuItem
        {
            public DisplayItem? Target { get; set; }
            public string? Caption { get; set; }
            public string? Action { get; set; }
        }
    }
}
