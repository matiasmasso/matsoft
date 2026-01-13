using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class YouTubeMovieModel:BaseGuid
    {
        public string? YoutubeId { get; set; }
        public LangTextModel Nom { get; set; }
        public LangTextModel Excerpt { get; set; }
        public LangDTO? Lang { get; set; }
        public LangDTO.Set? LangSet { get; set; }
        public DateTime FchCreated { get; set; }
        public List<Guid> Products { get; set; } = new();

        public TimeSpan? Duration { get; set; }
        public DateTime? FchTo { get; set; }
        public bool Obsoleto { get; set; }
        public List<string> Tags { get; set; } = new();
        public Media.MimeCods? ThumbnailMime { get; set; }

        // Public Const UrlSitePrefix As String = "https://www.youtube.com/watch?v="
        public const string UrlSitePrefix = "https://youtu.be";
        public const string UrlEmbedPrefix = "https://www.youtube.com/embed";


        public YouTubeMovieModel() : base()
        {
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.YouTubeNom);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.YouTubeExcerpt);
        }

        public YouTubeMovieModel(Guid guid) : base(guid)
        {
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.YouTubeNom);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.YouTubeExcerpt);
        }

        public string AbsoluteUrl() => string.Format("{0}/{1}", UrlSitePrefix, YoutubeId ?? "");
        public string EmbedUrl() => string.Format("{0}/{1}", UrlEmbedPrefix, YoutubeId ?? "");

        public string ThumbnailUrl() => Globals.ApiUrl("youtubeMovie/thumbnail", Guid.ToString());

        public string LandingPageUrl(LangDTO lang)
        {
            var segment = Uri.EscapeDataString(Nom.Tradueix(lang));
            return string.Format("video/{0}", segment);
        }


        public bool GoogleSearchQualified()
        {
            bool retval = true;
            if (Lang == null)
            {
                if (string.IsNullOrEmpty(Nom?.Esp)) retval = false;
                if (string.IsNullOrEmpty(Excerpt?.Esp)) retval = false;
            } else
            {
                if (string.IsNullOrEmpty(Nom.Text(Lang))) retval = false;
                if (string.IsNullOrEmpty(Excerpt.Text(Lang))) retval = false;
            }
            if (Duration == null) retval = false;
            if (ThumbnailMime == null || ThumbnailMime == Media.MimeCods.NotSet) retval = false;
            return retval;
        }
    }
}
