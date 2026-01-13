using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Excel
{
    public class Sheet
    {
        public string? Name { get; set; }
        public string? Filename { get; set; }
        //public System.Globalization.CultureInfo? CultureInfo { get; set; }
        public List<Column> Columns { get; set; } = new();
        public List<Row> Rows { get; set; } = new();

        public List<RowGroup> RowGroups { get; set; } = new();
        public bool DisplayTotals { get; set; }

        public bool ColumnHeadersOnFirstRow { get; set; }

        public Column.HeaderRowStyles HeaderRowStyle { get; set; }

        public double? FontSize { get; set; } = null;

        public Sheet() { }

        public Sheet(string sheetName = "", string fileName = "")
        {
            Name = sheetName;
            Filename = fileName;

        }

        public Column AddColumn(string header = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Column(header, numberFormat);
            Columns.Add(retval);
            retval.Min = Columns.Count();
            retval.Max = retval.Min;
            return retval;
        }

        public void AddRowGroup(int firstRow, int lastRow)
        {
            RowGroups.Add(new RowGroup { FirstRow = firstRow, LastRow = lastRow });
        }

        public List<Column> AllColumns()
        {
            var retval = new List<Column>();
            for (var i = 0; i < MaxRowCells(); i++)
                retval.Add(new Column());
            return retval;
        }

        public Row AddRow()
        {
            var retval = new Row();
            Rows.Add(retval);
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
            for (int i = 0; i < cellCount; i++)
            {
                retval.AddCell();
            }
            return retval;
        }

        public bool MatchHeaderCaptions(params string[] Captions)
        {
            bool retval = false;
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

        public int MaxRowCells() => Rows.Select(x=>x.Cells.Count).Max();

        public List<Row> RowsRange(int firstRow, int lastRow)
        {
            var retval = new List<Row>();
            for (int i = firstRow; i <= lastRow; i++)
            {
                if (i >= Rows.Count) break;
                retval.Add(Rows[i]);
            }
            return retval;
        }

        public override string ToString()
        {
            return String.Format("{Excel.Sheet: {0}}", Name);
        }
    }


    public class ReadException
    {
        public int Row { get; set; }
        public string? Msg { get; set; }

        public static ReadException Factory(int row, string msg)
        {
            ReadException retval = new ReadException();
            retval.Row = row;
            retval.Msg = msg;
            return retval;
        }
    }
}
