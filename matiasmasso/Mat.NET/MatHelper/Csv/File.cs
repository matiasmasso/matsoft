using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatHelper.Csv
{
    public class File
    {
        public string Filename { get; set; }
        public List<Row> Rows { get; set; }

        public File() {
            Rows = new List<Row> ();
        }

        public static File Factory(Byte[] bytes, string filename = null)
        {
            var text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var lines = Regex.Split(text, "\r\n|\r|\n").ToList();
            var retval = new File();
            foreach(var line in lines)
            {
                var row = new Row();
                row.Cells = line.Split(';').ToList();
                retval.Rows.Add(row);
            }
            return retval;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var row in Rows)
            {
                sb.AppendLine(row.ToString());
            }
            return sb.ToString();
        }
    }
}
