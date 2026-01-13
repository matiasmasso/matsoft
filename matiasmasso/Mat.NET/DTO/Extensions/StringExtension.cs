using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DTO
{
    public static class StringExtensions
    {
            public static string Truncate(this string value, int maxLength)
            {
                if (string.IsNullOrEmpty(value)) return value;
                return value.Length <= maxLength ? value : value.Substring(0, maxLength);
            }


        public static bool isNotEmpty(this string src)
        {
            return (!string.IsNullOrEmpty(src));
        }

        public static int toInteger(this string value)
        {
            int retval = 0;
            try
            {
                retval = Int32.Parse(value);
            }

            catch (FormatException)
            {
                retval = 0;
            }
            return retval;
        }
        public static long toBigInteger(this string value)
        {
            long retval = 0;
            try
            {
                retval = long.Parse(value);
            }

            catch (FormatException)
            {
                retval = 0;
            }
            return retval;
        }
        public static string right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        public static bool isGreaterThan(this string src, string candidate)
        {
            int comparison = String.Compare(src, candidate, comparisonType: StringComparison.OrdinalIgnoreCase);
            return (comparison > 0);
        }
        public static bool isGreaterOrEqualThan(this string src, string candidate)
        {
            int comparison = String.Compare(src, candidate, comparisonType: StringComparison.OrdinalIgnoreCase);
            return (comparison >= 0);
        }
        public static bool isLowerThan(this string src, string candidate)
        {
            int comparison = String.Compare(src, candidate, comparisonType: StringComparison.OrdinalIgnoreCase);
            return (comparison < 0);
        }
        public static bool isLowerOrEqualThan(this string src, string candidate)
        {
            int comparison = String.Compare(src, candidate, comparisonType: StringComparison.OrdinalIgnoreCase);
            bool retval = (comparison <= 0);
            return retval;
        }
        public static string toSingleLine(this string src)
        {
            List<string> lines = System.Text.RegularExpressions.Regex.Split(src, "\r\n|\r|\n").ToList();
            lines.RemoveAll(x => string.IsNullOrEmpty(x));
            string retval = "";
            if (lines.Count > 0)
                retval = lines.Count > 1 ? string.Join("-", lines) : lines.First();
            return retval;
        }
        public static List<String> toLinesList(this string src)
        {
            string[] splitted = src.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<String> retval = splitted.ToList();
            return retval;
        }
        public static string toMultiLineHtml(this string src)
        {
            string br = "<br/>";
            string[] splitted = src.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string retval = string.Join(br, splitted);
            if (!retval.EndsWith(br))
            {
                retval += br;
            }
            return retval;
        }

        public static IEnumerable<String> splitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ReplaceNotExpectedCharacters(this string text, string allowedPattern, string replacement)
        {
            //[^ ] at the start of a character class negates it - it matches characters not in the class.
            string retval = "";
            if (!string.IsNullOrEmpty(text))
                retval = Regex.Replace(text, @"[^" + allowedPattern + "]", replacement);
            return retval; //returns result free of negated chars
        }

        public static string Urlfy(this string src)
        {
            string charset = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\-_~";
            string removedAccents = src.Trim().RemoveDiacritics();
            string removedWhiteSpaces = Regex.Replace(removedAccents, @" ", "-");
            string removedDots = Regex.Replace(removedWhiteSpaces, @"\.", "-");
            string blanksAndDotsReplacedWithDashes = removedDots.ReplaceNotExpectedCharacters(charset, "");
            string retval = RemoveDuplicateDashes(blanksAndDotsReplacedWithDashes);
            return retval.ToLower();
        }


        public static string RemoveDuplicateDashes(string src)
        {
            string retval = "";
            if (src.Length < 2)
                retval = src;
            else
            {
                retval = src.Substring(0, 1);
                for (int i = 1; i < src.Length; i++)
                {
                    string lastChar = retval.Substring(retval.Length - 1, 1);
                    string nextChar = src.Substring(i, 1);
                    if (lastChar != "-" || nextChar != "-")
                        retval += nextChar;
                }
            }

            return retval;
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source?.IndexOf(toCheck, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public static bool IsNumericStrict(this string source)
        {
            return Regex.IsMatch(source, @"^[0-9]+$");
        }

    }
}
