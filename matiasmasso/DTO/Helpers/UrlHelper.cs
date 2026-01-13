using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Helpers
{
    public class UrlHelper
    {
        public enum Domains
        {
            matiasmasso,
            shop4moms
        }

        public static string RelativeUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            var cleanPath = UrlHelper.RemoveLangSegment(absolutePath);
            if (lang != null && (lang.IsCat() || lang.IsEng())) cleanPath = string.Format("{0}/{1}", lang.Culture2Digits(), cleanPath);
            var retval = "/" + cleanPath;
            return retval;
        }

        public static string CanonicalUrl(Domains domain, LangDTO? lang = null, string? absolutePath = null)
        {
            var baseUri = BaseUri(domain, lang);
            var cleanPath = UrlHelper.RemoveLangSegment(absolutePath);
            if (lang != null && (lang.IsCat() || lang.IsEng())) cleanPath = string.Format("{0}/{1}", lang.Culture2Digits(), cleanPath);
            var retval = new Uri(baseUri, cleanPath);
            return retval.ToString();
        }

        public static Uri BaseUri(Domains domain, LangDTO? lang = null)
        {
            string retval = "";

            switch (domain)
            {
                case Domains.matiasmasso:
                    retval = "https://beta.matiasmasso.es";
                    if (lang != null && lang.Id == LangDTO.Ids.POR)
                        retval = "https://beta.matiasmasso.pt/";
                    break;
                case Domains.shop4moms:
                    retval = "https://www.4moms.es";
                    if (lang != null && lang.Id == LangDTO.Ids.POR)
                        retval = "https://www.4moms.pt/";
                    break;
            }
            return new Uri(retval);
        }

        public static bool IsBlazorInternal(Uri uri)
        {
            var path = uri.AbsolutePath;
            var retval = path == "/_blazor";
            return retval;
        }
        public static LangDTO Lang(Uri uri)
        {
            var retval = FirstSegmentLang(uri) ?? DomainLang(uri);
            return retval;
        }

        public static bool HasLangSegment(Uri uri)
        {
            var segments = uri.AbsolutePath.Split('/').ToList();
            var firstSegment = segments.FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var langSegment = firstSegment == null ? null : LangDTO.LangUrlSegments().FirstOrDefault(x => x == firstSegment);
            return langSegment != null;
        }

        public static LangDTO? FirstSegmentLang(Uri uri)
        {
            var segments = uri.AbsolutePath.Split('/').ToList();
            var firstSegment = segments.FirstOrDefault(x=>!string.IsNullOrEmpty(x));
            var langSegment = firstSegment == null ? null : LangDTO.LangUrlSegments().FirstOrDefault(x => x == firstSegment);
            var retval = langSegment == null ? null : LangDTO.FromCultureInfo(langSegment);
            return retval;
        }
        public static LangDTO DomainLang(Uri? uri)
        {
            var retval = LangDTO.Esp();
            if (uri != null)
            {
                var ccTld = CountryCodeToplevelDomain(uri);
                retval = string.IsNullOrEmpty(ccTld) ? LangDTO.Esp() : LangDTO.FromCultureInfo(ccTld);
            }
            return retval;
        }
        //returns ".pt", ".es" or string.empty
        public static string CountryCodeToplevelDomain(Uri uri)
        {
            string retval = "";
            var host = uri.Host;
            var pos = host.LastIndexOf('.');
            if (pos >= 0) retval = host.Substring(pos, host.Length - pos);
            return retval;
        }

        public static string RemoveLangSegment(string? absolutePath)
        {
            var segments = PathSegments(absolutePath);
            if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First())) segments.RemoveAt(0);
            var retval = string.Join('/', segments);
            return retval;
        }

        public static List<string> PathSegments(string? absolutePath)
        {
            var trimmedPath = string.IsNullOrEmpty(absolutePath) ? "" : absolutePath.Trim('/');
            var retval = trimmedPath.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            return retval;
        }

    }
}
