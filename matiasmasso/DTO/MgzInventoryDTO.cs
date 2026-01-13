using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MgzInventoryModel
    {
        public Guid Sku { get; set; }
        public Guid Category { get; set; }
        public Guid Brand { get; set; }
        public int Stk { get; set; }
        public decimal Eur { get; set; }
        public DateTime LastIn { get; set; }
    }

    public class MgzInventoryExtracteModel
    {
        public Guid Sku { get; set; }
        public List<Item> Items { get; set; } = new();
        public class Item
        {
            public Guid AlbGuid { get; set; }
            public int Alb { get; set; }
            public DateTime Fch { get; set; }
            public string Nom { get; set; }
            public int Cod { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }
            public decimal Dto { get; set; }
            public int Stock { get; set; }
            public decimal Pmc { get; set; }

            public decimal Inventory() => Stock * Pmc;
            public decimal SignedQty() => Cod < 50 ? Qty : -Qty;
            public decimal NetPrice() => Math.Round(Eur * (100 - Dto) / 100, 2);
            public decimal Amount() => Qty*NetPrice();

        }
    }

    public class MgzInventoryDTO // TO DEPRECATE
    {
        public List<MonthlyIO> Movements { get; set; } = new();
        public List<Cost> Costs { get; set; } = new();
        public LangDTO? Lang { get; set; }
        public CacheDTO? Cache { get; set; }

        public List<Item> Expand(Item? parent = null)
        {
            var retval = new List<Item>();
            if (parent == null)
                retval = Years();
            else if (parent.Product.Count == 1)
                retval = Categories(parent.Product, parent.Period);
            else if (parent.Product.Count == 2)
                retval = Skus(parent.Product, parent.Period);
            else if (parent.Product.Count == 3)
                retval = Logs(parent.Product, parent.Period, parent.Qty);
            else if (parent.Period.Count == 1)
                retval = Months(parent.Period.First());
            //else if (parent.Period.Count == 2)
            //    retval = Days(parent.Period[0], parent.Period[1]);
            else if (parent.Period.Count == 3)
                retval = Brands(parent.Period);
            return retval;
        }

        public List<Item> Years(Guid? brand = null, Guid? category = null, Guid? sku = null)
        {
            var retval = new List<Item>();
            Lang ??= LangDTO.Default();

            List<int> years = Movements.GroupBy(x => x.Year).Select(x => x.Key).OrderByDescending(y => y).ToList();
            foreach (var year in years)
            {
                var stocks = Stocks(year, 12, brand, category, sku);
                retval.Add(new Item
                {
                    Period = new int[] { year }.ToList(),
                    Caption = year.ToString(),
                    Qty = stocks.Sum(x => x.Qty),
                    Eur = stocks.Sum(x => x.Eur)
                });
            }
            return retval;
        }

        public List<Item> Months(int year, Guid? brand = null, Guid? category = null, Guid? sku = null)
        {
            var retval = new List<Item>();
            Lang ??= LangDTO.Default();
            var lastMonth = year == DateTime.Today.Year ? DateTime.Today.Month : 12;
            List<int> months = Enumerable.Range(1, lastMonth).OrderByDescending(x => x).ToList();
            foreach (var month in months)
            {
                var stocks = Stocks(year, month, brand, category, sku);
                retval.Add(new Item
                {
                    Period = new int[] { year, month }.ToList(),
                    Caption = String.Format("{0} {1}", year, Lang.MonthAbr(month)),
                    Qty = stocks.Sum(x => x.Qty),
                    Eur = stocks.Sum(x => x.Eur)
                }); ;
            }
            return retval;
        }

        //public List<Item> Days(int year, int month, Guid? brand = null, Guid? category = null, Guid? sku = null)
        //{
        //    var retval = new List<Item>();
        //    Lang ??= LangDTO.Default();
        //    var lastDay = year == DateTime.Today.Year && month == DateTime.Today.Month ? DateTime.Today.Day : DateTime.DaysInMonth(year, month);
        //    List<int> days = Enumerable.Range(1, lastDay).OrderByDescending(x => x).ToList();
        //    foreach (var day in days)
        //    {
        //        var fch = new DateTime(year, month, day);
        //        var stocks = Stocks(fch, brand, category, sku);
        //        retval.Add(new Item
        //        {
        //            Period = new int[] { year, month, fch.Day }.ToList(),
        //            Caption = string.Format("{0:00} {1}", fch.Day, Lang.Weekday(year, month, fch.Day)),
        //            Qty = stocks.Sum(x => x.Qty),
        //            Eur = stocks.Sum(x => x.Eur)
        //        });
        //    }
        //    return retval;
        //}


        private DateTime Fch(List<int> period)
        {
            DateTime retval = DateTime.Today;
            if (period.Count == 1) retval = new DateTime(period[0], 12, 31);
            if (period.Count == 2) retval = new DateTime(period[0], period[1], DateTime.DaysInMonth(period[0], period[1]));
            if (period.Count == 3) retval = new DateTime(period[0], period[1], period[2]);
            return retval;
        }
        public List<Item> Brands(List<int> period)
        {
            var fch = Fch(period);
            var stocks = Stocks(fch.Year, fch.Month);
            var retval = stocks.GroupBy(x => x.Brand).Select(y => new Item
            {
                Period = period,
                Product = new Guid[] { y.Key }.ToList(),
                Caption = Cache?.Brand(y.Key)?.Nom?.Tradueix(Lang) ?? "?",
                Qty = y.Sum(z => z.Qty),
                Eur = y.Sum(z => z.Eur)
            }).ToList();
            return retval;
        }

        public List<Item> Categories(List<Guid> product, List<int> period)
        {
            var fch = Fch(period);
            var brand = product[0];
            var stocks = Stocks(fch.Year, fch.Month, brand);
            var retval = stocks.GroupBy(x => x.Category)
                .Where(y => y.Sum(z => z.Qty) != 0 || y.Sum(z => z.Eur) != 0)
                .Select(y => new Item
                {
                    Period = period,
                    Product = new Guid[] { brand, y.Key }.ToList(),
                    Caption = Cache?.Category(y.Key)?.Nom?.Tradueix(Lang) ?? "?",
                    Qty = y.Sum(z => z.Qty),
                    Eur = y.Sum(z => z.Eur)
                }).OrderBy(a => a.Caption).ToList();
            return retval;
        }

        public List<Item> Skus(List<Guid> product, List<int> period)
        {
            var fch = Fch(period);
            var brand = product[0];
            var category = product[1];
            var stocks = Stocks(fch.Year, fch.Month, brand, category);
            var retval = stocks.GroupBy(x => x.Sku).Select(y => new Item
            {
                Period = period,
                Product = new Guid[] { brand, category, y.Key }.ToList(),
                Caption = Cache?.Sku(y.Key)?.NomLlarg?.Tradueix(Lang) ?? "?",
                Qty = y.Sum(z => z.Qty),
                Eur = y.Sum(z => z.Eur)
            }).ToList();
            return retval;
        }

        public List<Item> Logs(List<Guid> product, List<int> period, int stk)
        {
            var fch = Fch(period);
            var brand = product[0];
            var category = product[1];
            var sku = product[2];
            var costs = Costs
                .Where(x => x.Sku == sku && x.Fch.Date <= fch)
                .OrderByDescending(y => y.Fch.Date)
                .ThenByDescending(z => z.AlbId)
                .ToList();

            var retval = new List<Item>();
            foreach (var cost in costs)
            {
                int qty = 0;
                if (cost.Qty < stk)
                {
                    qty = cost.Qty;
                    stk -= cost.Qty;
                }
                else
                {
                    qty = stk;
                    stk = 0;
                }
                retval.Add(new Item
                {
                    Period = period,
                    Product = new Guid[] { brand, category, sku, cost.AlbGuid }.ToList(),
                    Caption = String.Format("{0} {1:n0} {2} {3:dd/MM/yy}", Lang!.Tradueix("Albarán", "Albarà", "Delivery note"), cost.AlbId, Lang.Tradueix("del", "del", "from"), cost.Fch.Date),
                    Qty = qty,
                    Eur = cost.Import()
                });

                if (stk <= 0) break;
            }
            return retval;
        }

        public List<Log> Logs(Stock stock, DateTime fch)
        {
            var retval = new List<Log>();
            var stk = stock.Qty;
            var costs = Costs.Where(x => x.Sku == stock.Sku && x.Fch.Date <= fch.Date).OrderByDescending(y => y.Fch).ToList();
            foreach (var cost in costs)
            {
                int qty = 0;
                if (cost.Qty < stk)
                {
                    qty = cost.Qty;
                    stk -= cost.Qty;
                }
                else
                {
                    qty = stk;
                    stk = 0;
                }

                retval.Add(new Log
                {
                    AlbGuid = cost.AlbGuid,
                    AlbId = cost.AlbId,
                    Fch = cost.Fch.Date,
                    Qty = qty,
                    Eur = cost.Eur,
                    Dto = cost.Dto
                });

                if (stk <= 0) break;
            }
            return retval;
        }


        public List<Stock> Stocks(int year, int month, Guid? brand = null, Guid? category = null, Guid? sku = null)
        {
            var retval = new List<Stock>();
            if (brand == null)
                retval = Movements.Where(x => IsWithin(x.Year, x.Month, year, month))
                .GroupBy(y => new { y.Brand, y.Category, y.Sku })
                .Select(z => new Stock
                {
                    Brand = z.Key.Brand,
                    Category = z.Key.Category,
                    Sku = z.Key.Sku,
                    Qty = z.Sum(x => x.IO),
                    Eur = GetCost(z.Key.Sku, z.Sum(x => x.IO), year, month)
                }).ToList();

            else if (category is null)
                retval = Movements.Where(x => IsWithin(x.Year, x.Month, year, month) && x.Brand == brand).
                GroupBy(y => new { y.Category, y.Sku }).
                Select(z => new Stock
                {
                    Brand = (Guid)brand!,
                    Category = z.Key.Category,
                    Sku = z.Key.Sku,
                    Qty = z.Sum(x => x.IO),
                    Eur = GetCost(z.Key.Sku, z.Sum(x => x.IO), year, month)
                }).ToList();

            else
                retval = Movements.Where(x => IsWithin(x.Year, x.Month, year, month) && x.Brand == brand && x.Category == category).
                GroupBy(y => new { y.Sku }).
                Select(z => new Stock
                {
                    Brand = (Guid)brand!,
                    Category = (Guid)category!,
                    Sku = z.Key.Sku,
                    Qty = z.Sum(x => x.IO),
                    Eur = GetCost(z.Key.Sku, z.Sum(x => x.IO), year, month)
                }).ToList();

            return retval;
        }

        private decimal GetCost(Guid sku, int stk, int year, int month)
        {
            decimal retval = 0;
            var costs = Costs.Where(x => x.Sku == sku && IsWithin(x.Fch, year, month)).OrderByDescending(x => x.Fch).ToList();
            foreach (var cost in costs)
            {
                var qty = 0;
                if (stk > cost.Qty)
                {
                    qty = cost.Qty;
                    stk -= qty;
                }
                else
                {
                    qty = stk;
                    stk = 0;
                }

                retval += Math.Round(qty * cost.Eur * (100 - cost.Dto) / 100, 2);

                if (stk <= 0) break;
            }
            return retval;
        }

        private bool IsWithin(DateTime fch, int year, int month) => IsWithin(fch.Year, fch.Month, year, month);
        private bool IsWithin(int candidateYear, int candidateMonth, int year, int month) => (candidateYear < year) || (candidateYear == year && candidateMonth <= month);


        public Excel.Book ExcelBook(Item item)
        {
            var fch = new DateTime(item.Period[0], item.Period[1], item.Period[2]);
            var retval = new Excel.Book(string.Format("Inventari {0:yyyy.MM.dd}.xlsx", fch));
            var sheet = retval.AddSheet();
            sheet.AddColumn(Lang!.Tradueix("Ref.M+O"));
            sheet.AddColumn(Lang.Tradueix("Marca", "Marca", "Brand"));
            sheet.AddColumn(Lang.Tradueix("Categoria", "Categoria", "Category"));
            sheet.AddColumn(Lang.Tradueix("Producto", "Producte", "Product"));
            sheet.AddColumn(Lang.Tradueix("Fecha", "Data", "Date"));
            sheet.AddColumn(Lang.Tradueix("Unidades", "Unitats", "Units"), DTO.Excel.Cell.NumberFormats.Integer);
            sheet.AddColumn(Lang.Tradueix("Coste", "Cost", "Cost"), DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn(Lang.Tradueix("Descuento", "Descompte", "Discount"), DTO.Excel.Cell.NumberFormats.Percent);
            sheet.AddColumn(Lang.Tradueix("Importe", "Import", "Amount"), DTO.Excel.Cell.NumberFormats.Euro);

            var stocks = Stocks(fch.Year, fch.Month);
            foreach (var sku in stocks)
            {
                var brandNom = Cache?.Brand(sku.Brand)?.Nom?.Tradueix(Lang) ?? "?";
                var categoryNom = Cache?.Category(sku.Category)?.Nom?.Tradueix(Lang) ?? "?";
                var skuNom = Cache?.Sku(sku.Sku)?.NomLlarg?.Tradueix(Lang) ?? "?";
                var skuId = Cache?.Sku(sku.Sku)?.Id ;

                foreach (var log in Logs(sku, fch))
                {
                    var row = sheet.AddRow();
                    row.AddCell(skuId?.ToString() ?? "");
                    row.AddCell(brandNom);
                    row.AddCell(categoryNom);
                    row.AddCell(skuNom);
                    row.AddCell(log.Fch);
                    row.AddInt(log.Qty);
                    row.AddCell(log.Eur);
                    row.AddCell(log.Dto);
                    //row.AddFormula("RC[-3]*RC[-2]*(100-RC{-1])/100");
                }
            }
            return retval;
        }



        public class Cost
        {
            public Guid Sku { get; set; }
            public Guid AlbGuid { get; set; }
            public int AlbId { get; set; }
            public DateTime Fch { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }
            public decimal Dto { get; set; }
            public decimal Import() => Math.Round(Qty * Eur * (100 - Dto) / 100, 2);

        }

        public class Log
        {
            public Guid AlbGuid { get; set; }
            public int AlbId { get; set; }
            public DateTime Fch { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }
            public decimal Dto { get; set; }
            public decimal Import() => Math.Round(Qty * Eur * (100 - Dto) / 100, 2);

        }
        public class MonthlyIO
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public Guid Brand { get; set; }
            public Guid Category { get; set; }
            public Guid Sku { get; set; }
            public int IO { get; set; }
        }

        public class Stock
        {
            public Guid Brand { get; set; }
            public Guid Category { get; set; }
            public Guid Sku { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }


        }

        public class Item
        {
            public Guid Guid { get; set; }
            public string? Caption { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }
            public List<Guid> Product { get; set; } = new();

            public List<int> Period { get; set; } = new();
            public bool IsLoading { get; set; }
            public bool IsLoadingExcel { get; set; }



            public override string ToString() => string.Format("{0} {1:n2}", Caption, Eur);

        }
    }
}
