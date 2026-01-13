using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContentModel:BaseGuid
    {
        public LangTextModel Caption { get; set; }
        public LangTextModel Excerpt { get; set; }
        public LangTextModel Content { get; set; }
        public LangTextModel UrlSegment { get; set; }

        public ContentModel() : base()
        {
            Caption = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentTitle);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentText);
            UrlSegment = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentUrl);
        }
        public ContentModel(Guid guid) : base(guid)
        {
            Caption = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentTitle);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentText);
            UrlSegment = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentUrl);
        }

        public string Url(LangDTO lang) => string.Format("content/{0}.html", UrlSegment.Tradueix(lang));

        public string Html(LangDTO lang) => Content.Tradueix(lang).Html();

        public class Comment:BaseGuid
        {
            public UserModel? User { get; set; }
            public DateTime? Fch { get; set; }
            public ContentModel? Content { get; set; }
            public Srcs ContentSrc { get; set; }
            public LangDTO? Lang { get; set; }
            public Guid? Answering { get; set; }
            public Guid? AnswerRoot { get; set; }
            public DateTime? FchApproved { get; set; }
            public DateTime? FchDeleted { get; set; }
            public string? Text { get; set; }

            public bool IsExpanded { get; set; } = false; // for client

            public Comment? AnsweringMsg { get; set; }
            public List<Comment>? Answers { get; set; }
            public bool HasAnswers { get; set; }

            public enum StatusEnum
            {
                NotSet,
                Pendent,
                Aprobat,
                Eliminat
            }

            public enum Srcs
            {
                NotSet,
                Noticia,
                News,
                Blog,
                Contact4moms,
                ContactMmo
            }

            public Comment() : base() { }
            public Comment(Guid guid) : base(guid) { }

            public Comment? FirstAnswer() => Answers?.FirstOrDefault();

            public override bool Matches(string? searchTerm)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    var searchTarget = Text ?? "";
                    //var searchTarget = Contact?.Nom ?? "" + " " + Id.ToString() ?? "";
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }
        }

    }
}
