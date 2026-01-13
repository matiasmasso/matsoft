using MatHelperStd;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOBalance
    {
        public DTOEmp emp { get; set; }
        public DTOYearMonth yearMonthFrom { get; set; }
        public DTOYearMonth yearMonthTo { get; set; }
        public List<DTOPgcClass> items { get; set; }

        public static DTOBalance Factory(DTOEmp oEmp, DTOYearMonth YearMonthFrom, DTOYearMonth YearMonthTo)
        {
            DTOBalance retval = new DTOBalance();
            {
                var withBlock = retval;
                withBlock.emp = oEmp;
                withBlock.yearMonthFrom = YearMonthFrom;
                withBlock.yearMonthTo = YearMonthTo;
                withBlock.items = new List<DTOPgcClass>();
            }
            return retval;
        }

        public List<DTOYearMonth> YearMonths(DTOPgcClass oPgcClass)
        {
            List<DTOPgcCta> oCtas = new List<DTOPgcCta>();
            if (oPgcClass.Ctas != null && oPgcClass.Ctas.Count > 0)
                oCtas = oPgcClass.Ctas;
            else
                GetPgcCtas(oPgcClass, oCtas);
            var oYearMonths = oCtas.SelectMany(x => x.YearMonths).ToList();
            var retval = oYearMonths.GroupBy(g => new { g.Year, g.Month }).Select(group => new DTOYearMonth() { Year = group.Key.Year, Month = group.Key.Month, Eur = group.Sum(x => x.Eur) }).ToList();
            return retval;
        }

        public List<DTOYearMonth> YearMonths(DTOPgcClass.Cods oCod)
        {
            var oCtas = Ctas(oCod);
            var oYearMonths = oCtas.SelectMany(x => x.YearMonths).ToList();
            var retval = oYearMonths.GroupBy(g => new { g.Year, g.Month }).Select(group => new DTOYearMonth() { Year = group.Key.Year, Month = group.Key.Month, Eur = group.Sum(x => x.Eur) }).ToList();
            return retval;
        }

        public List<DTOPgcCta> Ctas(DTOPgcClass.Cods oCod)
        {
            List<DTOPgcCta> retval = new List<DTOPgcCta>();
            var oClass = GetPgcClass(oCod);
            if (oClass != null)
                GetPgcCtas(oClass, retval);
            return retval;
        }

        public DTOPgcClass GetPgcClass(DTOPgcClass.Cods oCod, List<DTOPgcClass> oParentsFrom = null)
        {
            DTOPgcClass retval = null/* TODO Change to default(_) if this is not a reference type */;

            if (oParentsFrom == null)
                oParentsFrom = items;

            foreach (DTOPgcClass oClass in oParentsFrom)
            {
                if (oClass.Cod == oCod)
                {
                    retval = oClass;
                    break;
                }
                else if (oClass.Children.Count > 0)
                {
                    retval = GetPgcClass(oCod, oClass.Children);
                    if (retval != null)
                        break;
                }
            }
            return retval;
        }

        public void GetPgcCtas(DTOPgcClass oParentFrom, List<DTOPgcCta> oCtas)
        {
            foreach (DTOPgcClass oClass in oParentFrom.Children)
            {
                if (oClass.Ctas != null && oClass.Ctas.Count > 0)
                    oCtas.AddRange(oClass.Ctas);
                if (oClass.Children != null && oClass.Children.Count > 0)
                    GetPgcCtas(oClass, oCtas);
            }
        }

        public int YearMonthsCount()
        {
            return DTOYearMonth.MonthsDiff(yearMonthFrom, yearMonthTo);
        }


        public DTOKpi getKpi(DTOKpi.Ids id)
        {
            DTOKpi retval = new DTOKpi();
            retval.Id = id;
            retval.Caption = id.ToString().Replace("_", " ");
            switch (id)
            {
                case DTOKpi.Ids.Activo_Corriente:
                    {
                        retval.YearMonths = YearMonths(DTOPgcClass.Cods.aAB_Activo_Corriente);
                        break;
                    }

                case DTOKpi.Ids.Pasivo_Corriente:
                    {
                        retval.YearMonths = YearMonths(DTOPgcClass.Cods.aBC_Pasivo_Corriente);
                        break;
                    }
            }
            return retval;
        }

        public MatHelper.Excel.Book ExcelBook(List<DTOKpi> oKpis, DTOLang oLang)
        {
            MatHelper.Excel.Book retval = new MatHelper.Excel.Book();
            if (oKpis.Count > 0)
                retval.Sheets.Add(ExcelSheetKpis(oKpis, oLang));
            retval.Sheets.Add(ExcelSheet(oLang));
            return retval;
        }

        public MatHelper.Excel.Sheet ExcelSheet(DTOLang oLang)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            retval.AddColumn("concepte");
            var oYearMonths = DTOYearMonth.Range(yearMonthFrom, yearMonthTo);
            foreach (var oYearMonth in oYearMonths)
                retval.AddColumn(oYearMonth.Caption(oLang), MatHelper.Excel.Cell.NumberFormats.Euro);

            foreach (var oClass in items)
                AddRow(retval, oClass, oLang);
            return retval;
        }

        private void AddRow(MatHelper.Excel.Sheet oSheet, DTOPgcClass oClass, DTOLang oLang)
        {
            var oRow = oSheet.AddRow();
            var paddingLeft = new string(' ', oClass.GetLevel() * 4);
            oRow.AddCell(paddingLeft + oClass.Nom.Tradueix(oLang));
            var oYearMonths = DTOYearMonth.Range(yearMonthFrom, yearMonthTo);
            if (oClass.YearMonths != null && oClass.YearMonths.Count > 0)
            {
                foreach (var oYearMonth in oYearMonths)
                {
                    var pYearMonth = oClass.YearMonths.FirstOrDefault(x => x.Equals(oYearMonth));
                    if (pYearMonth == null)
                        oRow.AddCell();
                    else
                        oRow.AddCell(pYearMonth.Eur);
                }
            }

            if (oClass.Ctas != null && oClass.Ctas.Count > 0)
            {
                paddingLeft = new string(' ', (oClass.GetLevel() + 1) * 4);
                foreach (var oCta in oClass.Ctas)
                {
                    oRow = oSheet.AddRow();
                    oRow.AddCell(string.Format("{0} {1} {2}", paddingLeft, oCta.Id, oCta.Nom.Tradueix(oLang)));
                    foreach (var oYearMonth in oYearMonths)
                    {
                        var pYearMonth = oCta.YearMonths.FirstOrDefault(x => x.Equals(oYearMonth));
                        if (pYearMonth == null)
                            oRow.AddCell();
                        else
                            oRow.AddCell(pYearMonth.Eur);
                    }
                }
            }
            if (oClass.Children != null && oClass.Children.Count > 0)
            {
                foreach (var oChild in oClass.Children)
                    AddRow(oSheet, oChild, oLang);
            }
        }




        public MatHelper.Excel.Sheet ExcelSheetKpis(List<DTOKpi> oKpis, DTOLang oLang)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            retval.AddColumn("any");
            retval.AddColumn("mes");
            foreach (var oKpi in oKpis)
                retval.AddColumn(oKpi.Caption, MatHelper.Excel.Cell.NumberFormats.Euro);
            for (int i = 0; i <= YearMonthsCount() - 1; i++)
            {
                var oYearMonth = yearMonthFrom.Addmonths(i);
                var oRow = retval.AddRow();
                oRow.AddCell(oYearMonth.Year);
                oRow.AddCell(oLang.MesAbr((int)oYearMonth.Month));
                foreach (var oKpi in oKpis)
                {
                    var kYearMonth = oKpi.YearMonths.FirstOrDefault(x => x.Equals(oYearMonth));
                    if (kYearMonth == null)
                        oRow.AddCell();
                    else
                        oRow.AddCell(kYearMonth.Eur);
                }
            }
            return retval;
        }
    }
}
