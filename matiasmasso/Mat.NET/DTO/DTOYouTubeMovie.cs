using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOYouTubeMovie : DTOBaseGuid
    {
        public string YoutubeId { get; set; } = "";
        public DTOLangText Nom { get; set; } 
        public DTOLangText Dsc { get; set; }
        public DTOLang Lang { get; set; }
        public DTOLang.Set LangSet { get; set; }
        public DateTime FchCreated { get; set; }
        public List<DTOProduct> Products { get; set; }

        public TimeSpan? Duration { get; set; }
        public DateTime? FchTo { get; set; }
        public List<string> Tags { get; set; }
        public Byte[] Thumbnail { get; set; }
        public MatHelperStd.MimeCods ThumbnailMime { get; set; }

        public bool Obsoleto { get; set; }

        // Public Const UrlSitePrefix As String = "https://www.youtube.com/watch?v="
        public const string UrlSitePrefix = "https://youtu.be/";
        public const string UrlEmbedPrefix = "https://www.youtube.com/embed/";

        public bool HasThumbnail() => ThumbnailMime != MatHelperStd.MimeCods.NotSet;

        public DTOYouTubeMovie() : base()
        {
            Nom = new DTOLangText(Guid, DTOLangText.Srcs.YouTubeNom);
            Dsc = new DTOLangText(Guid, DTOLangText.Srcs.YouTubeExcerpt);
            LangSet = DTOLang.Set.Default();
            Products = new List<DTOProduct>();
            Tags = new List<string>();
        }

        public DTOYouTubeMovie(Guid oGuid) : base(oGuid)
        {
            Nom = new DTOLangText(Guid, DTOLangText.Srcs.YouTubeNom);
            Dsc = new DTOLangText(Guid, DTOLangText.Srcs.YouTubeExcerpt);
            LangSet = DTOLang.Set.Default();
            Products = new List<DTOProduct>();
            Tags = new List<string>();
        }

        public static DTOYouTubeMovie Factory(DTOProduct oProduct)
        {
            DTOYouTubeMovie retval = new DTOYouTubeMovie();
            {
                var withBlock = retval;
                withBlock.Products.Add(oProduct);
            }
            return retval;
        }

        public static string Url_YouTubeSite(DTOYouTubeMovie oMovie)
        {
            string retval = DTOYouTubeMovie.UrlSitePrefix + oMovie.YoutubeId;
            return retval;
        }

        public static string FullText(DTOYouTubeMovie oMovie)
        {
            string retval = "[" + oMovie.YoutubeId + "] " + oMovie.Nom;
            return retval;
        }

        public class ProductModel
        {
            public List<Video> Videos { get; set; }
            public List<ProductVideosClass> ProductVideos { get; set; }
            public DTOBasicCatalog Catalog { get; set; }

            public ProductModel()
            {
                this.Videos = new List<Video>();
                this.Catalog = new DTOBasicCatalog();
                this.ProductVideos = new List<ProductVideosClass>();
            }

            public class ProductVideosClass
            {
                public Guid Product { get; set; }
                public List<Guid> Videos { get; set; }

                public ProductVideosClass()
                {
                    this.Videos = new List<Guid>();
                }
            }

            public class Video
            {
                public Guid Guid { get; set; }
                public string Nom { get; set; }
                public DateTime FchCreated { get; set; }
                public string YouTubeId { get; set; }

                public List<Guid> Products { get; set; }

                public Video() : base() { }

                //public string SerializedProducts()
                //{
                //    var serializer = new JavaScriptSerializer();
                //    string retval = serializer.Serialize(this.Products);
                //    return retval;

                //}

            }

        }
    }
}
