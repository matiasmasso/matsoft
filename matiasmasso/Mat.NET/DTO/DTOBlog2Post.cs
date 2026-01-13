using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBlogPost : DTOContent
    {
        public DTOLangText UrlFriendlySegment { get; set; }

        [JsonIgnore]
        public Byte[] Thumbnail { get; set; }

        public DTOBlogPost() : base()
        {
            base.Src = Srcs.Blog;
            base.Fch = DTO.GlobalVariables.Today();
            base.Visible = false;
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogExcerpt);
            this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogText);
            this.UrlSegment = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogUrl);
        }
        public DTOBlogPost(Guid guid) : base(guid)
        {
            base.Src = Srcs.Blog;
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogExcerpt);
            this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogText);
            this.UrlSegment = new DTOLangText(base.Guid, DTOLangText.Srcs.BlogUrl);
        }

        public static DTOBlogPost Factory(DTOUser oUser)
        {
            DTOBlogPost retval = new DTOBlogPost();
            retval.UsrLog = DTOUsrLog.Factory(oUser);
            return retval;
        }


        public string HtmlText(DTOLang lang)
        {
            string src = this.Text.Tradueix(lang);
            string retval = System.Text.RegularExpressions.Regex.Replace(src, "\r\n?|\n", "<br />");
            return retval;
        }

        public new string ThumbnailUrl()
        {
            string retval = MmoUrl.ApiUrl("BlogPost/thumbnail", this.Guid.ToString());
            return retval;
        }


        public new DTOUrl Url()
        {
            DTOUrl retval = DTOUrl.Factory("blog");
            retval.Path = this.UrlSegment;
            retval.FileExtension = ".html";
            return retval;
        }

        public static DTOLang GetLangFromUrl(string fullUrl)
        {
            DTOLang retval = null;
            if (!string.IsNullOrEmpty(fullUrl))
            {
                fullUrl = fullUrl.Trim('/');
                string[] segments = fullUrl.Split('/');
                //check if second segment is lang
                if (segments.Length > 1)
                {
                    string[] stringArray = { "es", "ca", "en", "pt" };
                    string stringToCheck = segments[1];
                    if (Array.IndexOf(stringArray, stringToCheck) >= 0)
                        retval = DTOLang.FromISO639(stringToCheck);
                }
            }
            return retval;
        }


        public static string GetUrlFriendlySegment(string fullUrl)
        {
            string retval = fullUrl;
            if (!string.IsNullOrEmpty(fullUrl))
            {
                string[] segments = fullUrl.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int offset = Array.IndexOf(segments, "blog");
                if (offset >= 0 && segments.Length > 1)
                {
                    if (segments.Length > offset + 1)
                    {
                        DTOLang lang = DTOLang.FromISO639(segments[offset + 1]);
                        if (lang != null)
                        {
                            offset += 1;
                        }

                    }
                }

                List<string> results = new List<string>();
                for (int i = offset + 1; i < segments.Length; i++)
                {
                    string src = segments[i];
                    results.Add(src);
                }
                if (!string.IsNullOrEmpty(retval))
                    retval = string.Join("/", results.ToArray());

                if (retval.Contains(".html"))
                    retval = retval.Substring(0, retval.IndexOf(".html"));


            }
            return retval;
        }

        public static string BlogHomeUrl(bool absoluteUrl = false)
        {
            return MmoUrl.Factory(absoluteUrl, "blog");
        }

        //public SyndicationItem Rss(DTOWebDomain domain)
        //{
        //    var lang = domain.DefaultLang();
        //    var retval = new SyndicationItem()
        //    {
        //        Id = this.Guid.ToString(),
        //        Title = SyndicationContent.CreatePlaintextContent(this.Title.Tradueix(lang)),
        //        Content = SyndicationContent.CreateHtmlContent(this.Excerpt.Tradueix(lang)),
        //        PublishDate = this.Fch,
        //        LastUpdatedTime = this.UsrLog.FchLastEdited
        //    };
        //    retval.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(this.Url().CanonicalUrl(lang))));
        //    return retval;
        //}


    }
}
