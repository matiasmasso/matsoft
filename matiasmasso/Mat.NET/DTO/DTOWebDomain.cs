using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOWebDomain
    {
        public const string URL_RELATIVE = "/";
        public const string URL_ES = "https://www.matiasmasso.es/";
        public const string URL_PT = "https://www.matiasmasso.pt/";

        public Ids Id;
        public enum Ids
        {
            relative,
            matiasmasso_es,
            matiasmasso_pt,
        }

        public DTOWebDomain() : base()
        {
            this.Id = Ids.relative;
        }
        public DTOWebDomain(Ids id) : base()
        {
            this.Id = id;
        }

        public static DTOWebDomain Factory(string tag)
        {
            DTOWebDomain retval = null;
            switch (tag.ToLower())
            {
                case "por":
                case "pt":
                case "pt_pt":
                case "pt_br":
                    retval = new DTOWebDomain(Ids.matiasmasso_pt);
                    break;
                default:
                    retval = new DTOWebDomain(Ids.matiasmasso_es);
                    break;
            }
            return retval;
        }
        public static DTOWebDomain Factory(DTOLang lang = null, bool absoluteUrl = true)
        {
            DTOWebDomain retval = null;
            if (lang == null) lang = DTOLang.Default();
            if (absoluteUrl)
            {
                if (lang.Tag == DTOLang.POR().Tag)
                    retval = new DTOWebDomain(Ids.matiasmasso_pt);
                else
                    retval = new DTOWebDomain(Ids.matiasmasso_es);
            }
            else
                retval = new DTOWebDomain();
            return retval;
        }

        public static DTOWebDomain Default(bool absoluteUrl = false)
        {
            if (absoluteUrl)
                return new DTOWebDomain(Ids.matiasmasso_es);
            else
                return new DTOWebDomain(Ids.relative);
        }

        public DTOLang DefaultLang()
        {
            DTOLang retval = null;
            switch (this.Id)
            {
                case Ids.matiasmasso_pt:
                    retval = DTOLang.POR();
                    break;
                default:
                    retval = DTOLang.ESP();
                    break;
            }
            return retval;
        }

        public string TopLevelTag()
        {
            string retval = "";
            switch (this.Id)
            {
                case Ids.matiasmasso_es:
                    retval = "es";
                    break;
                case Ids.matiasmasso_pt:
                    retval = "pt";
                    break;
            }
            return retval;
        }
        public string RootUrl()
        {
            string retval = "";
            switch (this.Id)
            {
                case Ids.relative:
                    retval = URL_RELATIVE;
                    break;
                case Ids.matiasmasso_es:
                    retval = URL_ES;
                    break;
                case Ids.matiasmasso_pt:
                    retval = URL_PT;
                    break;
            }
            return retval;
        }

        public string LangUrl(DTOLang lang, params string[] urlSegments)
        {
            List<string> segments = urlSegments.ToList();
            string ISO6391 = lang.ISO6391();
            segments.Insert(0, ISO6391);
            return Url(segments.ToArray());
        }

        public string Url(params string[] UrlSegments)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string retval = "";
            if (UrlSegments[0].StartsWith("http") && UrlSegments.Length == 1)
                retval = UrlSegments[0];
            else
            {
                sb.Append(RootUrl());

                for (int intLoopIndex = 0; intLoopIndex <= UrlSegments.Length - 1; intLoopIndex++)
                {
                    string sSegment = UrlSegments[intLoopIndex].Trim();
                    if (!sb.ToString().EndsWith("/"))
                        sb.Append("/");
                    if (sSegment.StartsWith("/"))
                        sSegment = sSegment.Substring(1);
                    sb.Append(sSegment);
                }

                retval = sb.ToString();
            }
            return retval;
        }

        public string ImageUrl(DTO.Defaults.ImgTypes imgType, Guid guid)
        {
            string retval = this.Url("img", ((int)imgType).ToString(), guid.ToString());
            return retval;
        }



    }
}
