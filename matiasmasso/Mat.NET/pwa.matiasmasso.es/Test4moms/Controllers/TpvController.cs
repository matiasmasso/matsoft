
using Components;
using DTO;
using DTO.Integracions.Redsys;
using DTO.Integracions.Redsys.Redsys;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Test4moms.Services;

namespace Test4moms.Controllers
{


    [Route("[controller]/[action]")]
    public class TpvController : Controller
    {
        string? ds_MerchantParameters;
        string? ds_SignatureVersion;
        string? ds_Signature;
        Redsys.Request? request;
        Components.ProblemDetails? problemDetails;

        private readonly AppState _appstate;
        private readonly HttpClient _http;
        public TpvController(AppState appstate, HttpClient http)
        {
            _appstate = appstate;
            _http = http;
        }

        public async Task<IActionResult>Feed()
        {
            try
            {
                ds_MerchantParameters = Request.Form?["Ds_MerchantParameters"];
                ds_SignatureVersion = Request.Form?["Ds_SignatureVersion"];
                ds_Signature = Request.Form?["Ds_Signature"];
                request = Redsys.Request.Factory(ds_MerchantParameters, ds_SignatureVersion, ds_Signature);

                if (request.IsValidSignature())
                {
                    var basket = _appstate.Basket(request.OrderNum());
                    var consumerTicket = basket?.ConsumerTicket();
                    if (consumerTicket != null)
                    {
                        var apiResponse = await HttpService.PostAsync<ConsumerTicketModel, bool>(_http, consumerTicket, "consumerTicket");
                        if (apiResponse.Success())
                        {
                            await NotifyCTO();
                            await NotifyConsumer();
                        }
                        else
                            await WarnUpdateErr();
                    }
                    else
                        await WarnNullConsumerTicket();
                }
                else
                    await WarnInvalidSignature();
            }
            catch (Exception ex)
            {
                await WarnSysErr(ex);
            }
            return Ok(true);
        }

        async Task CheckEmail()
        {
            var body = "test";
            var subject = "4moms Tpv feed hit";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }

        async Task NotifyCTO()
        {
            var body = request?.HtmlReport() ?? "no request";
            var subject = (request?.IsValidSignature() ?? false) ? "4moms TPV NotifyCTO: Tpv success" : "4moms TPV NotifyCTO: Tpv error";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);
        }

        async Task NotifyConsumer()
        {
            //TODO: confirm email to consumer
            var body = request?.HtmlReport() ?? "no request";
            var subject = "4moms TPV consumer notification: Order confirmation";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }

        string ParamsTable(Exception? ex = null)
        {
            var tdstyle = " style='ms-word-break:break-word;'";
            var sb = new StringBuilder();
            sb.AppendLine("<table>");
            if(ex!= null)
            {
                sb.AppendFormat("<tr><td>Exception</td><td></td {0}>{1}</tr>", tdstyle, ex.Message);
                if (ex.InnerException != null)
                    sb.AppendFormat("<tr><td>InnerException</td><td></td{0}>{1}</tr>", tdstyle, ex.InnerException?.Message ?? "");
            }
            sb.AppendFormat("<tr><td>ds_SignatureVersion</td><td></td{0}>{1}</tr>", tdstyle, ds_SignatureVersion ?? "");
            sb.AppendFormat("<tr><td>ds_Signature</td><td></td{0}>{1}</tr>", tdstyle, ds_Signature ?? "");
            sb.AppendFormat("<tr><td>ds_MerchantParameters</td><td></td{0}>{1}</tr>", tdstyle, ds_MerchantParameters ?? "");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        async Task WarnSysErr(Exception ex)
        {
            var body = ParamsTable(ex) + request?.HtmlReport() ?? "no request";
            var subject = "4moms Tpv error SysErr";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }

        async Task WarnUpdateErr()
        {
            var body = request?.HtmlReport() ?? "no request";
            var subject = "4moms Tpv error WarnUpdateErr";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }
        async Task WarnNullConsumerTicket()
        {
            var body = request?.HtmlReport() ?? "no request";
            var subject = "4moms Tpv error WarnNullConsumerTicket";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }
        async Task WarnInvalidSignature()
        {
            var body = request?.HtmlReport() ?? "no request";
            var subject = "4moms Tpv error WarnInvalidSignature";
            var msgTo = "matias@matiasmasso.es";

            await Services.MailService.SendEmail(msgTo, subject, body);

        }
    }
}
