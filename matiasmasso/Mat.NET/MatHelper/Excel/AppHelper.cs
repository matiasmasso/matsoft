using System;
using System.Collections.Generic;
using System.Linq;
using Xls = Microsoft.Office.Interop.Excel;

namespace MatHelper.Excel
{

    public class AppHelper
    {
        private static Xls.Application _App;

        public static Xls.Application ExcelApp(List<Exception> exs)
        {
            if (_App == null)
            {
                try
                {
                    _App = new Xls.Application();
                }
                catch (Exception ex)
                {
                    exs.Add(ex);
                }
            }
            return _App;
        }

        public static void Quit()
        {
            if (_App != null)
            {
                _App.Quit();
                _App = null;
            }
        }


        public static string GetXpsFileNameFromExcelFileName(List<Exception> exs, string sExcelFilename, string sXpsFilename = "")
        {
            var oApp = ExcelApp(exs);
            if (exs.Count == 0)
            {
                Xls.Workbook workbook = oApp.Workbooks.Open(sExcelFilename);

                if (sXpsFilename == "")
                    sXpsFilename = sExcelFilename + ".xps";
                workbook.ExportAsFixedFormat(Xls.XlFixedFormatType.xlTypeXPS, sXpsFilename, From: 1, To: 1);
            }

            return sXpsFilename;
        }

        public static List<string> GetSheetNames(List<Exception> exs, string sFilename)
        {
            List<string> retval = new List<string>();
            var oApp = ExcelApp(exs);
            if (exs.Count == 0)
            {
                Xls.Workbook oWb = oApp.Workbooks.Open(sFilename);
                foreach (Xls.Worksheet oSheet in oWb.Worksheets)
                    retval.Add(oSheet.Name);
            }
            return retval;
        }


        public static List<string> GetColumnNames(List<Exception> exs, string sFilename, string sSheetName = "")
        {
            List<string> retval = new List<string>();
            var oApp = ExcelApp(exs);
            if (exs.Count == 0)
            {
                Xls.Workbook oWb = oApp.Workbooks.Open(sFilename);

                Xls.Worksheet oSheet = null/* TODO Change to default(_) if this is not a reference type */;
                if (sSheetName == "")
                    oSheet = (Xls.Worksheet)oWb.ActiveSheet;
                else
                    oSheet = (Xls.Worksheet)oWb.Sheets[sSheetName];


                Xls.Range cellsRange = (Xls.Range)oSheet.Cells[1, oSheet.Columns.Count];
                int LastCol = cellsRange.End[Xls.XlDirection.xlToLeft].Column;
                for (int iCol = 1; iCol <= LastCol; iCol++)
                {
                    var cell = (Xls.Range)oSheet.Cells[1, iCol];
                    var value = cell.Value.ToString();
                    retval.Add(value);
                }
            }
            return retval;
        }
    }
}

