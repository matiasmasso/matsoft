using DocumentFormat.OpenXml.Office2013.ExcelAc;
using DTO.Integracions.Redsys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Shop4moms
{
    public class Common
    {
        public static Tpv Tpv() => Redsys.Tpv.Wellknown(Redsys.Tpv.Ids.Shop4moms)!;

        public static string CanonicalUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            var baseUri = BaseUri(lang);
            var cleanPath = RemoveLangSegment(absolutePath);
            if(lang != null && (lang.IsCat() || lang.IsEng())) cleanPath = string.Format("{0}/{1}", lang.Culture2Digits(), cleanPath);
            var retval = new Uri(baseUri, cleanPath);
            return retval.ToString();
        }

        public static LangDTO GetLang(Uri uri)
        {
            var retval = LangDTO.Esp();
            if (uri.Host.EndsWith(".pt"))
                retval = LangDTO.Por();
            else
            {
                var segments = PathSegments(uri.AbsolutePath);
                if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First()))
                    retval = LangDTO.FromCultureInfo(segments.First());
            }
            return retval;
        }

        #region Utilities

        private static Uri BaseUri(LangDTO? lang = null)
        {
            var retval = "https://www.4moms.es";
            if (lang != null && lang.Id == LangDTO.Ids.POR)
                retval = "https://www.4moms.pt/";
            return new Uri(retval);
        }

        private static string RemoveLangSegment(string? absolutePath)
        {
            var segments = PathSegments(absolutePath);
            if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First())) segments.RemoveAt(0);
            var retval = string.Join('/', segments);
            return retval;
        }

        private static List<string> PathSegments(string? absolutePath)
        {
            var trimmedPath = string.IsNullOrEmpty(absolutePath) ? "" : absolutePath.Trim('/');
            var retval = trimmedPath.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            return retval;
        }

        #endregion
    }
}
