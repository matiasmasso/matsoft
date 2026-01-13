using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Excel
{
    public class Sheet
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public System.Globalization.CultureInfo CultureInfo { get; set; }
        public List<Column> Columns { get; set; }
        public List<Row> Rows { get; set; } 
        public bool DisplayTotals { get; set; }

        public bool ColumnHeadersOnFirstRow { get; set; }   

        public Column.HeaderRowStyles HeaderRowStyle { get; set; }

        public Sheet() {
            Columns = new List<Column>();
            Rows = new List<Row>();
        }

        public Sheet(string sheetName = "", string fileName = "")
        {
            Columns = new List<Column>();
            Rows = new List<Row>();
            Name = sheetName;
            Filename = fileName;
            
        }

        public Column AddColumn(string header = "", MatHelper.Excel.Cell.NumberFormats numberFormat = MatHelper.Excel.Cell.NumberFormats.NotSet)
        {
            var retval = new Column(header, numberFormat);
            Columns.Add(retval);
            retval.Min = Columns.Count();
            retval.Max = retval.Min;
            return retval;           
        }

        public Row AddRow()
        {
            var retval = new Row();
            Rows.Add(retval);
            retval.Sheet = this;
            return retval;
        }

        public Row AddRowWithCells(params string[] CellTexts)
        {
            var retval = AddRow();
            foreach (string sCellText in CellTexts)
                retval.AddCell(sCellText);
            return retval;
        }

        public Row AddRowWithEmptyCells(int cellCount)
        {
            var retval = AddRow();
            for(int i=0;i<cellCount;i++)
            {
                retval.AddCell();
            }
            return retval;
        }

        public bool MatchHeaderCaptions(params string[] Captions)
        {
            bool retval =false;
            if (Rows.Count > 0)
            {
                var oFirstRow = Rows[0];
                if (Captions.Count() <= oFirstRow.Cells.Count)
                {
                    retval = true;
                    for (var i = 0; i <= Captions.Count() - 1; i++)
                    {
                        if (oFirstRow.GetString(i).Trim() != Captions[i].Trim())
                        {
                            retval = false;
                            break;
                        }
                    }
                }
            }
            return retval;
        }

        public void TrimCols(int maxCols)
        {
            foreach (var oRow in Rows)
            {
                for (var Col = oRow.Cells.Count - 1; Col >= maxCols - 1; Col += -1)
                    oRow.Cells.RemoveAt(Col);
            }
        }

        public static Sheet Factory(System.Data.DataSet oDs)
        {
            Sheet retval = new Sheet();
            System.Data.DataTable oTb = oDs.Tables[0];

            int j;

            for (j = 0; j <= oTb.Columns.Count - 1; j++)
            {
                    retval.AddColumn(oTb.Columns[j].Caption);
            }

            foreach (System.Data.DataRow oDataRow in oTb.Rows)
            {
                Row oRow = retval.AddRow();
                for (j = 0; j <= oTb.Columns.Count - 1; j++)
                {
                        oRow.AddCell(oDataRow[j].ToString());
                }
            }

            return retval;
        }


        public override string ToString()
        {
            return String.Format("{Excel.Sheet: {0}}",Name);
        }
    }

    public class ReadException
    {
        public int Row { get; set; }
        public string Msg { get; set; }

        public static ReadException Factory(int row, string msg)
        {
            ReadException retval = new ReadException();
            retval.Row = row;
            retval.Msg = msg;
            return retval;
        }
    }
}
