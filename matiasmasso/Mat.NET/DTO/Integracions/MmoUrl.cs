using MatHelperStd;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class MmoUrl
    {
        //public const string URLAPI = "https://matiasmasso-api.azurewebsites.net/api";
        public const string URLAPI = "https://api.matiasmasso.es/api";
        public static bool useLocalApi { get; set; }

        public static string ApiUrl(params string[] UrlSegments)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (useLocalApi)
                sb.Append("http://localhost:55836/api");
            else
                sb.Append(URLAPI);

            for (int i = 0; i <= UrlSegments.Length - 1; i++)
            {
                string sSegment = UrlSegments[i].Trim();
                if (!sb.ToString().EndsWith("/"))
                    sb.Append("/");
                if (sSegment.StartsWith("/"))
                    sSegment = sSegment.Substring(1);
                sb.Append(sSegment);
            }

            string retval = sb.ToString();
            return retval;
        }



        public static string image(Defaults.ImgTypes oType, Guid oGuid, bool AbsoluteUrl = false)
        {
            string retval = Factory(AbsoluteUrl, "img", ((int)oType).ToString(), oGuid.ToString());
            return retval;
        }


        public static string Dox(bool AbsoluteUrl, DTODocFile.Cods doxcod, params string[] oParams)
        {
            Dictionary<string, string> oParamsDictionary = new Dictionary<string, string>();
            oParamsDictionary.Add("dox", doxcod.ToString());
            for (int i = 0; i <= oParams.Length - 2; i += 2)
                oParamsDictionary.Add(oParams[i], oParams[i + 1]);

            string sUrlFriendlyBase64Json = CryptoHelper.UrlFriendlyBase64Json(oParamsDictionary);
            string retval = Factory(AbsoluteUrl, "dox", sUrlFriendlyBase64Json);
            return retval;
        }

        public static string Factory(DTODocFile.Cods oCod, Dictionary<string, string> oParameters, bool AbsoluteUrl = false)
        {
            string sBase64Json = CryptoHelper.UrlFriendlyBase64Json(oParameters);
            string retval = Factory(AbsoluteUrl, "doc", ((int)oCod).ToString(), sBase64Json);
            return retval;
        }

        public static string Factory(bool absoluteUrl, params string[] UrlSegments)
        {
            DTOWebDomain domain = DTOWebDomain.Factory(null, absoluteUrl);
            string retval = domain.Url(UrlSegments);
            return retval;
        }

        public static void addQueryStringParam(ref string url, string sParam, string sValue)
        {
            bool BlFirstParam = url.IndexOf("?") == -1;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(url);
            sb.Append(BlFirstParam ? "?" : "&");
            sb.Append(sParam);
            sb.Append("=");
            sb.Append(sValue);
            url = sb.ToString();
        }

        public static string BodyTemplateUrl(DTODefault.MailingTemplates oTemplate, params string[] UrlSegments)
        {
            List<string> oSegments = new List<string>();
            oSegments.Add("mail");
            oSegments.Add(oTemplate.ToString());
            foreach (var s in UrlSegments)
                oSegments.Add(s);
            string retval = Factory(true, oSegments.ToArray());
            return retval;
        }
    }
}
