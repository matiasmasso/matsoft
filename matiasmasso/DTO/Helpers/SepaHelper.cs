using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Helpers
{
    public class SepaHelper
    {
        public static System.Globalization.CultureInfo InvariantCulture = System.Globalization.CultureInfo.InvariantCulture;

        public static string Normalize(string? src)
        {
            if (string.IsNullOrEmpty(src))
                return string.Empty;
            else
            {
                char[] validChars = CharsAllowed();
                var sb = new System.Text.StringBuilder();
                foreach (char oChar in src.ToArray())
                {
                    if (validChars.Contains(oChar))
                        sb.Append(oChar);
                    else
                    {
                        int iHex32 = Convert.ToInt32(oChar);
                        string HexValue = iHex32.ToString("X4");
                        string sAlt = string.Format(@"\u{0}", HexValue);
                        sb.Append(sAlt);
                    }
                }
                string retval = sb.ToString();
                return retval;
            }
        }
        public static string Normalize(int src) => src.ToString("N0", InvariantCulture);
        public static string Normalize(decimal src) => src.ToString("F2", InvariantCulture);
        public static string Normalize(DateTime src) => src.ToString("yyyy-MM-ddTHH:mm:ss");
        public static string Normalize(DateOnly src) => src.ToString("yyyy-MM-dd");


        public static char[] CharsAllowed()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("abcdefghijklmnopqrstuvwxyz");
            sb.AppendLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            sb.AppendLine("0123456789");
            sb.AppendLine("/-?:().,'+ ");
            string src = sb.ToString();
            char[] retval = src.ToArray();
            return retval;
        }
    }

}
