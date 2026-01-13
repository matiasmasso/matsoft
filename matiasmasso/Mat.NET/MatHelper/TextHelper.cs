using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace MatHelper
{
    public class TextHelper
    {
        public static string Html(string textWithNewlines)
        {
            var retval = "";
            if (!string.IsNullOrEmpty(textWithNewlines))
            {
                var lines =  Regex.Split(textWithNewlines, "\r\n|\r|\n");
                //if (lines.Count() == 1)
                //    lines = textWithNewlines.Split(Constants.vbLf);
                // Dim lines = textWithNewlines.Split(Environment.NewLine)
                for (int i = 1; i <= lines.Length - 1; i++)
                {
                    var line = lines[i].Replace(Constants.vbLf, "");
                    bool skip = line.StartsWith("<td") | line.StartsWith("<tr") | line.StartsWith("</td") | line.StartsWith("</tr") | line.StartsWith("</table") | line.StartsWith("<more");
                    if (!skip)
                        lines[i - 1] = lines[i - 1] + "<br/>";
                }
                retval = string.Join(Environment.NewLine, lines);
            }
            return retval;
        }

        public static string VbFormat(int src, string pattern)
        {
            var retval = string.Format("{0:" + pattern + "}", src);
            return retval;
        }

        public static string VbFormat(decimal src, string pattern)
        {
            var retval = string.Format("{0:" + pattern + "}", src);
            return retval;
        }

        public static string VbFormat(DateTime dtFch, string pattern)
        {
            var retval = dtFch.ToString(pattern);
            return retval;
        }

        public static string VbChoose(int idx, params string[] values)
        {
            string retval = "";
            if (idx < values.Length)
                retval = values[idx - 1];
            return retval;
        }

        /// <summary>
        ///     ''' returns de AsCIICode of the first character
        ///     ''' </summary>
        ///     ''' <param name="UTF8String">input string</param>
        ///     ''' <returns></returns>
        public static int VbAsc(string UTF8String)
        {
            byte[] oByteArray = System.Text.Encoding.UTF8.GetBytes(UTF8String);
            int retval = oByteArray[0];
            return retval;
        }

        public static string VbChr(int src)
        {
            return ((char)src).ToString();
        }

        public static string VbLeft(string src, int len)
        {
            string retval = src.Trim();
            if (len < src.Length)
                retval = retval.Substring(0, len);
            return retval;
        }

        public static string VbRight(string src, int len)
        {
            string retval = src.Trim();
            if (len < src.Length)
                retval = retval.Substring(retval.Length - len, len);
            return retval;
        }

        public static string VbMid(string src, int pos, int len = 0)
        {
            string retval = src.Trim();
            if (pos < retval.Length)
                retval = retval.Substring(pos - 1, retval.Length - (pos - 1));
            if (len < retval.Length)
                retval = retval.Substring(0, len);
            return retval;
        }


        public static bool VbIsNumeric(string src)
        {
            return int.TryParse(src, out _);
        }

        public static bool Match(string fullSentence, string searchString)
        {
            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;

            var Index = compareInfo.IndexOf(fullSentence, searchString, options);
            var retval = Index >= 0;
            return retval;
        }

        public static List<string> MatchingSegments(List<string> oSegments, string sPattern)
        {
            List<string> retval = oSegments.Where(x => Regex.Matches(x, sPattern).Count > 0).ToList();
            return retval;
        }

        public static List<string> splitByLength(string src, int itemLength)
        {
            var retval = Enumerable.Range(0, (int)src.Length / (int)itemLength).Select(x => src.Substring(x * itemLength, itemLength)).ToList();
            return retval;
        }

        public static string Excerpt(string sLongText, int MaxLen = 0, bool BlAppendEllipsis = true)
        {
            if (!string.IsNullOrEmpty(sLongText))
            {
                if (sLongText.IndexOf("<more/>") >= 0)
                    sLongText = sLongText.Substring(0, sLongText.IndexOf("<more/>"));
                else if (!string.IsNullOrEmpty(sLongText))
                {
                    if (MaxLen > 0)
                    {
                        string ellipsis = BlAppendEllipsis ? "..." : "";
                        if (sLongText.Length > MaxLen - ellipsis.Length)
                        {
                            int iLastBlank = sLongText.Substring(0, MaxLen).LastIndexOf(" ");
                            if (iLastBlank > 0)
                                sLongText = sLongText.Substring(0, iLastBlank) + ellipsis;
                            else
                                sLongText = sLongText.Substring(0, MaxLen - ellipsis.Length) + ellipsis;
                        }
                    }
                }
            }
            return sLongText;
        }

        public static bool RegexMatch(string src, string Pattern)
        {
            System.Text.RegularExpressions.Match oMatch = System.Text.RegularExpressions.Regex.Match(src, Pattern);
            bool retval = oMatch.Success;
            return retval;
        }

        public static string RegexValue(string src, string pattern)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern.ToString(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            MatchCollection m1 = Regex.Matches(src, pattern);
            System.Text.RegularExpressions.Match oMatch = r.Match(src);
            string retval = oMatch.Value;
            return retval;
        }

        public static string RegexSuppress(string src, string sRegexPattern)
        {
            string retval = "";
            if (!string.IsNullOrEmpty(src))
            {
                Regex rgx = new Regex(sRegexPattern);
                string sReplacement = "";
                retval = rgx.Replace(src, sReplacement);
            }
            return retval;
        }

        public static string RegexSelectBetween(string sStart, string sEnd)
        {
            string retval = "(?<=" + sStart + ")(.*)(?=" + sEnd + ")";
            return retval;
        }

        public static string LeaveJustNumbericDigits(string src)
        {
            string retval = Regex.Replace(src, "[^0-9]", string.Empty);
            return retval;
        }
        public static string LeaveJustNumericDigits(string src)
        {
            string retval = Regex.Replace(src, "[^0-9]", string.Empty);
            return retval;
        }

        public static string FbImg(string src)
        {
            string retval = "";
            var elements = HtmlElements(src);
            string sFbImgElement = elements.FirstOrDefault(x => x.StartsWith("<img ") & x.Contains(" fbimg "));
            if (string.IsNullOrEmpty(sFbImgElement))
                sFbImgElement = elements.FirstOrDefault(x => x.StartsWith("<img "));
            if (!string.IsNullOrEmpty(sFbImgElement))
                retval = srcFromImgTag(sFbImgElement);
            return retval;
        }

        public static string srcFromImgTag(string imgElement)
        {
            string retval = "";
            var srcPos = imgElement.IndexOf("src");
            if (srcPos >= 0)
            {
                string delimiterVal;
                for (var i = 0; i <= imgElement.Length - 1; i++)
                {
                    switch (imgElement[i])
                    {
                        case (char)34:
                        case (char)39:
                        case (char)44:
                            {
                                delimiterVal = imgElement.Substring(i, 1);
                                var tmp = imgElement.Substring(i + 1);
                                var delimiterPos = tmp.IndexOf(delimiterVal);
                                retval = tmp.Substring(0, delimiterPos);
                                break;
                            }
                    }
                }
            }

            return retval;
        }

        public static List<string> HtmlElements(string src)
        {
            var pattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[\^'"">\s]+))?)+\s*|\s*)/?>";
            // ("<" & tag & "[^>]*id[\s]?=[\s]?['""]" + id & "['""][\s\S]*?</" + tag & ">")
            MatchCollection matches = Regex.Matches(src, pattern);
            List<string> retval = new List<string>();
            foreach (Match m in matches)
            {
                foreach (Capture c in m.Captures)
                    retval.Add(c.Value.ToLower());
            }
            return retval;
        }

        public static List<string> HtmlTagById(string html, string tag, string id)
        {
            var pattern = "<" + tag + @"[^>]*id[\s]?=[\s]?['""]" + id + @"['""][\s\S]*?</" + tag + ">";
            MatchCollection matches = Regex.Matches(html, pattern);
            List<string> retval = new List<string>();
            foreach (Match m in matches)
            {
                foreach (Capture c in m.Captures)
                    retval.Add(c.Value.ToLower());
            }
            return retval;
        }

        public static string InsertStringRepeatedly(string input, string separator, Int32 length)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int chr = 0; chr <= input.Length - 1; chr++)
            {
                if (chr % length == 0 & sb.Length > 0)
                    sb.Append(separator);
                sb.Append(input[chr]);
            }
            return sb.ToString();
        }


        public static string StringListToMultiline(List<string> src)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (src != null)
            {
                foreach (string line in src)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                        sb.AppendLine(line.Trim());
                }
            }
            string retval = sb.ToString();
            return retval;
        }

        public static List<string> StringListFromMultiline(string src)
        {
            List<string> retval = new List<string>();
            var lines = Regex.Split(src, "\r\n|\r|\n");

            foreach (string s in lines)
            {
                if (!string.IsNullOrEmpty(s.Trim()))
                    retval.Add(s.Trim());
            }
            return retval;
        }

        public static List<string> ReadFileToStringList(string sFilename, List<Exception> exs)
        {
            System.IO.FileStream fStream = null;
            System.IO.StreamReader sReader = null;
            List<string> retval = new List<string>();

            try
            {
                fStream = new System.IO.FileStream(sFilename, System.IO.FileMode.Open);
                sReader = new System.IO.StreamReader(fStream);
                while (sReader.Peek() >= 0)
                    retval.Add(sReader.ReadLine());
            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }
            finally
            {
                if (fStream != null)
                    fStream.Close();
                if (sReader != null)
                    sReader.Close();
            }
            return retval;
        }



        public static string CleanForUrl(string src)
        {
            string retval = "";
            if (!string.IsNullOrEmpty(src))
            {
                retval = src.ToLower();
                retval = retval.Replace(" ", "_");
                retval = retval.Replace("ö", "o");
            }
            return retval;
        }


        public static string RemoveAccents(string src)
        {
            // the normalization to FormD splits accented letters in accents+letters
            string sSplitted = src.Normalize(System.Text.NormalizationForm.FormD);

            // removes those accents (and other non-spacing characters)
            string retval = new string(sSplitted.ToCharArray().Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            return retval;
        }

        public static string RemoveDiacritics(string src)
        {
            // the normalization to FormD splits accented letters in accents+letters
            string normalizedString = src.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string retval = sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
            return retval;
        }

        public static string ShortenText(string src, int iMaxLen)
        {
            string retval = src;
            if (src.Length > iMaxLen)
            {
                retval = src.Substring(0, iMaxLen - 3);
                int ilastBlank = retval.LastIndexOf(" ");
                if (ilastBlank > 0)
                    retval = src.Substring(0, ilastBlank);
                retval = retval + "...";
            }
            return retval;
        }



        public static string SelectBetween(string src, string sStart, string sEnd)
        {
            string sRegex = @"\" + sStart + "(.*?)" + sEnd;
            Regex oRegex = new Regex(sRegex);
            Match oMatch = oRegex.Match(src);
            string retval = oMatch.Groups[1].Value.ToString();

            return retval;
        }

        public static string CleanFromHtmlTags(string src, int iMaxLen = 0)
        {
            string retval = src;
            if (!string.IsNullOrEmpty(src))
            {
                string sRegex = RegexSelectBetween("<img", "/>");
                retval = RegexSuppress(src, sRegex);
                sRegex = RegexSelectBetween("<a ", ">");
                retval = RegexSuppress(retval, sRegex);

                sRegex = RegexSelectBetween("<div ", ">");
                retval = RegexSuppress(retval, sRegex);

                retval = retval.Replace("<div >", "");
                retval = retval.Replace("<p>", "");
                retval = retval.Replace("</p>", "");
                retval = retval.Replace("<img/>", "");
                retval = retval.Replace("<b>", "");
                retval = retval.Replace("</b>", "");
                retval = retval.Replace("<strong>", "");
                retval = retval.Replace("</strong>", "");

                retval = retval.Trim();

                if (iMaxLen > 0)
                    retval = ShortenText(retval, iMaxLen);
            }
            return retval;
        }

        public static void NomSplit(string sRaoSocial, ref string sFirstNom, ref string sCognom)
        {
            string sLastChar = TextHelper.VbRight(sCognom, 1);
            int iComa = sRaoSocial.IndexOf(",");
            if (sLastChar.ToLower() == sLastChar & iComa > 0)
            {
                sCognom = sRaoSocial.Substring(0, iComa);
                sFirstNom = sRaoSocial.Substring(iComa + 1).Trim();
            }
            else
                sCognom = sRaoSocial;
        }

        public static string GuessFraNum(string src)
        {
            // retorna la primera paraula (despres de punt o espai) que inclou digits numerics
            string retval = "";
            int iFirstChar = 0;
            int iLastChar = src.Length - 1;
            for (int i = 0; i <= iLastChar; i++)
            {
                string sChar = src.Substring(i, 1);
                switch (sChar)
                {
                    case ".":
                    case " ":
                        {
                            iFirstChar = i + 1;
                            break;
                        }

                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        {
                            if (src.IndexOf(" ", iFirstChar) > 0)
                                iLastChar = src.IndexOf(" ", iFirstChar) - 1;
                            retval = src.Substring(iFirstChar, iLastChar - iFirstChar + 1);
                            break;
                            break;
                        }
                }
            }
            return retval;
        }

        public static string EncodedDate(DateTime DtFch)
        {
            int sWeekDay = (int)DtFch.DayOfWeek;
            int sYear = DtFch.Year - 2000;
            int sDayOfYear = DtFch.DayOfYear;
            string retval = string.Format("{0}{1}{2}", sWeekDay, sYear, sDayOfYear);
            return retval;
        }

        public static DateTime DecodedDate(string src)
        {
            DateTime retval = default(DateTime);
            if (TextHelper.VbIsNumeric(src))
            {
                if (src.Length > 3)
                {
                    int iWeekDay = Convert.ToInt32(src.Substring(0, 1));
                    int iYear = 2000 + System.Convert.ToInt32(src.Substring(1, 2));
                    int iDayOfYear = Convert.ToInt32(src.Substring(3));
                    DateTime DtFch = (new DateTime(iYear, 1, 1).AddDays(iDayOfYear - 1));

                    if ((int)DtFch.DayOfWeek == iWeekDay)
                        retval = DtFch;
                }
            }
            return retval;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            char[] charOutput = new char[length - 1 + 1];
            for (int i = 0; i <= length - 1; i++)
            {
                int selector = random.Next(65, 101);
                if (selector > 90)
                    selector -= 43;
                charOutput[i] = Convert.ToChar(selector);
            }
            string retval = new string(charOutput);
            return retval;
        }


        public static string ToSingleLine(string src, int MaxLength = 0)
        {
            string retval = src.Replace(Constants.vbCrLf, " / ");
            if (MaxLength > 0)
                retval = TextHelper.VbLeft(retval, MaxLength);
            return retval;
        }

        public static string CleanNonAscii(string src)
        {
            string pattern = "[^ -~]+"; // selecciona tots els caracters entre l'espai i la tilde (ASCII 32 - ASCII 126)
            Regex reg_exp = new Regex(pattern);
            string retval = reg_exp.Replace(src, " ");
            return retval;
        }


        public static bool IsValidUrl(string src)
        {
            string pattern = RegexUrl();
            System.Text.RegularExpressions.Match oMatch = System.Text.RegularExpressions.Regex.Match(src, pattern);
            bool retval = oMatch.Success;
            return retval;
        }

        public static string RegexUrl()
        {

            // validates as follows:
            // =========================================================================
            // TODO: Evita validar emails en aquesta expresió
            // =========================================================================
            // www.google.com
            // https://www.google.com
            // mailto: somebody@Google.com 
            // somebody@Google.com
            // www.url-with-querystring.com/?url=has-querystring
            string retval = @"/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/";
            return retval;
        }


        public static string GetSplitCharSeparatedStringFromArrayList(List<string> oArray, string SplitChar = ",")
        {
            string t = "";
            foreach (var s in oArray)
                t = t + SplitChar + s;
            string retval = "";
            if (t.Length > 0)
                retval = t.Substring(1);
            return retval;
        }

        public static List<string> GetArrayListFromSplitCharSeparatedString(string Src, char SplitChar = ',')
        {
            List<string> retval = new List<string>();

            string[] srcs = Src.ToString().Split(SplitChar);
            retval = new List<string>();
            foreach (string s in srcs)
            {
                string sTrimmed = s.Trim();
                if (!string.IsNullOrEmpty(sTrimmed))
                    retval.Add(sTrimmed);
            }
            return retval;
        }
    }
}