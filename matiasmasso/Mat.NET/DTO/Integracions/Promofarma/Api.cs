using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Integracions.Promofarma
{
    public class Api
    {
        public enum Calls
        {
            GetOrderList,
            GetOrder,
            GetOrderLines,
            PostRequestPickup,
            GetShippingLabel,
            GetShippingWaybill
        }

        public static string Url(params string[] segmentArray)
        {
            string segments = String.Join("/", segmentArray);
            string retval = string.Format("https://apiv2.promofarma.com/{0}", segments);
            return retval;
        }

        public static Dictionary<String, String> AuthHeader()
        {
            var ApiKey = "2abf7fe07f0c136c4a577a0fe064b77b";
            var retval = new Dictionary<String, String>();
            retval.Add("ApiKey", ApiKey);
            return retval;
        }
    }
}
