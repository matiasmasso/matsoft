using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static List<string> GetLines(this string str, bool removeEmptyLines = false)
    {var retval = new List<string>();
        if(str != null)
        {
            retval = str.Split(new[] { "\r\n", "\r", "\n" },
            removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None)
            .ToList();
        }
        return retval;
    }

    public static string RemoveAccents(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;

        // Normalize the string to decompose accented characters into base characters + diacritics
        var normalized = str.Normalize(NormalizationForm.FormD);

        // Use a StringBuilder to filter out non-spacing marks (diacritics)
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        // Normalize back to Form C (composed form)
        return sb.ToString().Normalize(NormalizationForm.FormC);
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
        if (!string.IsNullOrEmpty(strEnumValue)) {
            try
            {
                retval = (TEnum?)Enum.Parse(typeof(TEnum), strEnumValue, true);
            }catch (System.Exception)
            {
                retval = default(TEnum);
            }
        }
        return retval;
    }
}

