using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOStat
    {
        public DTOLang Lang { get; set; }
        public int Year { get; set; } = DTO.GlobalVariables.Today().Year;
        public DTORep Rep { get; set; }
        public List<DTORep> Reps { get; set; }
        public ConceptTypes ConceptType { get; set; }
        public DTOCnap Cnap { get; set; }
        public List<DTOCnap> Cnaps { get; set; }
        public DTOArea Area { get; set; }
        public List<DTOArea> Areas { get; set; }
        public DTOCustomer Customer { get; set; }
        public List<DTOCustomer> Customers { get; set; }
        public DTOProveidor Proveidor { get; set; }
        public List<DTOProveidor> Proveidors { get; set; }
        public DTOProduct Product { get; set; }
        public List<DTOProduct> Products { get; set; }
        public DTODistributionChannel DistributionChannel { get; set; }
        public List<DTODistributionChannel> DistributionChannels { get; set; }
        public bool IncludeHidden { get; set; }
        public bool GroupByHolding { get; set; }

        public string Title { get; set; }
        public object SelectedKey { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public List<DTOStatItem> Items { get; set; } = new List<DTOStatItem>();
        public Formats Format { get; set; }
        public int ExpandToLevel { get; set; }

        public List<DTOStatFilter> Filters { get; set; }

        public enum ConceptTypes
        {
            Product,
            Geo,
            Reps,
            Yeas,
            Channels,
            Cnaps
        }

        public enum Formats
        {
            Amounts,
            Units
        }

        public DTOStatItem Tot()
        {
            DTOStatItem retval = new DTOStatItem(this, default(Guid), "totals");
            for (int i = 0; i <= 11; i++)
                retval.Values.Add(0);
            foreach (DTOStatItem oItem in Items)
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



        public DTOStat(DTOStat.ConceptTypes oConceptType = DTOStat.ConceptTypes.Product, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */) : base()
        {
            Lang = oLang;
            ConceptType = oConceptType;
            ExpandToLevel = 0;
            Filters = new List<DTOStatFilter>();
        }

        public MatHelper.Excel.Sheet ExcelSheet()
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(string.Format("M+O - ECI Sellout {0:yyyy.MM.dd}", DTO.GlobalVariables.Today()));
            retval.AddColumn("Centro");
            for (int i = 0; i < 12; i++)
            {
                retval.AddColumn(DTOLang.ESP().MesAbr(i + 1), MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            foreach (DTOStatItem item in Items)
            {
                MatHelper.Excel.Row row = retval.AddRow();
                row.AddCell(item.Concept);
                for (int i = 0; i < 12; i++)
                {
                    row.AddCell(item.Values[i]);
                }
            }
            return retval;
        }

    }

    public class DTOStatFilter
    {
        private Cods _Cod;
        private List<object> _SourceValues;
        public List<object> SelectedValues { get; set; }

        public enum Cods
        {
            NotSet,
            Year,
            UnitsOrAmounts,
            Product,
            Area,
            Rep,
            Customer,
            Proveidor
        }

        public DTOStatFilter(Cods oCod, List<object> oSourceValues) : base()
        {
            _Cod = oCod;
            _SourceValues = oSourceValues;
            SelectedValues = new List<object>();
        }

        public Cods Cod
        {
            get
            {
                return _Cod;
            }
        }
    }



    public class DTOStatItem
    {
        public DTOStat Stat { get; set; }
        public string Concept { get; set; }
        public Guid Key { get; set; }
        public DTOBaseGuid Tag { get; set; }
        public Guid Parent { get; set; }
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public int Level { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public List<DTOStatItem> Items { get; set; }
        public List<decimal> Values { get; set; }

        public DTOStatItem(DTOStat oStat, Guid oKey, string sConcept)
        {
            Stat = oStat;
            Key = oKey;
            Concept = sConcept;
            Items = new List<DTOStatItem>();
            Values = new List<decimal>();
            for (var i = 1; i <= 12; i++)
                Values.Add(0);
        }

        public decimal Tot()
        {
            decimal retval = 0;
            foreach (decimal oValue in Values)
                retval += oValue;
            return retval;
        }

        public decimal Average()
        {
            decimal retval = Stat.Tot().Tot() / Stat.Items.Count;
            return retval;
        }
    }


}
