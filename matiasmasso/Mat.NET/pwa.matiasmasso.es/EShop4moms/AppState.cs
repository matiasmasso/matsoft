using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Mail;
using Components;
using System;
using DTO.Integracions.Shop4moms;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Microsoft.VisualStudio.Threading;
using DTO.Integracions.Redsys;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.OutputCaching;

namespace Shop4moms
{
    public class AppState : IAppState
    {
        public string RootUrl = "https://4moms.matiasmasso.es"; //TO DO: canviar a www.4moms.es quan estigui disponible
        private MailAddress mailFrom = new MailAddress("info@matiasmasso.es", "4moms info"); //TO DO: canviar a info@4moms.es quan estigui disponible
        private static DTO.Integracions.Redsys.Common.Environments tpvEnvironment = DTO.Integracions.Redsys.Common.Environments.Production;
        private static Tpv.Ids tpvOwner = Tpv.Ids.Shop4moms;
        protected HttpClient http;



        public AppState(HttpClient httpclient)
        {
            http = httpclient;
        }


        #region SessionsManagement
        public IEnumerable<Components.SessionState> Sessions { get; set; } = new List<SessionState>();


        public SessionState? Session(Guid? sessionId) => (SessionState?)Sessions.FirstOrDefault(x => x.Guid == sessionId);

        public Components.SessionState AddSession()
        {
            var retval = new SessionState(http);
            ((List<SessionState>)Sessions).Add(retval);
            return retval;
        }

        //public ShoppingBasketModel? Basket(string? TpvOrderNum) => Sessions.Select(x => ((SessionState)x).Basket).FirstOrDefault(x => x.TpvOrderNum == TpvOrderNum);
        #endregion


        #region Tpv
        //public Tpv Tpv { get; set; } //= new Tpv(tpvOwner, tpvEnvironment);
        #endregion


        #region Cache
        private DTO.Integracions.Shop4moms.Cache? _cache { get; set; }

        public CacheDTO Cache()//TO DO: try to update upon request
        {
            _cache = _cache ?? new DTO.Integracions.Shop4moms.Cache();
            return _cache;
        }
        //public async Task StartUpTasksAsync()
        //{
        //    IsLoadingCache = true;
        //    await InitiateCacheAsync();
        //    IsLoadingCache = false;
        //    IsLoadedCache = true;
        //}

        private CacheDTO CacheRequest()
        {
            var retval = new CacheDTO(EmpModel.EmpIds.MatiasMasso);
            return retval.Request(CacheDTO.Table.TableIds.StringLocalizer
                    , CacheDTO.Table.TableIds.Tpa
                    , CacheDTO.Table.TableIds.Dept
                    , CacheDTO.Table.TableIds.Stp
                    , CacheDTO.Table.TableIds.Art
                    , CacheDTO.Table.TableIds.PriceListItem_Customer
                    , CacheDTO.Table.TableIds.Arc
                    , CacheDTO.Table.TableIds.Pnc
                    , CacheDTO.Table.TableIds.UrlSegment
                    , CacheDTO.Table.TableIds.Country
                    , CacheDTO.Table.TableIds.Regio
                    , CacheDTO.Table.TableIds.Provincia
                    , CacheDTO.Table.TableIds.Zona
                    , CacheDTO.Table.TableIds.Location
                    , CacheDTO.Table.TableIds.Zip
                    , CacheDTO.Table.TableIds.CliGral
                    );
        }

        //private async Task InitiateCacheAsync()
        //{

        //    var apiResponse = await HttpService.GetAsync<DTO.Integracions.Shop4moms.Cache>(http, "shop4moms");
        //    if (apiResponse.Value != null)
        //    {
        //        _cache = apiResponse.Value; //update cache if needed
        //        this.Tpv = _cache.Tpv!;
        //        StringsLocalizer = _cache.StringsLocalizer;
        //    }
        //    CacheJustLoaded?.DynamicInvoke(this, new DTO.Helpers.MatEventArgs<ProblemDetails>(apiResponse.ProblemDetails));
        //}

        #endregion


        #region Mailing

        public async Task SendMailAsync(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = mailFrom;
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

        #endregion


    }

}
