using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Office2013.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shop4moms.Services
{


    public class CultureService
    {
        //private UrlHelper.Domains _domain = UrlHelper.Domains.matiasmasso;
        private UrlHelper.Domains _domain = UrlHelper.Domains.shop4moms;
        private StringsLocalizerService _localizer;

        private NavigationManager _navigationManager;
        public CultureService(NavigationManager navigationManager, StringsLocalizerService localizer)
        {
            _navigationManager = navigationManager;
            _localizer = localizer;

        }

        public string? RelativeUrl(params string?[] segments)
        {
            string? retval = null;
            var segmentList = new List<string?>() { };
            segmentList.AddRange(segments.Where(x => !string.IsNullOrEmpty(x)).ToList());
            if (segmentList.Count > 0)
            {
                var absolutePath = string.Join("/", segmentList);
                retval = RelativeUrl(Lang(), absolutePath);
            }
            return retval;
        }

        public string RelativeUrl(LangDTO lang, string? absolutePath = null)
        {
            return UrlHelper.RelativeUrl(lang, absolutePath);
        }
        public string RelativeUrl(LangTextModel absolutePath)
        {
            return UrlHelper.RelativeUrl(Lang(), absolutePath.Tradueix(Lang()));
        }



        public string CanonicalUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            return UrlHelper.CanonicalUrl(_domain, lang, absolutePath);
        }

        public bool IsPor() => Lang().IsPor();

        public string Tradueix(string? Esp = null, string? Cat = null, string? Eng = null, string? Por = null) => Lang().Tradueix(Esp, Cat, Eng, Por);

        public LangDTO Lang()
        {
            var retval = LangDTO.Esp();
            var uri = new Uri(_navigationManager.Uri);
            if (uri.Host.EndsWith(".pt"))
                retval = LangDTO.Por();
            else
            {
                var segments = UrlHelper.PathSegments(uri.AbsolutePath);
                if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First()))
                    retval = LangDTO.FromCultureInfo(segments.First());
            }
            return retval;
        }

        public string Url(string? absolutePath = null)
        {
            var lang = Lang();
            var baseUri = BaseUri(lang);
            var uri = new Uri(baseUri, absolutePath);
            return uri.ToString();
        }

        public string PageUrl(params string[] segments)
        {
            var segmentList = new List<string>() { };
            segmentList.AddRange(segments);
            var absolutePath = "/" + string.Join("/", segmentList);
            var retval = Url(absolutePath);
            return retval;
        }

        public LangDTO GetLang(Uri uri)
        {
            var retval = LangDTO.Esp();
            if (uri.Host.EndsWith(".pt"))
                retval = LangDTO.Por();
            else
            {
                var segments = UrlHelper.PathSegments(uri.AbsolutePath);
                if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First()))
                    retval = LangDTO.FromCultureInfo(segments.First());
            }
            return retval;
        }

        public string ResetUrl(LangDTO lang, Uri redirectUri)
        {
            string redirectUrl;
            string uriEscaped;
            var cc = lang.Culture2Digits();
            var cultureEscaped = Uri.EscapeDataString(cc);
            var isLocalHost = redirectUri.Host.Contains("localhost", StringComparison.CurrentCultureIgnoreCase);
            if (isLocalHost)
            {
                var relativePath = UrlHelper.RemoveLangSegment(redirectUri.AbsolutePath);
                if (!lang.IsEsp()) relativePath = $"{cc}/{relativePath}";
                var baseUri = new Uri($"https://{redirectUri.Authority}");
                var uri = new Uri(baseUri, relativePath); //.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                uriEscaped = Uri.EscapeDataString(uri.ToString()); // HttpUtility.UrlEncode(uri.);
            }
            else
            {
                redirectUrl = CanonicalUrl(lang, redirectUri.AbsolutePath);
                var uri = new Uri(redirectUrl); //.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                uriEscaped = Uri.EscapeDataString(uri.ToString());
            }
            var retval = $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}";
            return retval;
        }

        public string Localize(string? stringKey, params object?[] parameters) => _localizer.Localize(Lang(),stringKey, parameters);

        #region Utilities

        public Uri BaseUri(LangDTO? lang = null) => UrlHelper.BaseUri(_domain, lang);

        #endregion

    }

}
