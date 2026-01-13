using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSiteMap
    {
        public Types Type { get; set; }
        public List<string> Urls { get; set; } // per SitemapIndex
        public List<DTOSiteMapItem> Items { get; set; } // per no SitemapIndex
        public DTOWebDomain Domain { get; set; }

        public Modes Mode { get; set; }

        public enum Modes
        {
            Index,
            Web,
            News
        }

        public enum ChangeFreqs
        {
            NotSet,
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
        }

        public enum Types
        {
            NotSet,
            Index,
            Product_Brands,
            Product_Depts,
            Product_Categories,
            Product_Skus,
            Product_Accessories,
            Product_Descargas,
            Product_Downloads,
            Product_Videos,
            Distributors,
            Noticias,
            NoticiasxCategoria,
            Pages,
            BlogPosts,
            LastNews
        }

        public DTOSiteMap(Types oType, Modes oMode = Modes.Web) : base()
        {
            Type = oType;
            Mode = oMode;
            Items = new List<DTOSiteMapItem>();
            Urls = new List<string>();
        }

        public DTOSiteMapItem AddItem(string sLoc, DateTimeOffset DtLastmod, DTOSiteMap.ChangeFreqs oChangeFreq, decimal DcPriority)
        {
            DTOSiteMapItem retval = new DTOSiteMapItem();
            {
                var withBlock = retval;
                withBlock.Loc = sLoc;
                withBlock.Lastmod = DtLastmod;
                withBlock.Changefreq = oChangeFreq;
                withBlock.Priority = DcPriority;
            }
            Items.Add(retval);
            return retval;
        }

        public void AddItems(DTONoticia oNoticia, DTOWebDomain oDomain)
        {
            foreach (var oLang in oNoticia.Title.Langs())
            {
                var item = new DTOSiteMapItem();
                {
                    var withBlock = item;
                    withBlock.Loc = oNoticia.Url().CanonicalUrl(oLang);
                    withBlock.PublicationLang = oLang.ISO6391();
                    withBlock.PublicationName = oLang.Tradueix("Ultimas Noticias M+O", "Noticies fresques M+O", "M+O Last News");
                    withBlock.Title = oNoticia.Title.Tradueix(oLang);
                    withBlock.Lastmod = oNoticia.UsrLog.FchLastEdited;
                }
                Items.Add(item);
            }
        }

        public static string Url(DTOWebDomain oDomain)
        {
            var retval = oDomain.Url("sitemaps/index.xml");
            return retval;
        }
    }

    public class DTOSiteMapItem
    {
        public string Loc { get; set; }
        public DateTimeOffset Lastmod { get; set; }
        public DTOSiteMap.ChangeFreqs Changefreq { get; set; }
        public decimal Priority { get; set; }
        public List<DTOSitemapItemImage> Images { get; set; }
        public List<LangRef> LangRefs { get; set; }
        public string PublicationName { get; set; }
        public string PublicationLang { get; set; }
        public string Title { get; set; }

        public DTOSiteMapItem() : base()
        {
            Images = new List<DTOSitemapItemImage>();
            LangRefs = new List<LangRef>();
        }

        public DTOSitemapItemImage AddImage(string sLoc, string sTitle, string sCaption = "", DTOSitemapItemImage.Licenses oLicense = DTOSitemapItemImage.Licenses.None)
        {
            DTOSitemapItemImage retval = new DTOSitemapItemImage();
            {
                var withBlock = retval;
                withBlock.loc = sLoc;
                withBlock.title = sTitle;
                withBlock.caption = sCaption;
                withBlock.license = oLicense;
            }

            Images.Add(retval);
            return retval;
        }

        public LangRef AddLangRef(DTOLang oLang, string url)
        {
            LangRef retval = new LangRef();
            {
                var withBlock = retval;
                withBlock.Lang = oLang;
                withBlock.Url = url;
            }
            LangRefs.Add(retval);
            return retval;
        }

        public LangRef AddDefaultRef(string url)
        {
            LangRef retval = new LangRef();
            {
                var withBlock = retval;
                withBlock.Url = url;
            }
            LangRefs.Add(retval);
            return retval;
        }

        public class LangRef
        {
            public DTOLang Lang { get; set; }
            public string Url { get; set; }
        }
    }

    public class DTOSitemapItemImage
    {
        public string loc { get; set; }
        public string title { get; set; }
        public string caption { get; set; }
        public Licenses license { get; set; }

        public enum Licenses
        {
            None,
            CCByNd
        }
    }
}
