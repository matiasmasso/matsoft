using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Mail;
using Components;
using System;
using DTO.Integracions.Shop4moms.Cache.Shop4moms;
using DTO.Integracions.Shop4moms.Cache;
using DTO.Integracions.Shop4moms;

namespace Test4moms.Services
{

    public class AppState
    {
        public const string RootUrl = "https://4moms.matiasmasso.es";
        public event EventHandler<EventArgs>? CatalogLoaded;

        public Cache? Cache { get; set; }
        public List<Session> Sessions { get; set; } = new();


        public AppState()
        {
            Task task = Task.Run(async () => await LoadCatalog());
        }

        private async Task LoadCatalog()
        {
            using (var http = new HttpClient())
            {
                var apiResult = await HttpService.GetAsync<Cache>(http, "Shop4moms");
                if (apiResult.Success())
                    Cache = apiResult.Value!;
                CatalogLoaded?.Invoke(this, EventArgs.Empty);
            }
        }


        public Session AddSession()
        {
            var retval = new Session { Guid = Guid.NewGuid() };
            Sessions.Add(retval);
            return retval;
        }

        public Session? Session(Guid? sessionId) => Sessions.FirstOrDefault(x => x.Guid == sessionId);

        public ShoppingBasketModel? Basket(string? ticketNumber) => Sessions.Where(x => x.Basket.TicketNumber == ticketNumber).Select(x => x.Basket).FirstOrDefault();

        public void SendMail(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress("info@matiasmasso.es", "M+O info");
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient
            {
                EnableSsl = true,
                Host = "smtp.office365.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("info@matiasmasso.es", "Tan76831") // Enter seders User name and password
            };
            smtp.Send(mail);
        }
    }

    public class Session
    {
        public Guid Guid { get; set; }
        public ShoppingBasketModel Basket { get; set; } = new();
        public DateTime FchCreated { get; set; } = DateTime.Now;
    }
}
