using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Excel
{
    public class Cell
    {
        public string Url { get; set; }
        public Object Content {get; set; }
        public String FormulaR1C1 { get; set; }
        public  int Indent { get; set; } = 0;
        public Alignments Alignment {get; set; }
        public  CellStyles CellStyle { get; set; }

        public NumberFormats NumberFormat { get; set; }


        public enum Alignments
        {
            NotSet,
            Left,
            Center,
            Right
        }

        public enum CellStyles {
            NotSet,
            Bold,
            Italic,
            Total
        }

        public enum NumberFormats
        {
            NotSet,
            PlainText,
            DDMMYY,
            Integer,
            Decimal2Digits,
            Euro,
            W50,
            Percent,
            Kg,
            KgD1,
            m3,
            m3D2,
            mm,
            CenteredText,
            EAN13
        }

        public Cell():base() { }

        public Cell(string content = "", string url = "", MatHelper.Excel.Cell.NumberFormats numberFormat = MatHelper.Excel.Cell.NumberFormats.NotSet)
        {
            if (!string.IsNullOrEmpty(content))
            {
                Content = content;
                if (!string.IsNullOrEmpty(url))
                    Url = url;
            }
            NumberFormat = numberFormat;
        }

        public Cell(decimal content , string url = "", MatHelper.Excel.Cell.NumberFormats numberFormat = MatHelper.Excel.Cell.NumberFormats.NotSet)
        {
                Content = content;
                if (!string.IsNullOrEmpty(url))
                    Url = url;
                NumberFormat = numberFormat == MatHelper.Excel.Cell.NumberFormats.NotSet ? MatHelper.Excel.Cell.NumberFormats.Decimal2Digits : numberFormat;
        }

        public bool IsNotEmpty()
        {
            return (Content != null);
        }

         public override string ToString()
        {
            return Content.ToString();
        }


    }
}
