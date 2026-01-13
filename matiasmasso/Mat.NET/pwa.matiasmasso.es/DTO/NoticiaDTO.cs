using DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class NoticiaDTO
    {
        public string? Content { get; set; }
        public List<Box> Boxes { get; set; } = new();
    }

    public class NoticiaModel : BaseGuid
    {
        public int? Emp { get; set; }
        public DateTime Fch { get; set; }
        public LangTextModel Caption { get; set; }
        public LangTextModel Excerpt { get; set; }
        public LangTextModel Content { get; set; }
        public LangTextModel UrlSegment { get; set; }
        public Guid? Brand { get; set; }
        public Guid? Cnap { get; set; }
        public bool Professional { get; set; }
        public bool Visible { get; set; }
        public int Priority { get; set; }
        public int Cod { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
        public Guid? Location { get; set; }
        public DateTime? DestacarFrom { get; set; }
        public DateTime? DestacarTo { get; set; }
        public UsrLogModel UsrLog { get; set; } = new();

        public const int THUMBNAILWIDTH = 325;
        public const int THUMBNAILHEIGHT = 205;


        public NoticiaModel() : base()
        {
            Caption = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentTitle);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentText);
            UrlSegment = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentUrl);
        }
        public NoticiaModel(Guid guid) : base(guid) {
            Caption = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentTitle);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentText);
            UrlSegment = new LangTextModel(base.Guid, LangTextModel.Srcs.ContentUrl);
        }

        public string Url(LangDTO lang) => string.Format("news/{0}.html", UrlSegment.Tradueix(lang));
        public string ThumbnailUrl() => Globals.ApiUrl("noticia/thumbnail", Guid.ToString());

        public bool IsActive() => Visible == true && (FchFrom == null || FchFrom <= DateTime.Now) && (FchTo == null || FchTo >= DateTime.Now);

        public Box Box(LangDTO lang) => new Box
             {
                 Caption = Caption.Tradueix(lang),
                 Url = UrlHelper.RelativeUrl(lang, Url(lang)),
                 ImageUrl = ThumbnailUrl()
        };

        public string Html(LangDTO lang) => Content.Tradueix(lang).Html();

        public override string ToString() => String.Format("Noticia {0:dd/MM/yy} {1}", Fch, Caption.Esp);
    }
}
