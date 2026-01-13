using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Excel
{
    public class Column
    {
        public string? Header { get; set; }
        public Cell.NumberFormats NumberFormat { get; set; }
        public Double Width { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public bool CustomWidth { get; set; }


        public enum HeaderRowStyles
        {
            NotSet,
            ElCorteIngles
        }

        public Column() { }

        public Column(string header = "",Cell.NumberFormats numberFormat =Cell.NumberFormats.NotSet)
        {
            Header = header;
            NumberFormat = numberFormat;
            //Width = 16;
            //Min = 1;
            //Max = 1;
            //CustomWidth = true;
        }

        public override string ToString()
        {
            return String.Format("{Excel.Column: {0}}", Header);
        }

    }
}
