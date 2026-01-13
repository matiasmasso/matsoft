using System.Globalization;
using System.Web;
using Components;
using Components.Services;
using DocumentFormat.OpenXml.Office2013.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Components.Services
{

    public static class CultureServiceExtensions
    {

        public static IServiceCollection AddCulture(
            this IServiceCollection services,
            IWebHostEnvironment env,
            UrlHelper.Domains domain)
        {
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<IHttpContextAccessor>();
            return services.AddScoped<CultureService_Deprecated>(x => new CultureService_Deprecated(context, env, domain));
        }
    }
    public class CultureService_Deprecated
    {
        private UrlHelper.Domains _domain;
        private readonly IWebHostEnvironment _env;
        private IHttpContextAccessor _context;

        public CultureService_Deprecated(IHttpContextAccessor context, IWebHostEnvironment env, UrlHelper.Domains domain)
        {
            _domain = domain;
            _env = env;
            _context = context;
        }

        public string RelativeUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            return UrlHelper.RelativeUrl(lang, absolutePath);
        }

        public string CanonicalUrl(LangDTO? lang = null, string? absolutePath = null)
        {
            return UrlHelper.CanonicalUrl(_domain, lang, absolutePath);
        }

        public bool IsPor() => GetLang().IsPor();

        public LangDTO GetLang()
        {
            var retval = LangDTO.Esp();
            if (_context.HttpContext?.Request != null)
            {
                var uri = new Uri(_context.HttpContext?.Request?.GetDisplayUrl() ?? "");
                if (uri.Host.EndsWith(".pt"))
                    retval = LangDTO.Por();
                else
                {
                    var segments = UrlHelper.PathSegments(uri.AbsolutePath);
                    if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First()))
                        retval = LangDTO.FromCultureInfo(segments.First());
                }
            }
            return retval;
        }
        //public LangDTO Lang()
        //{
        //    var retval = LangDTO.Esp();
        //    var uri = new Uri(_context?.HttpContext?.Request?.GetDisplayUrl() ?? "");
        //    if (uri.Host.EndsWith(".pt"))
        //        retval = LangDTO.Por();
        //    else
        //    {
        //        var segments = UrlHelper.PathSegments(uri.AbsolutePath);
        //        if (segments.Count > 0 && LangDTO.LangUrlSegments().Contains(segments.First()))
        //            retval = LangDTO.FromCultureInfo(segments.First());
        //    }
        //    return retval;
        //}

        public string Url(string? absolutePath = null)
        {
            var lang = GetLang();
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

        #region Utilities

        public Uri BaseUri(LangDTO? lang = null) => UrlHelper.BaseUri(_domain, lang);

        #endregion

    }

}
