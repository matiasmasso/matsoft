using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Excel
{
    public class Row
    {
        public List<Cell> Cells { get; set; }

        public string? Forecolor { get; set; }
        public string? Backcolor { get; set; }

        public Row()
        {
            Cells = new List<Cell>();
        }


        public Cell AddCell(string? content = null, string? url = null)
        {
            var retval = new Cell();
            if (!string.IsNullOrEmpty(content))
            {
                retval.Content = content;
                if (!string.IsNullOrEmpty(url))
                    retval.Url = url;
            }
            retval.NumberFormat = Cell.NumberFormats.PlainText;
            Cells.Add(retval);
            return retval;
        }


        public Cell AddCell(DateTime fch, string url = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Cell();
            if (fch != DateTime.MinValue)
            {
                retval.Content = string.Format("{0:dd/MM/yyyy}", fch);
                if (!string.IsNullOrEmpty(url))
                    retval.Url = url;
                retval.NumberFormat = Cell.NumberFormats.DDMMYY;
            }
            Cells.Add(retval);
            return retval;
        }
        public Cell AddCell(int content, string url = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Cell();
            retval.Content = content.ToString();
            if (!string.IsNullOrEmpty(url))
                retval.Url = url;
            retval.NumberFormat = numberFormat;
            Cells.Add(retval);
            return retval;
        }
        public Cell AddCell(decimal content, string url = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Cell();
            retval.Content = content.ToString();
            if (!string.IsNullOrEmpty(url))
                retval.Url = url;
            retval.NumberFormat = numberFormat;
            Cells.Add(retval);
            return retval;
        }
        public Cell AddCell(double content, string url = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Cell();
            retval.Content = content.ToString();
            if (!string.IsNullOrEmpty(url))
                retval.Url = url;
            retval.NumberFormat = numberFormat;
            Cells.Add(retval);
            return retval;
        }
        public Cell AddFormula(string formulaR1C1, string url = "", Cell.NumberFormats numberFormat = Cell.NumberFormats.NotSet)
        {
            var retval = new Cell();
            retval.FormulaR1C1 = formulaR1C1;
            if (!string.IsNullOrEmpty(url))
                retval.Url = url;
            retval.NumberFormat = numberFormat;
            Cells.Add(retval);
            return retval;
        }


        public string GetString(int CellIdx)
        {
            string retval = "";
            if (CellIdx >= 0 & CellIdx < Cells.Count && Cells[CellIdx].Content != null)
                retval = Cells[CellIdx].Content?.ToString() ?? "";
            return retval;
        }

        public int GetInt(int cellIdx)
        {
            var retval = 0;
            if (Cells.Count > cellIdx )
                retval = Cells[cellIdx].Content == null ? 0 : Convert.ToInt32(Cells[cellIdx].Content);
            return retval;
        }

        public decimal GetDecimal(int cellIdx)
        {
            decimal retval = 0;
            if (Cells.Count > cellIdx )
                retval = Cells[cellIdx].Content == null ? 0 : Convert.ToDecimal(Cells[cellIdx].Content);
            return retval;
        }

        public Guid? GetGuid(int CellIdx)
        {
            Guid? retval = default(Guid?);
            var src = GetString(CellIdx);
            Guid guidCandidate;
            if (Guid.TryParse(src, out guidCandidate))
                retval = guidCandidate;
            return retval;
        }


        public DateTime GetFchSpain(int CellIdx)
        {
            var src = GetString(CellIdx);
            DateTime retval = default(DateTime);
            if (!string.IsNullOrEmpty(src))
            {
                var segments = src.Split('/');
                if (segments.Length >= 3)
                {
                    var day = System.Convert.ToInt32(segments[0]);
                    var month = System.Convert.ToInt32(segments[1]);
                    var year = System.Convert.ToInt32(segments[2].Substring(6, 4));
                    retval = new DateTime(year, month, day);
                }
            }

            return retval.Date;
        }

        //public string CellString(string ColHeader)
        //{
        //    return GetString(CellIdx(ColHeader));
        //}
        //public Guid? CellGuid(string ColHeader)
        //{
        //    return GetGuid(CellIdx(ColHeader));
        //}
        //public int CellInt(string ColHeader)
        //{
        //    return GetInt(CellIdx(ColHeader));
        //}
        //public decimal CellDecimal(string ColHeader)
        //{
        //    return GetDecimal(CellIdx(ColHeader));
        //}
        //public DateTime FchSpain(string ColHeader)
        //{
        //    return GetFchSpain(CellIdx(ColHeader));
        //}

        //public int CellIdx(string ColHeader)
        //{
        //    var retval = -1;
        //    for (int idx = 0; idx <= (Sheet?.Columns.Count ?? 0) - 1; idx++)
        //    {
        //        if (Sheet?.Columns[idx]?.Header?.ToLower() == ColHeader.ToLower())
        //            retval = idx;
        //    }
        //    return retval;
        //}

        public override string ToString()
        {
            return "Row: " + string.Join(";", Cells.Select(x => x.Content?.ToString() ?? ""));
        }

    }
}
