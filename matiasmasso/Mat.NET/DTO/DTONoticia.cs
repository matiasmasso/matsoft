using System;

namespace DTO
{
    public class DTONoticia : DTONoticiaBase
    {
        public DTOProduct product { get; set; }
        public PriorityLevels priority { get; set; }
        public DTOPostComment.TreeModel Comments { get; set; }

        public const int IMAGEWIDTH = 325;
        public const int IMAGEHEIGHT = 205;

        public DTONoticia() : base(Srcs.News)
        {
        }

        public DTONoticia(Guid oGuid) : base(oGuid)
        {
        }

        public enum PriorityLevels
        {
            Low,
            High
        }

        public new enum Wellknowns
        {
            NotSet,
            iMat2
        }

        public static DTONoticia Factory(DTOUser oUser)
        {
            DTONoticia retval = new DTONoticia();
            retval.UsrLog = DTOUsrLog.Factory(oUser);
            return retval;
        }

        public static DTONoticia Wellknown(DTONoticia.Wellknowns id)
        {
            DTONoticia retval = null;
            switch (id)
            {
                case DTONoticia.Wellknowns.iMat2:
                    {
                        retval = new DTONoticia(new Guid("12C5299A-573C-4A90-860F-3CDA98A713AD"));
                        break;
                    }
            }
            return retval;
        }

        public new DTOUrl Url()
        {
            DTOUrl retval = DTOUrl.Factory("news");
            retval.Path = new DTOLangText(this.urlFriendlySegment);
            retval.FileExtension = ".html";
            return retval;
        }

        public static DTOLangText ExcerptOrCroppedText(DTONoticia oNoticia)
        {
            DTOLangText retval = new DTOLangText();
            if (oNoticia.Excerpt != null)
            {
                if (!oNoticia.Excerpt.isEmpty())
                    retval = oNoticia.Excerpt;
            }

            if (retval.isEmpty() && !oNoticia.Text.isEmpty())
            {
                string sText = oNoticia.Text.Esp;
                if (sText.isNotEmpty())
                {
                    int iMore = sText.IndexOf("<more/>");
                    if (iMore < 0)
                        iMore = 255;
                    retval.Esp = sText.Substring(0, iMore);

                    sText = oNoticia.Text.Cat;
                    if (sText.Length > 0)
                    {
                        iMore = sText.IndexOf("<more/>");
                        if (iMore < 0)
                            iMore = 255;
                        retval.Cat = sText.Substring(0, iMore);
                    }

                    sText = oNoticia.Text.Eng;
                    if (sText.Length > 0)
                    {
                        iMore = sText.IndexOf("<more/>");
                        if (iMore < 0)
                            iMore = 255;
                        retval.Eng = sText.Substring(0, iMore);
                    }

                    sText = oNoticia.Text.Por;
                    if (sText.Length > 0)
                    {
                        iMore = sText.IndexOf("<more/>");
                        if (iMore < 0)
                            iMore = 255;
                        retval.Por = sText.Substring(0, iMore);
                    }
                }
            }

            return retval;
        }

        public static DTOLangText Filtered(DTONoticia oNoticia, DTOUser oUser)
        {
            DTOLangText retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oUser == null)
                retval = DTOLangText.Replace(oNoticia.Text, "@UserGuid", "");
            else
                retval = DTOLangText.Replace(oNoticia.Text, "@UserGuid", oUser.Guid.ToString());
            return retval;
        }

        public static string urlAllNoticias(DTOWebDomain domain = null)
        {
            if (domain == null) domain = DTOWebDomain.Default();
            return domain.Url("noticias");
        }

        public bool isVisibleOnNewsSitemap(DTOWebDomain domain)
        {
            bool retval = false;
            int lastValidDaysCount = 2;
            bool dateIsInRange = this.UsrLog.FchLastEdited >= DTO.GlobalVariables.Today().AddDays(-lastValidDaysCount);
            retval = (dateIsInRange && this.Visible && !this.professional && this.Title.HasContent(domain));
            return retval;
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
