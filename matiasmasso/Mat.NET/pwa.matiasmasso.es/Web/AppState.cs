using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.Threading;
using System.Net.Mail;
using DTO.Integracions.Redsys;
using System.Collections.Generic;

namespace Components
{

    public class AppState: IAppState
    {
        public string RootUrl;

        protected HttpClient http;


        public AppState(HttpClient httpclient)
        {
            RootUrl = "https://pwa.matiasmasso.es";
            http = httpclient;
        }


        #region SessionsManagement
        public IEnumerable<SessionStateDeprecated> Sessions { get; set; }=new List<SessionStateDeprecated>();

        public SessionStateDeprecated? Session(Guid? sessionId) => Sessions.FirstOrDefault(x => x.Guid == sessionId);
        public SessionStateDeprecated AddSession()
        {
            var retval = new SessionStateDeprecated(http);
            ((List<SessionStateDeprecated>)Sessions).Add(retval);
            return retval;
        }
        #endregion


        #region Localizer

        public List<StringLocalizerModel> StringsLocalizer = new();

        #endregion


 
        #region Cache

        public bool IsLoadedCache { get; set; } = false;
        public bool IsLoadingCache { get; set; } = false;
        public AsyncEventHandler? CacheJustLoaded { get; set; }

        private CacheDTO? _cache { get; set; }

        public CacheDTO Cache()//TO DO: try to update upon request
        {
            if(_cache == null) _cache = new CacheDTO(EmpModel.EmpIds.MatiasMasso);
            return _cache;
        }



        #endregion

        public async Task SendMailAsync(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress("info@matiasmasso.es", "M+O info");
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp.office365.com";
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential("info@matiasmasso.es", "Tan76831"); // Enter seders User name and password
                await smtpClient.SendMailAsync(mail);
            }
        }


    }

}
