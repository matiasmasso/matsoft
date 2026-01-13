using Microsoft.AspNetCore.Components;
using DTO.Helpers;

namespace Shop4moms.Services
{
    public class SEOHelper
    {
        public static string CanonicalUrl(HttpRequest request)
        {
            var scheme = request.Scheme;
            var host = request.Host.Host;
            var path = request.Path;
            var url = string.Format("{0}://{1}{2}", scheme, host, path);
            var retval = CanonicalUrl(url);
            return retval;
        }

        private static string CanonicalUrl(string url)
        {
            var esCanonicalHost = "https://www.4moms.es";
            var ptCanonicalHost = "https://www.4moms.pt";

            var ccTld = UrlHelper.CountryCodeToplevelDomain(url);
            var canonicalHost = ccTld == ".pt" ? ptCanonicalHost : esCanonicalHost;
            var baseUri = new Uri(canonicalHost);
            var absolutePath = new Uri(url).AbsolutePath;
            var retval = new Uri(baseUri, absolutePath);
            return retval.ToString();
        }


    }
}
