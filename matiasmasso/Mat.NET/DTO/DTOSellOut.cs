using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSellOut
    {
        public DTOLang Lang { get; set; }
        public DTOUser User { get; set; }
        public List<DTOYearMonth> YearMonths { get; set; }
        public ConceptTypes ConceptType { get; set; }

        public List<Filter> Filters { get; set; }
        public bool IncludeHidden { get; set; }
        public bool GroupByHolding { get; set; }

        public string Title { get; set; }
        public object SelectedKey { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public List<DTOSelloutItem> Items { get; set; } = new List<DTOSelloutItem>();
        public Formats Format { get; set; }
        public int ExpandToLevel { get; set; }
        public bool IsBundle { get; set; }

        public enum ConceptTypes
        {
            product,
            geo,
            repsGeo,
            repsProduct,
            yeas,
            channels,
            cnaps,
            centres,
            full
        }

        public enum Formats
        {
            amounts,
            units
        }


        public DTOSelloutItem Tot()
        {
            DTOSelloutItem retval = new DTOSelloutItem(this, "totals");
            for (int i = 0; i <= 11; i++)
                retval.Values.Add(0);
            foreach (DTOSelloutItem oItem in Items)
            {
                if (oItem != null)
                {
                    if (oItem.Level == 0)
                    {
                        for (int i = 0; i <= oItem.Values.Count - 1; i++)
                            retval.Values[i] += oItem.Values[i];
                    }
                }
            }
            return retval;
        }

        public DTOSellOut() : base()
        {
        }

        public DTOSellOut(DTOSellOut.ConceptTypes oConceptType = DTOSellOut.ConceptTypes.product, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */) : base()
        {
            Lang = oLang;
            ConceptType = oConceptType;
            ExpandToLevel = 0;
        }

        public static DTOSellOut Factory(DTOUser oUser, int year, DTOSellOut.ConceptTypes conceptType, DTOSellOut.ConceptTypes format, string brand, string category, string channel, string country, string zona, string location, string contact, bool groupbyholding)
        {
            // de controller via jquery
            List<Exception> exs = new List<Exception>();
            DTOYearMonth oYearMonthTo = new DTOYearMonth(year, DTOYearMonth.Months.December);
            DTOSellOut retval = Factory(oUser, oYearMonthTo, conceptType, (Formats)format);

            {
                var withBlock = retval;
                if (GuidHelper.IsGuid(category))
                {
                    var value = new DTOGuidNom(new Guid(category));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Product);
                    oFilter.Values.Add(value);
                }
                else if (GuidHelper.IsGuid(brand))
                {
                    var value = new DTOGuidNom(new Guid(brand));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Product);
                    oFilter.Values.Add(value);
                }

                if (GuidHelper.IsGuid(contact))
                {
                    var value = new DTOCustomer(new Guid(contact));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Customer);
                    oFilter.Values.Add(DTOGuidNom.Factory(value.Guid, value.Nom));
                    withBlock.GroupByHolding = groupbyholding;
                }
                else if (GuidHelper.IsGuid(location))
                {
                    var value = new DTOLocation(new Guid(location));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas);
                    oFilter.Values.Add(DTOGuidNom.Factory(value.Guid, value.Nom));
                }
                else if (GuidHelper.IsGuid(zona))
                {
                    var value = new DTOZona(new Guid(zona));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas);
                    oFilter.Values.Add(DTOGuidNom.Factory(value.Guid, value.Nom));
                }
                else if (GuidHelper.IsGuid(country))
                {
                    var value = new DTOCountry(new Guid(country));
                    var oFilter = retval.GetFilter(DTOSellOut.Filter.Cods.Atlas);
                    oFilter.Values.Add(DTOGuidNom.Factory(value.Guid, value.Nom));
                }

                if (withBlock.User.Rol.id == DTORol.Ids.salesManager)
                    withBlock.ConceptType = DTOSellOut.ConceptTypes.repsGeo;
            }

            return retval;
        }


        public static DTOSellOut Factory(DTOUser oUser, DTOYearMonth oYearMonthTo = null/* TODO Change to default(_) if this is not a reference type */, DTOSellOut.ConceptTypes oConceptType = DTOSellOut.ConceptTypes.geo, DTOSellOut.Formats oFormat = DTOSellOut.Formats.amounts
    )
        {
            List<Exception> exs = new List<Exception>();
            if (oYearMonthTo == null)
                oYearMonthTo = new DTOYearMonth(DTO.GlobalVariables.Today().Year, DTOYearMonth.Months.December);
            DTOSellOut retval = new DTOSellOut();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Lang = oUser.Lang;
                withBlock.YearMonths = oYearMonthTo.Last12Yearmonths();
                withBlock.ConceptType = oConceptType;
                withBlock.Format = oFormat;
                withBlock.Filters = DTOSellOut.AllFilters(withBlock.User.Lang);
            }
            return retval;
        }

        public static int Column(DateTime DtFchTo, DTOCustomerProduct item)
        {
            int iMonthsCount = 13;
            int iFirstMonthColumn = 6;
            DTOYearMonth oSecondYearMonth = new DTOYearMonth(DtFchTo.Year, (DTOYearMonth.Months)DtFchTo.Month);
            int iDiff = DTOYearMonth.MonthsDiff(item.YearMonth, oSecondYearMonth);
            int retval = iFirstMonthColumn + iMonthsCount - iDiff - 1;
            return retval;
        }

        public static string ConceptTypeNom(ConceptTypes oConceptType, DTOLang oLang)
        {
            string retval = "";
            switch (oConceptType)
            {
                case ConceptTypes.channels:
                    {
                        retval = oLang.Tradueix("Canales", "Canals", "Channels");
                        break;
                    }

                case ConceptTypes.cnaps:
                    {
                        retval = "Cnap";
                        break;
                    }

                case ConceptTypes.geo:
                    {
                        retval = "Atlas";
                        break;
                    }

                case ConceptTypes.product:
                    {
                        retval = oLang.Tradueix("Productos", "Productes", "Products");
                        break;
                    }

                case ConceptTypes.repsGeo:
                    {
                        retval = "Reps/Atlas";
                        break;
                    }

                case ConceptTypes.repsProduct:
                    {
                        retval = oLang.Tradueix("Reps/Productos", "Reps/Productes", "Reps/Products");
                        break;
                    }

                case ConceptTypes.yeas:
                    {
                        retval = oLang.Tradueix("Años", "Anys", "Years");
                        break;
                    }

                default:
                    {
                        retval = oConceptType.ToString();
                        break;
                    }
            }
            return retval;
        }

        public static List<DTOSellOut.Filter> AllFilters(DTOLang oLang)
        {
            List<DTOSellOut.Filter> retval = new List<DTOSellOut.Filter>();
            foreach (Filter.Cods cod in Enum.GetValues(typeof(Filter.Cods)))
            {
                DTOSellOut.Filter oFilter = new DTOSellOut.Filter(cod);
                retval.Add(oFilter);
            }
            return retval;
        }

        public string AllFiltersText()
        {
            List<Exception> exs = new List<Exception>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (YearMonths != null && YearMonths.Count > 0)
                sb.AppendFormat("{0}:{1} ", Lang.Tradueix("Año", "Any", "Year"), YearMonths.Last().Year);

            var oFilter = GetFilter(DTOSellOut.Filter.Cods.Provider);
            if (oFilter.Values.Count > 0)
            {
                sb.Append(Lang.Tradueix("Proveedor", "Proveïdor", "Supplier"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    // FEBL.Contact.Load(value, exs))
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.Customer);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Cliente", "Client", "Customer"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    // FEBL.Contact.Load(value, exs)
                    sb.Append(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.Product);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Producto", "Producte", "Product"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    // FEBL.Product.Load(value, exs)
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.Channel);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Canal", "Canal", "Channel"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.Atlas);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Areas", "Areas", "Areas"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.Rep);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Reps", "Reps", "Reps"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            oFilter = GetFilter(DTOSellOut.Filter.Cods.CNap);
            if (oFilter.Values.Count > 0)
            {
                sb.AppendFormat(Lang.Tradueix("Cnap", "Cnap", "Cnap"));
                foreach (var value in oFilter.Values)
                {
                    sb.Append(value.Equals(oFilter.Values.First()) ? ": " : ", ");
                    sb.AppendFormat(value.Nom.Replace("'", ""));
                }
            }

            if (GroupByHolding)
                sb.AppendFormat("{0}:{1} ", Lang.Tradueix("Consolidado", "Consolidat", "Consolidated"), Lang.Tradueix("Si", "Si", "True"));

            string retval = sb.ToString();
            return retval;
        }


        public void AddFilter(Filter.Cods oCod, IEnumerable<DTOGuidNom> oValues)
        {
            var oFilter = Filters.FirstOrDefault(x => x.Cod == oCod);
            if (oFilter != null)
                oFilter.Values.AddRange(oValues);
        }


        public void ClearFilter(Filter.Cods oCod)
        {
            if (Filters != null)
            {
                var oFilter = Filters.FirstOrDefault(x => x.Cod == DTOSellOut.Filter.Cods.Rep);
                if (oFilter != null)
                    oFilter.Values = new List<DTOGuidNom>();
            }
        }

        public DTOSellOut.Filter GetFilter(Filter.Cods oCod)
        {
            DTOSellOut.Filter retval = null;
            if (Filters != null)
                retval = Filters.FirstOrDefault(x => x.Cod == oCod);
            return retval;
        }

        public static List<DTOGuidNom> FilterValues(DTOSellOut oSellout, Filter.Cods oCod)
        {
            List<DTOGuidNom> retval = new List<DTOGuidNom>();
            var oFilter = oSellout.GetFilter(oCod);
            if (oFilter != null)
                retval = oFilter.Values;
            return retval;
        }

        public static void SetLevelToExpand(ref DTOSellOut oSellout, int iLevel)
        {
            oSellout.ExpandToLevel = iLevel;
            foreach (DTOSelloutItem item in oSellout.Items)
                item.IsExpanded = (item.HasChildren & item.Level < iLevel);
        }



        public static List<DTOSelloutItem> FlattenItems(DTOSellOut oSellOut)
        {
            List<DTOSelloutItem> retval = FlattenItems(oSellOut.Items);
            return retval;
        }

        public static List<DTOSelloutItem> FlattenItems(List<DTOSelloutItem> items)
        {
            List<DTOSelloutItem> retval = new List<DTOSelloutItem>();
            foreach (var item in items)
            {
                retval.Add(item);
                if (item.Items.Count > 0)
                {
                    item.HasChildren = true;
                    retval.AddRange(FlattenItems(item.Items));
                }
            }
            return retval;
        }

        public DTOSellOut Clone()
        {
            DTOSellOut retval = new DTOSellOut(ConceptType, Lang);
            {
                var withBlock = retval;
                withBlock.User = User;
                withBlock.YearMonths = YearMonths;
                withBlock.Filters = new List<DTOSellOut.Filter>();
                foreach (var oFilter in Filters)
                    withBlock.Filters.Add(oFilter.Clone());
                withBlock.IncludeHidden = IncludeHidden;
                withBlock.GroupByHolding = GroupByHolding;
                withBlock.Title = Title;
                withBlock.SelectedKey = SelectedKey;
                withBlock.ColumnHeaders = ColumnHeaders;
                withBlock.Items = new List<DTOSelloutItem>();
                withBlock.Items.AddRange(Items);
                withBlock.Format = Format;
                withBlock.ExpandToLevel = ExpandToLevel;
            }
            return retval;
        }

        public static DTOCsv RawDataLast12MonthsCsv(List<DTOCustomerProduct> items)
        {
            DateTime DtFchTo = DTO.GlobalVariables.Today();

            DTOCsv retval = new DTOCsv("M+O Sellout.csv");
            DTOCsvRow oRow = retval.AddRow();
            oRow.AddCell("Country");
            oRow.AddCell("Area");
            oRow.AddCell("Location");
            oRow.AddCell("Customer");
            oRow.AddCell("Ref");
            oRow.AddCell("Product");
            for (int i = 12; i >= 0; i += -1)
            {
                DateTime DtFch = DtFchTo.AddMonths(-i);
                oRow.AddCell(DTOLang.ENG().MesAbr(DtFch.Month));
            }

            DTOProductSku oSku = new DTOProductSku();
            foreach (DTOCustomerProduct item in items)
            {
                if (oSku.UnEquals(item.Sku))
                {
                    oSku = item.Sku;
                    oRow = retval.AddRow();
                    oRow.AddCell(item.Customer.Address.Zip.Location.Zona.Country.LangNom.Eng);
                    oRow.AddCell(item.Customer.Address.Zip.Location.Zona.Nom);
                    oRow.AddCell(item.Customer.Address.Zip.Location.Nom);
                    oRow.AddCell(item.Customer.NomComercialOrDefault());
                    oRow.AddCell(oSku.RefProveidor);
                    oRow.AddCell(oSku.NomProveidor);
                    for (var i = 0; i <= 12; i++)
                        oRow.AddCell("0");
                }
                oRow.Cells[DTOSellOut.Column(DtFchTo, item)] = item.Qty.ToString();
            }
            return retval;
        }



        public class Filter
        {
            public Cods Cod { get; set; }
            public List<DTOGuidNom> Values { get; set; }

            public enum Cods
            {
                Product,
                Atlas,
                Customer,
                Holding,
                Provider,
                Channel,
                Rep,
                CNap
            }

            public Filter(Cods oCod) : base()
            {
                Cod = oCod;
                Values = new List<DTOGuidNom>();
            }

            public Filter Clone()
            {
                Filter retval = new Filter(Cod);
                retval.Values = new List<DTOGuidNom>();
                foreach (var oValue in Values)
                    retval.Values.Add(oValue);
                return retval;
            }

            public string Caption(DTOLang oLang)
            {
                string retval = "";
                switch (Cod)
                {
                    case DTOSellOut.Filter.Cods.Product:
                        {
                            retval = "productes";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Atlas:
                        {
                            retval = "areas";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Channel:
                        {
                            retval = "canals";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Customer:
                        {
                            retval = "clients";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Holding:
                        {
                            retval = "holdings";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Provider:
                        {
                            retval = "proveidors";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Rep:
                        {
                            retval = "reps";
                            break;
                        }

                    case DTOSellOut.Filter.Cods.CNap:
                        {
                            retval = "cnaps";
                            break;
                        }
                }
                return retval;
            }

            public string ValueCaption(DTOBaseGuid oValue, DTOLang oLang)
            {
                string retval = "";
                switch (Cod)
                {
                    case DTOSellOut.Filter.Cods.Product:
                        {
                            retval = ((DTOProduct)oValue).FullNom();
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Atlas:
                        {
                            retval = DTOArea.FullNom((DTOArea)oValue, oLang);
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Channel:
                        {
                            retval = ((DTODistributionChannel)oValue).LangText.Tradueix(oLang);
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Customer:
                        {
                            DTOCustomer oCustomer = DTOCustomer.FromContact((DTOContact)oValue);
                            retval = oCustomer.Nom;
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Provider:
                        {
                            DTOProveidor oProveidor = DTOProveidor.FromContact((DTOContact)oValue);
                            retval = oProveidor.Nom;
                            break;
                        }

                    case DTOSellOut.Filter.Cods.Rep:
                        {
                            DTORep oRep = DTORep.FromContact((DTOContact)oValue);
                            retval = oRep.NicknameOrNom();
                            break;
                        }

                    case DTOSellOut.Filter.Cods.CNap:
                        {
                            DTOCnap oCnap = (DTOCnap)oValue;
                            retval = oCnap.FullNom(oLang);
                            break;
                        }
                }
                return retval;
            }
        }

        public static MatHelper.Excel.Sheet Excel(List<Exception> exs, DTOSellOut oSellOut)
        {
            string sFilename = string.Format("M+O Sellout {0:yyyy.MM.dd}", DTO.GlobalVariables.Today());
            string sSheetname = oSellOut.AllFiltersText();
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sSheetname, sFilename);
            var oDomain = oSellOut.Lang.Domain();

            List<DTOSelloutItem> items = DTOSellOut.FlattenItems(oSellOut);
            if (items.Count > 0)
            {
                int maxLevel = items.Max(x => x.Level);
                MatHelper.Excel.Cell.NumberFormats numberFormat = oSellOut.Format == DTOSellOut.Formats.amounts ? MatHelper.Excel.Cell.NumberFormats.Euro : MatHelper.Excel.Cell.NumberFormats.Integer;

                var lastItem = items.Where(x => x.Level == maxLevel).First();

                bool includeSkuRef = false;
                switch (oSellOut.ConceptType)
                {
                    case DTOSellOut.ConceptTypes.product:
                    case DTOSellOut.ConceptTypes.repsProduct:
                        {
                            includeSkuRef = true;
                            break;
                        }
                }


                {
                    var withBlock = retval;
                    for (int j = 0; j <= maxLevel - 1; j++)
                        withBlock.AddColumn("", MatHelper.Excel.Cell.NumberFormats.W50);
                    if (includeSkuRef)
                    {
                        withBlock.AddColumn("Ref", MatHelper.Excel.Cell.NumberFormats.W50);
                        withBlock.AddColumn(oSellOut.Lang.Tradueix("Producto", "Producte", "Product"), MatHelper.Excel.Cell.NumberFormats.W50);
                    }
                    else
                        withBlock.AddColumn("", MatHelper.Excel.Cell.NumberFormats.W50);

                    withBlock.AddColumn("Total", numberFormat);
                    for (int m = 1; m <= 12; m++)
                        withBlock.AddColumn(oSellOut.Lang.MesAbr(m), numberFormat);
                }

                string[] levels = new string[maxLevel + 1 + 1];
                foreach (DTOSelloutItem oItem in items)
                {
                    if (oItem.Level == maxLevel)
                    {
                        var oRow = retval.AddRow();
                        for (int j = 0; j <= maxLevel - 1; j++)
                            oRow.AddCell(levels[j]);

                        if (includeSkuRef)
                        {
                            string jsonTag = JsonHelper.Serialize(oItem.Tag, exs);
                            DTOProductSku oSku = JsonHelper.Deserialize<DTOProductSku>(jsonTag, exs);
                            oRow.AddCell(oSku.RefProveidor, oSku.GetUrl(oDomain.DefaultLang(), DTOProduct.Tabs.general, true));
                        }

                        oRow.AddCell(oItem.Concept);

                        oRow.AddFormula("SUM(RC[1]:RC[12])");
                        for (int m = 0; m <= 11; m++)
                            oRow.AddCell(oItem.Values[m]);
                    }
                    else
                        levels[oItem.Level] = oItem.Concept;
                }
            }

            return retval;
        }
    }


    public class DTOSelloutItem
    {
        public DTOSellOut Sellout { get; set; }
        public string Concept { get; set; }
        public Guid Key { get; set; }
        public object Tag { get; set; }
        public Guid Parent { get; set; }
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public int Level { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public List<DTOSelloutItem> Items { get; set; }
        public List<decimal> Values { get; set; }

        public DTOSelloutItem(DTOSellOut oSellout, string sConcept = "")
        {
            Sellout = oSellout;
            Key = Guid.NewGuid();
            Concept = sConcept;
            Items = new List<DTOSelloutItem>();
            Tag = new DTOBaseGuid();
            Values = new List<decimal>();
            for (var i = 1; i <= 12; i++)
                Values.Add(0);
        }

        public decimal Tot()
        {
            decimal retval = Values.Sum();
            return retval;
        }

        public decimal Average()
        {
            decimal retval = Sellout.Tot().Tot() / Sellout.Items.Count;
            return retval;
        }

        public DTOSelloutItem AddItem(string sConcept)
        {
            DTOSelloutItem retval = new DTOSelloutItem(Sellout, sConcept);
            retval.Parent = Key;
            retval.Level = Level + 1;
            Items.Add(retval);
            return retval;
        }

        public bool IsEmpty()
        {
            bool retval = true;
            foreach (var item in Values)
            {
                if (item != 0)
                    retval = false;
            }
            return retval;
        }
    }
}
