//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Globals
    {
        public static bool UseLocalApi = false;
        public static string? Token;
        public static List<string> History = new();
        public enum LoadStatusEnum
        {
            Empty,
            Loading,
            Loaded,
            Failed
        }
        public enum Pages
        {
            None,
            Banners,
            Banner,
            Contacts,
            Contact,
            Contracts,
            Contract,
            Credencials,
            Credencial,
            Deliveries,
            Delivery,
            Escripturas,
            Escriptura,
            Immobles,
            Immoble,
            PurchaseOrders,
            PurchaseOrder,
            Raffles,
            Raffle,
            Vehicles,
            Vehicle
        }
        public static string PageUrl(params string[] segments)
        {
            var segmentList = new List<string>() { };
            segmentList.AddRange(segments);
            var retval = "/" + string.Join("/", segmentList.Where(x=>x!=null));
            return retval;
        }
        public static string ApiUrl(params string[] segments)
        {
            //UseLocalApi = true;//
            string retval = "";
            if (segments.Length > 0 && segments.First().StartsWith("http"))
            {
                retval = string.Join("/", segments.Where(x => x != null));
            }
            else
            {
                var host = UseLocalApi ? "localhost:7286" : "genapi.matiasmasso.es";
                var segmentList = new List<string>() { host };
                segmentList.AddRange(segments);
                retval = string.Format("https://{0}", string.Join("/", segmentList.Where(x => x != null)));
            }
            return retval;
        }
        public static string RemoteApiUrl(params string[] segments)
        {
            var host = "genapi.matiasmasso.es";
            var segmentList = new List<string>() { host };
            segmentList.AddRange(segments);
            var retval = string.Format("https://{0}", string.Join("/", segmentList.Where(x => x != null)));
            return retval;
        }

        public static string ExternalApiUrl(params string[] segments) => string.Join("/", segments.Where(x => x != null));

    }
}
