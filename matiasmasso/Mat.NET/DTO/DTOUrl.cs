using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOUrl
    {
        public DTOLangText MainSegment { get; set; }
        public DTOLangText Path { get; set; }
        public String SufixSegment { get; set; }

        public string FileExtension { get; set; }



        public static DTOUrl Factory(string esp = "", string cat = "", string eng = "", string por = "")
        {
            DTOUrl retval = new DTOUrl();
            retval.MainSegment = new DTOLangText(esp, cat, eng, por);
            return retval;
        }
        public static DTOUrl Factory(params DTOLangText[] langUrlSegments)
        {
            //joins the segments of each language top build a single LangText with all paths
            DTOUrl retval = new DTOUrl();
            try
            {
                string esp = langUrlFromSegments(DTOLang.ESP(), langUrlSegments);
                string cat = langUrlFromSegments(DTOLang.CAT(), langUrlSegments);
                string eng = langUrlFromSegments(DTOLang.ENG(), langUrlSegments);
                string por = langUrlFromSegments(DTOLang.POR(), langUrlSegments);
                retval.Path = new DTOLangText(esp, cat, eng, por);
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
            }
            return retval;
        }

        public JObject ToJObject()
        {
            JObject retval = new JObject();
            JObject mainSegment = new JObject();
            if (MainSegment != null)
            {
                if (!string.IsNullOrEmpty(MainSegment.Cat))
                    mainSegment.Add("Cat", MainSegment.Cat);
                if (!string.IsNullOrEmpty(MainSegment.Eng))
                    mainSegment.Add("Eng", MainSegment.Eng);
                if (!string.IsNullOrEmpty(MainSegment.Por))
                    mainSegment.Add("Por", MainSegment.Por);
                if (!string.IsNullOrEmpty(MainSegment.Esp))
                {
                    mainSegment.Add("Esp", MainSegment.Esp);
                    retval.Add("MainSegment", mainSegment);
                }
            }

            JObject path = new JObject();
            if (Path != null)
            {
                if (!string.IsNullOrEmpty(Path.Cat))
                    path.Add("Cat", Path.Cat);
                if (!string.IsNullOrEmpty(Path.Eng))
                    path.Add("Eng", Path.Eng);
                if (!string.IsNullOrEmpty(Path.Por))
                    path.Add("Por", Path.Por);
                if (!string.IsNullOrEmpty(Path.Esp))
                {
                    path.Add("Esp", Path.Esp);
                    retval.Add("Path", path);
                }
            }

            if (!string.IsNullOrEmpty(SufixSegment))
                retval.Add("SufixSegment", SufixSegment);

            if (!string.IsNullOrEmpty(FileExtension))
                retval.Add("FileExtension", FileExtension);
            return retval;
        }

        public DTOUrl WithSufix(string sufixSegment)
        {
            DTOUrl retval = this;
            retval.SufixSegment = sufixSegment;
            return retval;
        }

        private static string langUrlFromSegments(DTOLang lang, params DTOLangText[] langUrlSegments)
        {
            List<string> segments = langUrlSegments.Where(x => x != null).Select(x => x.Tradueix(lang)).ToList();
            string retval = "/" + string.Join("/", segments);
            return retval;
        }
        public string RelativeUrl(DTOLang lang)
        {
            string retval = "";
            DTOWebDomain domain = DTOWebDomain.Factory(lang, false);
            if (this.MainSegment == null)
                if (this.Path == null)
                    retval = domain.RootUrl();
                else
                    retval = domain.Url(this.Path.Tradueix(lang));
            else
            {
                if (this.Path == null)
                {
                    retval = domain.Url(this.MainSegment.Tradueix(lang));
                }
                else
                {
                    retval = domain.Url(this.MainSegment.Tradueix(lang), this.Path.Tradueix(lang));
                }
            }

            if (this.FileExtension.isNotEmpty())
                retval += this.FileExtension;

            if (this.SufixSegment.isNotEmpty())
            {
                retval += "/" + this.SufixSegment;
            }

            return retval.ToLower();
        }

        public DTOLangText RelativeUrl()
        {
            DTOLangText retval =  new DTOLangText("/", "/", "/", "/");
            if (this.MainSegment != null) retval = DTOLangText.Concatenate(retval, this.MainSegment);
            if (this.Path != null) retval = DTOLangText.Concatenate(retval, this.Path);
            if (this.SufixSegment.isNotEmpty()) retval = DTOLangText.Concatenate(retval, new DTOLangText("/"), new DTOLangText(this.SufixSegment));
            if (this.FileExtension.isNotEmpty()) retval = DTOLangText.Concatenate(retval, new DTOLangText(this.FileExtension));
            return retval;
        }

        public string DomainUrl(DTOLang lang) // for facebook for example, to ensure each domain does not default top English
        {
            string retval = lang.Domain(true).LangUrl(lang, RelativeUrl(lang));
            return retval.ToLower();
        }


        public string AbsoluteUrl(DTOLang lang)
        {
            string retval = "";
            DTOWebDomain domain = DTOWebDomain.Factory(lang);
            if (this.MainSegment == null)
                retval = domain.Url(this.Path.Tradueix(lang));
            else
                if (this.Path == null)
                retval = domain.Url(this.MainSegment.Tradueix(lang));
            else
                retval = domain.Url(this.MainSegment.Tradueix(lang), this.Path.Tradueix(lang));


            if (this.FileExtension.isNotEmpty())
                retval += this.FileExtension;

            if (this.SufixSegment.isNotEmpty())
            {
                retval += "/" + this.SufixSegment;
            }
            return retval.ToLower();
        }

        public string CanonicalUrl(DTOLang lang)
        {
            string retval = "";
            if (lang == null)
                lang = DTOLang.ESP();
            DTOWebDomain domain = DTOWebDomain.Factory(lang);
            //lang = domain.DefaultLang(); // defaults al domini
            if (this.Path == null)
            {
                if (this.MainSegment != null)
                    retval = domain.Url(this.MainSegment.Tradueix(lang));
                //retval = domain.Url(lang.ISO6391(), this.MainSegment.Tradueix(lang));
            }
            else
            {
                if (this.MainSegment == null)
                    //retval = domain.Url(this.Path.Tradueix(lang));
                    retval = domain.Url(lang.ISO6391(), this.Path.Tradueix(lang));
                else
                    //retval = domain.Url(this.MainSegment.Tradueix(lang), this.Path.Tradueix(lang));
                    retval = domain.Url(lang.ISO6391(), this.MainSegment.Tradueix(lang), this.Path.Tradueix(lang));

                if (this.FileExtension.isNotEmpty())
                    retval += this.FileExtension;
            }

            if (this.SufixSegment.isNotEmpty())
            {
                retval += "/" + this.SufixSegment;
            }

            return retval.ToLower();
        }
    }
}
