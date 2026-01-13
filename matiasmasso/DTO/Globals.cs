//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Globals
    {
        public static bool UseLocalApi = false; //=true; //= false; //
        public static EmpModel.EmpIds DefaultEmp = EmpModel.EmpIds.MatiasMasso;
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
            var retval = "/" + string.Join("/", segmentList);
            return retval;
        }
        public static string ApiUrl(bool useLocalApi, params string?[] segments)
        {
            string retval;
            List<string> cleanSegments = new();
            foreach (var segment in segments)
            {
                if (!string.IsNullOrEmpty(segment)) cleanSegments.Add(segment);
            }

            for (var i = 0; i < segments.Length; i++)
            {
                
                if (cleanSegments[i].StartsWith("/"))
                    cleanSegments[i] = cleanSegments[i].Remove(0, 1);
                if (cleanSegments[i].EndsWith("/"))
                    cleanSegments[i] = cleanSegments[i].Remove(cleanSegments[i].Length - 1);
            }
            if (cleanSegments.Count > 0 && cleanSegments.First().StartsWith("http"))
                retval = string.Join("/", cleanSegments);
            else
            {
                var host = useLocalApi ? "localhost:7111" : "api2.matiasmasso.es";
                var segmentList = new List<string>() { host };
                segmentList.AddRange(cleanSegments);
                retval = string.Format("https://{0}", string.Join("/", segmentList));
            }
            return retval;
        }
        public static string ApiUrl(params string[] segments) //To Deprecate
        {
            //UseLocalApi =  true;//
            string retval;
            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i].StartsWith("/"))
                    segments[i] = segments[i].Remove(0, 1);
                if (segments[i].EndsWith("/"))
                    segments[i] = segments[i].Remove(segments[i].Length - 1);
            }
            if (segments.Length > 0 && segments.First().StartsWith("http"))
                retval = string.Join("/", segments);
            else
            {
                var host = UseLocalApi ? "localhost:7111" : "api2.matiasmasso.es";
                var segmentList = new List<string>() { host };
                segmentList.AddRange(segments);
                retval = string.Format("https://{0}", string.Join("/", segmentList));
            }
            return retval;
        }
        public static string RemoteApiUrl(params string[] segments)
        {
            var host = "api2.matiasmasso.es";
            var segmentList = new List<string>() { host };
            segmentList.AddRange(segments);
            var retval = string.Format("https://{0}", string.Join("/", segmentList));
            return retval;
        }

        public static string ExternalApiUrl(params string[] segments) => string.Join("/", segments);

    }
}
