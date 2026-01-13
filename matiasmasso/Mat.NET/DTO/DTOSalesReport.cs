using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSalesReport
    {
        public List<DTOProductBrand> Catalog { get; set; }
        public List<DTOCountry> Atlas { get; set; }
        public List<Centro> Centros { get; set; }
        public List<Item> Items { get; set; }
        public bool IsLoaded { get; set; }

        public DTOHolding SelectedHolding { get; set; }
        public DTOExercici SelectedExercici { get; set; }
        public Concepts SelectedConcept { get; set; }
        public Formats SelectedFormat { get; set; }
        public Centro SelectedCentro { get; set; }
        public string SelectedDept { get; set; } = "";
        public DTOProduct SelectedProduct { get; set; }
        public DTOProveidor SelectedProveidor { get; set; }


        public enum Concepts
        {
            Centros,
            Brands,
            Categories,
            Skus
        }

        public enum Formats
        {
            Qty,
            Eur
        }


        public DTOSalesReport() : base()
        {
            Catalog = new List<DTOProductBrand>();
            Atlas = new List<DTOCountry>();
            Centros = new List<Centro>();
            Items = new List<Item>();
        }

        public static DTOSalesReport Factory(DTOExercici oExercici, DTOHolding.Wellknowns oHoldingId)
        {
            DTOSalesReport retval = new DTOSalesReport();
            {
                var withBlock = retval;
                withBlock.SelectedHolding = DTOHolding.Wellknown(oHoldingId);
                withBlock.SelectedExercici = oExercici;
                withBlock.SelectedConcept = Concepts.Centros;
                withBlock.SelectedFormat = Formats.Eur;
            }
            return retval;
        }

        public DTOSalesReport Clon()
        {
            DTOSalesReport retval = new DTOSalesReport();
            {
                var withBlock = retval;
                withBlock.Catalog = Catalog;
                withBlock.Atlas = Atlas;
                withBlock.Centros = Centros;
                withBlock.Items = Items;
                withBlock.SelectedExercici = SelectedExercici;
                withBlock.SelectedHolding = SelectedHolding;
                withBlock.SelectedCentro = SelectedCentro;
                withBlock.SelectedFormat = SelectedFormat;
                withBlock.SelectedConcept = SelectedConcept;
                withBlock.SelectedDept = SelectedDept;
                withBlock.SelectedProduct = SelectedProduct;
                withBlock.SelectedProveidor = SelectedProveidor;
                withBlock.IsLoaded = IsLoaded;
            }
            return retval;
        }

        public Centro AddCentro(string nom)
        {
            var retval = DTOSalesReport.Centro.Factory(nom);
            Centros.Add(retval);
            return retval;
        }

        public static Row Total(List<Row> oRows)
        {
            Row retval = new Row();
            {
                var withBlock = retval;
                withBlock.Txt = "Total";
                for (var mes = 1; mes <= 12; mes++)
                {
                    var idx = mes - 1;
                    withBlock.Months[idx] = oRows.Sum(x => x.Months[idx]);
                }
            }
            return retval;
        }

        public List<string> Depts()
        {
            var retval = Items.Where(x => x.Dept.isNotEmpty()).GroupBy(x => x.Dept).Select(y => y.First()).Select(z => z.Dept).ToList();
            return retval;
        }


        public List<Row> Rows()
        {
            var retval = new List<Row>();
            var items = FilteredItems(SelectedProduct, SelectedCentro, SelectedDept);
            switch (SelectedConcept)
            {
                case Concepts.Centros:
                    {
                        var GroupedItems = items.GroupBy(x => x.Centro.Guid).ToList();
                        foreach (var g in GroupedItems)
                        {
                            var oCentro = Centros.First(x => x.Guid.Equals(g.Key));
                            Row oRow = new Row();
                            {
                                var withBlock = oRow;
                                withBlock.Txt = oCentro.Nom;
                                withBlock.Tag = oCentro;
                                foreach (var item in g)
                                {
                                    switch (SelectedFormat)
                                    {
                                        case Formats.Qty:
                                            {
                                                oRow.Months[(int)item.YearMonth.Month - 1] += item.Qty;
                                                break;
                                            }

                                        case Formats.Eur:
                                            {
                                                oRow.Months[(int)item.YearMonth.Month - 1] += item.YearMonth.Eur;
                                                break;
                                            }
                                    }
                                }
                            }
                            retval.Add(oRow);
                        }

                        break;
                    }

                case Concepts.Brands:
                    {
                        var oCatalogSkus = Catalog.SelectMany(x => x.Categories.SelectMany(y => y.Skus)).ToList();
                        var oFilteredSkus = oCatalogSkus.Where(x => items.Any(y => y.Sku.Guid.Equals(x.Guid))).ToList();
                        var GroupedItems = oFilteredSkus.GroupBy(x => x.Category.Brand).Select(y => y.First()).Select(z => z.Category.Brand).ToList();
                        foreach (var oBrand in GroupedItems)
                        {
                            Row oRow = new Row();
                            {
                                var withBlock = oRow;
                                withBlock.Txt = oBrand.Nom.Esp;
                                withBlock.Tag = oBrand;
                                foreach (var oCategory in oBrand.Categories)
                                {
                                    foreach (var oSku in oCategory.Skus)
                                    {
                                        foreach (var item in FilteredItems().Where(x => x.Sku.Guid.Equals(oSku.Guid)))
                                        {
                                            switch (SelectedFormat)
                                            {
                                                case Formats.Qty:
                                                    {
                                                        oRow.Months[(int)item.YearMonth.Month - 1] += item.Qty;
                                                        break;
                                                    }

                                                case Formats.Eur:
                                                    {
                                                        oRow.Months[(int)item.YearMonth.Month - 1] += item.YearMonth.Eur;
                                                        break;
                                                    }
                                            }
                                        }
                                    }
                                }
                            }
                            retval.Add(oRow);
                        }

                        break;
                    }

                case Concepts.Categories:
                    {
                        var oCatalogSkus = Catalog.SelectMany(x => x.Categories.SelectMany(y => y.Skus)).ToList();
                        var oFilteredSkus = oCatalogSkus.Where(x => items.Any(y => y.Sku.Guid.Equals(x.Guid))).ToList();
                        var GroupedItems = oFilteredSkus.GroupBy(x => x.Category).Select(y => y.First()).Select(z => z.Category).ToList();
                        foreach (var oCategory in GroupedItems)
                        {
                            Row oRow = new Row();
                            {
                                var withBlock = oRow;
                                withBlock.Txt = oCategory.Brand.Nom.Esp + " " + oCategory.Nom.Esp;
                                withBlock.Tag = oCategory;
                                foreach (var oSku in oCategory.Skus)
                                {
                                    foreach (var item in items.Where(x => x.Sku.Guid.Equals(oSku.Guid)))
                                    {
                                        switch (SelectedFormat)
                                        {
                                            case Formats.Qty:
                                                {
                                                    oRow.Months[(int)item.YearMonth.Month - 1] += item.Qty;
                                                    break;
                                                }

                                            case Formats.Eur:
                                                {
                                                    oRow.Months[(int)item.YearMonth.Month - 1] += item.YearMonth.Eur;
                                                    break;
                                                }
                                        }
                                    }
                                }
                            }
                            retval.Add(oRow);
                        }

                        break;
                    }
            }
            return retval;
        }

        public MatHelper.Excel.Sheet Excel()
        {
            List<Exception> exs = new List<Exception>();
            var oFormat = SelectedFormat == DTOSalesReport.Formats.Eur ? MatHelper.Excel.Cell.NumberFormats.Euro : MatHelper.Excel.Cell.NumberFormats.Integer;
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Sales report " + SelectedExercici.Year);
            {
                var withBlock = retval;
                withBlock.AddColumn("Concept");
                withBlock.AddColumn("Total", oFormat);
                for (var i = 1; i <= 12; i++)
                    withBlock.AddColumn(DTOLang.ENG().MesAbr(i), oFormat);
                withBlock.DisplayTotals = true;
            }
            foreach (DTOSalesReport.Row item in this.Rows())
            {
                var oRow = retval.AddRow();
                {
                    var withBlock = oRow;
                    withBlock.AddCell(item.Txt);
                    withBlock.AddFormula("SUM(RC[1]:RC[12])");
                    for (var i = 1; i <= 12; i++)
                        withBlock.AddCell(item.Months[i - 1]);
                }
            }
            return retval;
        }

        public class Centro : DTOCompactGuid
        {
            public string Nom { get; set; }
            public DTOCompactGuid Location { get; set; }

            public static DTOSalesReport.Centro Factory(string nom)
            {
                var retval = new DTOSalesReport.Centro();
                {
                    var withBlock = retval;
                    withBlock.Nom = nom;
                }
                return retval;
            }
        }

        public class Item
        {
            public DTOCompactGuid Sku { get; set; }
            public DTOCompactGuid Centro { get; set; }
            public DTOYearMonth YearMonth { get; set; }
            public int Qty { get; set; }
            public string Dept { get; set; }

            public static Item Factory(DateTime Fch, string Dept, int Qty, decimal Eur)
            {
                Item retval = new Item();
                {
                    var withBlock = retval;
                    withBlock.YearMonth = DTOYearMonth.FromFch(Fch);
                    withBlock.YearMonth.Eur = Eur;
                    withBlock.Dept = Dept;
                    withBlock.Qty = Qty;
                }
                return retval;
            }
        }



        public class Row
        {
            public string Txt { get; set; }
            public List<decimal> Months { get; set; }
            public object Tag { get; set; }

            public Row()
            {
                Months = new List<decimal>();
                for (var i = 1; i <= 12; i++)
                    Months.Add(0);
            }
        }

        public class GraphPoint
        {
            public DateTime Fch { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }
        }


        public List<Item> FilteredItems(DTOProduct oProduct = null/* TODO Change to default(_) if this is not a reference type */, Centro oCentro = null, string sDept = "")
        {
            List<Item> retval = new List<Item>();
            foreach (var item in Items)
                retval.Add(item);

            if (oProduct != null)
            {
                switch (oProduct.SourceCod)
                {
                    case DTOProduct.SourceCods.Sku:
                        {
                            retval = Items.Where(x => x.Sku.Guid.Equals(oProduct.Guid)).ToList();
                            break;
                        }

                    case DTOProduct.SourceCods.Category:
                        {
                            var oCategory = Catalog.SelectMany(x => x.Categories).First(y => y.Equals(oProduct));
                            retval = Items.Where(x => oCategory.Skus.Any(y => y.Guid.Equals(x.Sku.Guid))).ToList();
                            break;
                        }

                    case DTOProduct.SourceCods.Brand:
                        {
                            var oCategories = Catalog.First(x => x.Equals(oProduct)).Categories;
                            var oSkus = oCategories.SelectMany(x => x.Skus).ToList();
                            retval = Items.Where(x => oSkus.Any(y => y.Guid.Equals(x.Sku.Guid))).ToList();
                            break;
                        }
                }
            }

            if (oCentro != null)
                retval = Items.Where(x => x.Centro.Guid.Equals(oCentro.Guid)).ToList();

            if (!string.IsNullOrEmpty(sDept))
                retval = retval.Where(x => x.Dept == sDept).ToList();

            return retval;
        }
    }
}
