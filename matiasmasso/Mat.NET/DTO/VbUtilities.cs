using System;

namespace DTO
{
    public class VbUtilities
    {
        public static string Choose(int idx, params string[] args)
        {
            string retval = "";
            //base 1
            if (idx > 0 && idx <= args.Length)
            {
                retval = args[idx - 1];
            }
            return retval;
        }

        public static string Left(string src, int chars)
        {
            string retval = src;
            if (retval.isNotEmpty())
            {
                if (chars >= 0 && chars < src.Length)
                {
                    retval = src.Substring(0, chars);
                }
            }
            return retval;
        }

        public static bool isNumeric(string input)
        {
            int result;
            return int.TryParse(input, out result);
        }

        public static string Chr(int src)
        {
            Char character = (char)src;
            return character.ToString();
        }

        public static string Format(int value, string pattern)
        {
            return string.Format("{0:" + pattern + "}", value);
        }
        public static string Format(decimal value, string pattern)
        {
            return string.Format("{0:" + pattern + "}", value);
        }
        public static string Format(DateTime value, string pattern)
        {
            return string.Format("{0:" + pattern + "}", value);
        }

    }
}
