using System.Globalization;
using Components;
using Components.Services;
using DTO;
using Microsoft.Extensions.DependencyInjection;

namespace Components.Services
{

    public class CultureService_OldWeb
    {

        public CultureService_OldWeb()
        {
            //this.httpContextAccessor = httpContextAccessor;
        }


        public string CanonicalUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            var baseUri = BaseUri(lang);
            var cleanPath = RemoveLangSegment(absolutePath);
            if (lang != null && (lang.IsCat() || lang.IsEng())) cleanPath = string.Format("{0}/{1}", lang.Culture2Digits(), cleanPath);
            var retval = new Uri(baseUri, cleanPath);
            return retval.ToString();
        }

        public LangDTO GetLang(Uri uri)
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

        public string ResetUrl(LangDTO lang, string redirectPath)
        {
            var cc = lang.Culture2Digits();
            var redirectUrl = CanonicalUrl(lang, redirectPath);
            var uri = new Uri(redirectUrl).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            var cultureEscaped = Uri.EscapeDataString(cc);
            var uriEscaped = Uri.EscapeDataString(uri);
            var retval = $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}";
            return retval;
        }

        #region Utilities

        private Uri BaseUri(LangDTO? lang = null)
        {
            var retval = "https://www.4moms.es";
            if (lang != null && lang.Id == LangDTO.Ids.POR)
                retval = "https://www.4moms.pt/";
            return new Uri(retval);
        }

        private string RemoveLangSegment(string? absolutePath)
        {
            var segments = PathSegments(absolutePath);
            if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First())) segments.RemoveAt(0);
            var retval = string.Join('/', segments);
            return retval;
        }

        private List<string> PathSegments(string? absolutePath)
        {
            var trimmedPath = string.IsNullOrEmpty(absolutePath) ? "" : absolutePath.Trim('/');
            var retval = trimmedPath.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            return retval;
        }

        #endregion

    }

}
