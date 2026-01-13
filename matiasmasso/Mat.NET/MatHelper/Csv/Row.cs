using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Csv
{
    public class Row
    {
        public List<string> Cells { get; set; }

        public Row()
        {
            Cells = new List<string>();
        }

        public override string ToString()
        {
            return String.Join(";", Cells.ToArray());
        }
    }
}
