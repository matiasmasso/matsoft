using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Excel
{
    public class ColMapping
    {
        public string? Caption { get; set; }

        //the original Excel file source Zero based column index
        //from which to fill this caption column
        public int SrcIdx { get; set; } = -1;

        public static List<ColMapping>? Factory(params string[]? args)
        {
            var items = args?.ToList();
            var retval = items?.Select(x => new ColMapping { Caption=x}).ToList();
            return retval;
        }
    }
}
