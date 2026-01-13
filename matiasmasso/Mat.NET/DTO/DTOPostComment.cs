using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{

    public class DTOPostComment : DTOBaseGuid
    {
        public Guid Parent { get; set; }
        public ParentSources ParentSource { get; set; }
        public DTOUser User { get; set; }
        public DTOLang Lang { get; set; }
        public DateTime Fch { get; set; }
        public string Text { get; set; }
        public DateTime FchApproved { get; set; }
        public DateTime FchDeleted { get; set; }
        public DTOPostComment Answering { get; set; }
        public DTOPostComment AnswerRoot { get; set; }
        public List<DTOPostComment> Answers { get; set; } = new List<DTOPostComment>();

        private DTOLangText _ParentTitle;

        public int CommentsCount { get; set; }
        public int Idx { get; set; }


        public ErrorCodes ErrorCode { get; set; }

        public enum StatusEnum
        {
            NotSet,
            Pendent,
            Aprobat,
            Eliminat
        }

        public enum ParentSources
        {
            NotSet,
            Noticia,
            News,
            Blog,
            Shop4moms
        }

        public enum ErrorCodes
        {
            None,
            NotLogged
        }

        public DTOPostComment() : base()
        {
        }

        public DTOPostComment(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOPostComment NewFromParent(Guid oParent)
        {
            DTOPostComment retval = new DTOPostComment();
            retval.Parent = oParent;
            return retval;
        }

        public static DTOPostComment NewFromParentSource(Guid oGuid)
        {
            DTOPostComment retval = new DTOPostComment();
            retval.Parent = oGuid;
            retval.Fch = DTO.GlobalVariables.Now();
            return retval;
        }

        public static DTOPostComment FromQuestion(DTOPostComment oAnsweringComment, string sText)
        {
            DTOPostComment retval = new DTOPostComment();
            {
                var withBlock = retval;
                withBlock.Answering = oAnsweringComment;
                withBlock.Parent = oAnsweringComment.Parent;
                withBlock.ParentSource = oAnsweringComment.ParentSource;
                withBlock.User = DTOUser.Wellknown(DTOUser.Wellknowns.matias);
                withBlock.FchApproved = DTO.GlobalVariables.Now();
                withBlock.Fch = DTO.GlobalVariables.Now();
                withBlock.Text = sText;
            }
            return retval;
        }

        public string Url(DTOLang lang, bool absoluteUrl = false)
        {
            DTOWebDomain domain = DTOWebDomain.Factory(lang, absoluteUrl);
            string retval = domain.Url("PostComment", base.Guid.ToString());
            return retval;
        }

        public StatusEnum Status
        {
            get
            {
                StatusEnum retval = StatusEnum.NotSet;
                if (FchDeleted == default(DateTime))
                {
                    if (FchApproved == default(DateTime))
                        retval = StatusEnum.Pendent;
                    else
                        retval = StatusEnum.Aprobat;
                }
                else
                    retval = StatusEnum.Eliminat;
                return retval;
            }
        }

        public string NickName
        {
            get
            {
                string retval = "";
                if (User != null)
                    retval = User.NickName;
                return retval;
            }
        }


        public DTOLangText ParentTitle
        {
            get
            {
                if (_ParentTitle == null)
                {
                    DTONoticia oNoticia = new DTONoticia(Parent);
                    _ParentTitle = oNoticia.Title;
                }
                return _ParentTitle;
            }
            set
            {
                _ParentTitle = value;
            }
        }


        public string HtmlText()
        {
            return HtmlText(this);
        }
        public static string HtmlText(DTOPostComment oComment)
        {
            string src = oComment.Text;
            string retval = System.Text.RegularExpressions.Regex.Replace(src, @"\r\n?|\n", "<br />"); // src.Replace(Constants.vbCrLf, "<br/>");
            return retval;
        }

        public static DTONoticia Noticia(DTOPostComment oPostComment)
        {
            DTONoticia retval = new DTONoticia(oPostComment.Parent);
            return retval;
        }

        public static string UserNickname(DTOPostComment oPostComment)
        {
            DTOUser oUser = oPostComment.User;
            string retval = oUser.NickName;
            if (retval == "")
                retval = oUser.Nom;
            if (retval == "")
                retval = oUser.EmailAddress.Substring(0, oUser.EmailAddress.IndexOf("@") - 1);
            return retval;
        }



        public string ShortenedText(int len = 99)
        {
            string retval = this.Text;
            if (retval.Length > len)
            {
                if (this.Text.IndexOf(" ", 99) > 0)
                {
                    retval = retval.Substring(0, this.Text.IndexOf(" ", 99));
                    if (retval.Length < this.Text.Length)
                        retval += "...";
                }
            }
            return retval;
        }

        public static int RecursiveCount(List<DTOPostComment> comments)
        {
            int retval = comments.Count();
            foreach (DTOPostComment item in comments)
            {
                retval += RecursiveCount(item.Answers);
            }
            return retval;
        }

        //public SyndicationItem Rss(DTOWebDomain domain)
        //{
        //    var lang = domain.DefaultLang();
        //    var retval = new SyndicationItem()
        //    {
        //        Id = this.Guid.ToString(),
        //        Title = SyndicationContent.CreatePlaintextContent(this.ParentTitle.Tradueix(lang)),
        //        Content = SyndicationContent.CreateHtmlContent(this.Text),
        //        PublishDate = this.Fch,
        //        LastUpdatedTime = this.Fch
        //    };
        //    retval.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(Url(domain.DefaultLang(), true))));
        //    return retval;
        //}

        //------------------------------------------------------------------------

        public class TreeModel
        {
            public DTOBaseGuid Target { get; set; }
            public DTOPostComment.ParentSources TargetSrc { get; set; }
            public List<Item> Items { get; set; }
            public int Take { get; set; }
            public int From { get; set; } //0 based
            public int RootItemsCount { get; set; }

            public TreeModel() : base()
            {
                this.Take = 15; //fetch comments from 15 to 15
                this.From = 0; //zero based
                this.Items = new List<Item>();
            }

            public int ItemsLeftCount()
            {
                return this.RootItemsCount - this.Items.Count - this.From;
            }

            public int NextCount()
            {
                //per el link ver {NextCount] más...
                int retval = Take < ItemsLeftCount() ? Take : ItemsLeftCount();
                return retval;
            }

            public bool MoreItems()
            {
                return (NextCount() > 0);
            }

            public class Item
            {
                public Guid Guid { get; set; }
                public int Idx { get; set; }
                public int Siblings { get; set; }
                public DateTime Fch { get; set; }
                public Author Author { get; set; }
                public string Text { get; set; }
                public Item Answering { get; set; }
                public Guid AnswerRootGuid { get; set; }
                public List<Item> Items { get; set; }

                public Item()
                {
                    this.Items = new List<Item>();
                }

                public static Item Factory(DTOPostComment comment)
                {
                    Item retval = new Item();
                    retval.Guid = comment.Guid;
                    if (retval.Guid != Guid.Empty)
                    {
                        retval.Author = new Author();
                        retval.Author.Guid = comment.User.Guid;
                        retval.Author.Nom = comment.User.NicknameForComments();
                        retval.Fch = comment.Fch;
                        retval.Text = comment.Text;
                        if (comment.Answering != null)
                        {
                            retval.Answering = Item.Factory(comment.Answering);
                        }
                        if (comment.AnswerRoot != null)
                        {
                            retval.AnswerRootGuid = comment.AnswerRoot.Guid;
                        }
                    }
                    return retval;
                }


                public string ShortenedText(int len = 99)
                {
                    string retval = "";
                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        retval = this.Text;
                        if (retval.Length > len)
                        {
                            if (this.Text.IndexOf(" ", 99) > 0)
                            {
                                retval = retval.Substring(0, this.Text.IndexOf(" ", 99));
                                if (retval.Length < this.Text.Length)
                                    retval += "...";
                            }
                        }
                    }
                    return retval;
                }

                public string Caption(DTOLang lang)
                {
                    StringBuilder sb = new StringBuilder();
                    if (this.Author != null)
                    {
                        sb.AppendFormat("{0:dd/MM/yy} {0:HH:mm} {1}", this.Fch, this.Author.Nom);
                        if (this.Answering != null)
                        {
                            sb.AppendFormat(" {0} {1}", lang.Tradueix("en respuesta a", "en resposta a", "answering"), this.Answering.Author.Nom);
                        }
                    }
                    return sb.ToString();
                }
            }

            public class Author
            {
                public Guid Guid { get; set; }
                public string Nom { get; set; }

                public Author() : base()
                {

                }
            }

        }
    }


}