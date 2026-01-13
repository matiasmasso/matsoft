using Components;
using System.Text;

namespace Shop4moms.Services
{
    public class RobotsService
    {
        public static string GetFile(HttpContext context)
        {
            var host = context.Request.Host.Host;

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("# robots.txt for {0}",host));
            sb.AppendLine("");
            sb.AppendLine(string.Format("Sitemap: https://{0}/sitemap.xml", host));
            sb.AppendLine("");
            sb.AppendLine("# disallow all files ending with these extensions");
            sb.AppendLine("User-agent: *");
            sb.AppendLine("Disallow: /*.pdf$");
            sb.AppendLine("Disallow: /*.js$");
            sb.AppendLine("Disallow: /*.css$");
            return sb.ToString();
        }
    }
}
