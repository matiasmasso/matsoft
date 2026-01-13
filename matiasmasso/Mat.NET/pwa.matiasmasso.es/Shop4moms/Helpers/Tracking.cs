
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace Shop4moms.Helpers
{
    public class Tracking
    {

        public async static Task<DTO.Integracions.Vivace.Trace> TraceAsync(DeliveryModel? delivery)
        {
            var retval = new DTO.Integracions.Vivace.Trace();
            if (delivery != null)
            {
                retval.Url = DTO.Integracions.Vivace.Trace.TrackingUrl(delivery);

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(retval.Url);
                var pageContents = await response.Content.ReadAsStringAsync();
                retval.MoreInfoAvailable = pageContents.IndexOf("lbValTracking") > 0;
                var span = HtmlTagById(pageContents, "span", "lbValHistorico").FirstOrDefault();
                if (string.IsNullOrEmpty(span))
                    throw new Exception("missing lbValHistorico id span on Vivace tracking page");
                else
                {
                    var iStartValue = span.IndexOf(">");
                    iStartValue = span.IndexOf(">", iStartValue + 1) + 1;
                    var iEndValue = span.IndexOf("</font>", iStartValue);
                    var spanValue = span.Substring(iStartValue, iEndValue - iStartValue);
                    var textInfo = new CultureInfo("es-ES", false).TextInfo;
                    var properCase = textInfo.ToTitleCase(spanValue);
                    string[] linebreak = { "</br>", "</Br>", "</BR>", "</bR>", "<br/>", "<Br/>", "<BR/>", "<bR/>" };
                    var lines = properCase.Split(linebreak, StringSplitOptions.None);
                    var cleanLines = lines.Where(x => x.Contains("|")).ToList();

                    retval.Items = cleanLines.Where(x => !string.IsNullOrEmpty(x)).Select(x => DTO.Integracions.Vivace.Trace.Item.Factory(x)!).ToList() ?? new();
                }
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

    }
}
