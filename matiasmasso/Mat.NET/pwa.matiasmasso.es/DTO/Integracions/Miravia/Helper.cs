using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Miravia
{
    public class Helper
    {
        public const string CsvFieldSeparator = ",";

        public static async Task<string> ToCsv(List<string>rows)
        {
            string retval = "";
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms, Encoding.UTF8))
                {
                    foreach (var row in rows)
                    {
                        await sw.WriteLineAsync(row);
                    }
                    sw.Flush(); // prevents result from being truncated
                    retval = System.Text.Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
                }
            }
            return retval;
        }

        public static string NoDecimals(int? val)
        {
            var sVal = val?.ToString("0", CultureInfo.InvariantCulture) ?? "";
            return Text(sVal);
        }

        public static string SingleDecimal(decimal? val)
        {
            var sVal = val?.ToString("0.0", CultureInfo.InvariantCulture) ?? "";
            return Text(sVal);
        }

        public static string TwoDecimals(decimal? val)
        {
            var sVal = val?.ToString("0.00", CultureInfo.InvariantCulture) ?? "";
            return Text(sVal);
        }

        public static string Text(string? val)
        {
            var retval = val;
            if (!string.IsNullOrEmpty(retval) && retval.Contains(","))
                retval = "\"" + (val?.Trim() ?? "") + "\"";
            return retval ?? "";
        }
    }
}
