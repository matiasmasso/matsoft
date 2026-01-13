using DTO;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Components.Services
{
    // Visa 4548812049400004 12/20 123

    public static class TpvServiceExtensions
    {

        /// <summary>
        /// Adds a Payment Gateway from Redsys
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tpvId">Enumeration of terminals available</param>
        /// <returns></returns>
        public static IServiceCollection AddRedsysTpv(
            this IServiceCollection services,
            DTO.Integracions.Redsys.Tpv.Ids tpvId)
        {
            return services.AddScoped<TpvService>(x => new TpvService(tpvId));
        }
    }


    public class TpvService
    {
        public const string managementUrl = "https://sis-t.redsys.es:25443/canales/";
        public DTO.Integracions.Redsys.Tpv.Ids Id { get; set; }
        public DTO.Integracions.Redsys.Common.Environments Environment { get; set; } = DTO.Integracions.Redsys.Common.Environments.Production;
        public TpvService(DTO.Integracions.Redsys.Tpv.Ids tpvId)
        {
            Id =tpvId;
        }

        public DTO.Integracions.Redsys.Tpv Tpv(LangDTO lang)
        {
            return new DTO.Integracions.Redsys.Tpv(this.Id, this.Environment, lang);
        }

        public static string HtmlReport(DTO.Integracions.Redsys.Request? response, DTO.Integracions.Redsys.Tpv tpv, Components.ProblemDetails? problemDetails = null)
        {
            var sb = new StringBuilder();
            if (response != null)
            {
                var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(response.Ds_MerchantParameters)!;
                var computedHash = DTO.Integracions.Redsys.Tpv.CreateMerchantSignature(response.Ds_MerchantParameters, response.TpvOrderNum(), tpv.PrivateKey());
                var normalizedResponseSignature = response.Ds_Signature?.Replace("_", "/");
                sb.AppendLine("<style>");
                sb.AppendLine("table.Rpt {border-collapse: collapse;max-width:600px;}");
                sb.AppendLine("table.Rpt tr:nth-child(even){background-color: #f2f2f2;}");
                sb.AppendLine("table.Rpt tr td {border: 1px solid #ddd; padding: 8px;}");
                sb.AppendLine("table.Rpt tr td:first-child {white-space:nowrap;}");
                sb.AppendLine("</style>");

                sb.AppendLine("<table class='Rpt'>");
                if (problemDetails?.Exception != null)
                {
                    sb.AppendLine("<tr><td>" + "Ex.message" + "</td><td>" + problemDetails?.Exception.Message + "</td></tr>");
                    if (problemDetails?.Exception.InnerException != null)
                    {
                        sb.AppendLine("<tr><td>" + "Ex.InnerException" + "</td><td>" + problemDetails?.Exception.InnerException.Message + "</td></tr>");
                    }
                }
                else if (problemDetails != null)
                {
                    sb.AppendLine("<tr><td>" + "problemDetails title" + "</td><td>" + problemDetails.Title + "</td></tr>");
                    if (!string.IsNullOrEmpty(problemDetails.Details()))
                    {
                        sb.AppendLine("<tr><td>" + "problemDetails details" + "</td><td>" + problemDetails.Details() + "</td></tr>");
                    }
                }
                sb.AppendLine("<tr><td>" + "IsValidSignature" + "</td><td>" + (tpv.IsValidSignature(response) ? "Yes" : "No") + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Encoded parameters" + "</td><td style='ms-word-break:break-word;'>" + response.Ds_MerchantParameters + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Signature Version" + "</td><td>" + response.Ds_SignatureVersion + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Signature" + "</td><td style='ms-word-break:break-word;'>" + normalizedResponseSignature + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Computed hash" + "</td><td style='ms-word-break:break-word;'>" + computedHash + "</td></tr>");
                foreach (var p in paramsDictionary ?? new Dictionary<string, string>())
                {
                    sb.AppendLine("<tr><td>" + p.Key + "</td><td>" + p.Value + "</td></tr>");
                }
                sb.Append("</table>");

            }
            return sb.ToString();
        }



    }
}
