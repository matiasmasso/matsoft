using MatHelperStd;
using System;
using System.Collections.Generic;
//using System.Web.Mvc;

namespace DTO
{
    public class DTOContent : DTOBaseGuid
    {

        public class Compact
        {
            public Guid Guid { get; set; }
            public DateTime Fch { get; set; }
            public DTOLangText Title { get; set; }
            //[AllowHtml]
            public DTOLangText UrlSegment { get; set; }
            public Srcs Src { get; set; }

            public Compact(Guid guid) : base()
            {
                this.Guid = guid;
                this.Title = new DTOLangText(this.Guid, DTOLangText.Srcs.ContentTitle);
                this.UrlSegment = new DTOLangText(this.Guid, DTOLangText.Srcs.ContentUrl);
            }
        }

        public const int THUMBNAIL_WIDTH = 265;
        public const int THUMBNAIL_HEIGHT = 150;

        public DateTime Fch { get; set; }
        public DTOLangText Title { get; set; }
        //[AllowHtml]
        public DTOLangText Excerpt { get; set; }
        //[AllowHtml]
        public DTOLangText Text { get; set; }
        public DTOLangText UrlSegment { get; set; }


        public string ThumbnailUrl { get; set; }

        public bool Visible { get; set; }
        public int VisitCount { get; set; }
        public int CommentCount { get; set; }
        public Srcs Src { get; set; }

        public DTOUsrLog UsrLog { get; set; }
        public DTOFeedback.Model Feedback { get; set; }

        public enum Srcs
        {
            News,
            Eventos,
            SabiasQue,
            Promos,
            TablonDeAnuncios,
            Blog,
            Content,
            Shop4moms
        }



        public enum Wellknowns
        {
            aboutUs,
            avisoLegal,
            cookies,
            privacidad,
            consultasBlog,
            BlogAboutUs,
            BlogNormasDeUso
        }

        public DTOContent() : base()
        {
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentExcerpt);
            this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentText);
            this.UrlSegment = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentUrl);
            this.UsrLog = DTOUsrLog.Factory();
        }
        public DTOContent(Guid guid) : base(guid)
        {
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentExcerpt);
            this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentText);
            this.UrlSegment = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentUrl);
            this.UsrLog = DTOUsrLog.Factory();
        }

        public static DTOContent Wellknown(Wellknowns id)
        {
            DTOContent retval = null;
            switch (id)
            {
                case Wellknowns.aboutUs:
                    retval = new DTOContent(new Guid("6116E07B-4878-4601-8BCE-F0DFEB811BD7"));
                    break;
                case Wellknowns.avisoLegal:
                    retval = new DTOContent(new Guid("233F2C9A-AC95-4C12-A87E-A0E8393575ED"));
                    break;
                case Wellknowns.privacidad:
                    retval = new DTOContent(new Guid("931af776-2952-4cf8-a3eb-e6f2995aeef5"));
                    break;
                case Wellknowns.cookies:
                    retval = new DTOContent(new Guid("cf529bb9-00ab-4452-b3b3-d54b38391f20"));
                    break;
                case Wellknowns.consultasBlog:
                    retval = new DTOContent(new Guid("9B193190-C600-4C35-BCF8-593E5FEF570C"));
                    break;
                case Wellknowns.BlogAboutUs:
                    retval = new DTOContent(new Guid("d5468572-b202-4a04-ad6f-8fb62e52c3be"));
                    break;
                case Wellknowns.BlogNormasDeUso:
                    retval = new DTOContent(new Guid("650B6C61-C116-4BAF-BB52-E5A390AC4ED6"));
                    break;
            }
            return retval;
        }

        public DTOUrl Url()
        {
            DTOUrl retval = DTOUrl.Factory("content");
            retval.Path = this.UrlSegment;
            retval.FileExtension = ".html";
            return retval;
        }

        public string Html(DTOLang lang)
        {
            //string  retval = System.Text.RegularExpressions.Regex.Replace(this.Text.Tradueix(lang), "\r\n?|\n", "<br />");
            string retval = TextHelper.Html(this.Text.Tradueix(lang));
            return retval;
        }

        public DTOContent toSpecificObject()
        {
            DTOContent retval = this;
            if (this is DTONoticia)
                retval = this;
            else if (this is DTOEvento)
                retval = this;
            else if (this is DTOBlogPost)
                retval = this;
            else
                switch (this.Src)
                {
                    case Srcs.News:
                        {
                            return new DTONoticia(base.Guid);
                        }

                    case Srcs.Eventos:
                        {
                            return new DTOEvento(base.Guid);
                        }
                    case Srcs.Blog:
                        {
                            return new DTOEvento(base.Guid);
                        }
                }
            return retval;
        }

    }
}
