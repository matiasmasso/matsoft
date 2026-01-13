using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using DTO.Integracions.Banca;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pos">zero index</param>
    /// <param name="maxLength">substring max length</param>
    /// <returns></returns>
    public static string Truncate(this string value, int pos, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        else if (value.Length <= pos + 1) return string.Empty;
        else
            return value.Substring(pos).Truncate(maxLength);
    }

    public static bool IsGuid(this string src) => Guid.TryParse(src, out _);
    public static bool IsNorma43(this string src) => Norma43.Validate(src);
    public static List<string> GetLines(this string str, bool removeEmptyLines = false)
    {
        var retval = new List<string>();
        if (str != null)
        {
            retval = str.Split(new[] { "\r\n", "\r", "\n" },
            removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None)
            .ToList();
        }
        return retval;
    }


    //inserts a <br/> after each previous line unless it begins with a table tag
    public static string Html(this string textWithNewlines)
    {

        var sb = new StringBuilder();
        var lines = textWithNewlines.GetLines();
        if (lines != null)
        {
            for (var i = 1; i < lines.Count(); i++)
            {
                var line = lines[i];
                if (line.StartsWith("<td") ||
                    line.StartsWith("<tr") ||
                    line.StartsWith("<more") ||
                    line.StartsWith("</td") ||
                    line.StartsWith("</tr") ||
                    line.StartsWith("</table")) { }
                else
                    lines[i - 1] += "<br/>";
            }
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }
        }
        var retval = sb.ToString();
        return retval;
    }


    public static TEnum? ToEnum<TEnum>(this string strEnumValue)
    {
        TEnum? retval = default(TEnum);
        if (!string.IsNullOrEmpty(strEnumValue))
        {
            try
            {
                retval = (TEnum?)Enum.Parse(typeof(TEnum), strEnumValue, true);
            }
            catch (System.Exception)
            {
                retval = default(TEnum);
            }
        }
        return retval;
    }

    public static DateTime? ToDate(this string value)
    {
        int year, month, day;
        DateTime? retval = null;
        switch (value.Length)
        {
            case 6:
                year = (value.Truncate(0, 2).ToInteger() ?? 0) + 2000;
                month = value.Truncate(2, 2).ToInteger() ?? 0;
                day = (value.Truncate(4, 2).ToInteger() ?? 0);
                retval = new DateTime(year, month, day);
                break;
            case 8:
                year = (value.Truncate(0, 4).ToInteger() ?? 0);
                month = value.Truncate(4, 2).ToInteger() ?? 0;
                day = value.Truncate(6, 2).ToInteger() ?? 0;
                retval = new DateTime(year, month, day);
                break;
        }
        return retval;
    }
    public static DateOnly? ToDateOnly(this string value)
    {
        int year, month, day;
        DateOnly? retval = null;
        switch (value.Length)
        {
            case 6:
                year = (value.Truncate(0, 2).ToInteger() ?? 0) + 2000;
                month = value.Truncate(2, 2).ToInteger() ?? 0;
                day = (value.Truncate(4, 2).ToInteger() ?? 0);
                retval = new DateOnly(year, month, day);
                break;
            case 8:
                year = (value.Truncate(0, 4).ToInteger() ?? 0);
                month = value.Truncate(4, 2).ToInteger() ?? 0;
                day = value.Truncate(6, 2).ToInteger() ?? 0;
                retval = new DateOnly(year, month, day);
                break;
        }
        return retval;
    }

    public static string TrimAtNearestWhiteSpace(this string src, int pos)
    {
        string retval = src;
        if (!string.IsNullOrEmpty(src) && src.Length > pos)
        {
            //get a sorted list of white space indexes
            var whiteSpaceIndexes = new List<int>();
            for (int i = 0; i < src.Length; i++)
                if (src[i] == ' ') whiteSpaceIndexes.Add(i);

            // let the whole source be an option if close to target position
            whiteSpaceIndexes.Add(src.Length);

            //compare nearest white space positions
            var nextSpace = whiteSpaceIndexes.FirstOrDefault(x => x >= pos);
            whiteSpaceIndexes.Reverse();
            var prevSpace = whiteSpaceIndexes.FirstOrDefault(x => x < pos);
            var bestDelta = nextSpace - pos < pos - prevSpace ? nextSpace : prevSpace;

            //add ellipsis if return value is trimmed
            if (bestDelta < src.Length)
                retval = src.Substring(0, bestDelta) + "...";
        }
        return retval;
    }

    public static bool Matches(this string fullSentence, string searchString)
    {
        var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
        var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;

        var Index = compareInfo.IndexOf(fullSentence, searchString, options);
        var retval = Index >= 0;
        return retval;
    }

    /// <summary>
    /// Splits a string into a list of specific length
    /// </summary>
    /// <param name="src"></param>
    /// <param name="itemLength"></param>
    /// <returns></returns>
    public static List<string> SplitByLength(this string src, int itemLength)
    {
        return Enumerable.Range(0, (int)src.Length / (int)itemLength).Select(x => src.Substring(x * itemLength, itemLength)).ToList();
    }

    public static DTO.Amt? ParseAmt(this string src, CultureInfo? culture = null)
    {
        decimal? value = src.ParseDecimal(culture);
        var retval = value == null ? null : new DTO.Amt((decimal)value);
        return retval;
    }

    public static decimal? ParseDecimal(this string src, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo("es-ES");
        decimal? retval = null;
        if (!string.IsNullOrEmpty(src))
            retval = decimal.Parse(src, NumberStyles.Number, culture);
        return retval;
    }

    public static int? ToInteger(this string src, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo("es-ES");
        int? retval = null;
        if (!string.IsNullOrEmpty(src))
            retval = int.Parse(src, NumberStyles.Number, culture);
        return retval;
    }

    /// <summary>
    ///     A string extension method that query if '@this' is numeric.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if numeric, false if not.</returns>
    public static bool IsNumeric(this string @this, System.Globalization.CultureInfo? culture = null)
    {
        culture ??= CultureInfo.InvariantCulture;
        var trimmed = @this.Trim();
        var retval = decimal.TryParse(trimmed, culture, out _);
        return retval;
    }

    public static string TruncateWithEllipsis(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
    }
}

