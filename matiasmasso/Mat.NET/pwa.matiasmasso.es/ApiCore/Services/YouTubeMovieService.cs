using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.Services
{
    //

    public class YouTubeMovieService
    {
        public static ImageMime? Thumbnail(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.YouTubes
                        .AsNoTracking()
                    .Where(x => x.Guid == guid && x.Thumbnail != null)
                    .Select(x => new ImageMime
                    {
                        Mime = (Media.MimeCods)(x.ThumbnailMime ?? 0),
                        Image = x.Thumbnail!
                    }).FirstOrDefault();
            }
        }
        public static YouTubeMovieModel? FromSegment(string segment)
        {
            using (var db = new Entities.MaxiContext())
            {
                var decodedSegment = WebUtility.UrlDecode(segment);
                var retval = db.VwYouTubes
                        .AsNoTracking()
                    .Where(x => x.NomEsp == decodedSegment || x.NomCat == decodedSegment || x.NomEng == decodedSegment || x.NomPor == decodedSegment)
                    .Select(x => new YouTubeMovieModel(x.Guid)
                    {
                        YoutubeId = x.YoutubeId,
                        FchCreated = x.FchCreated,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        LangSet = new LangDTO.Set(x.LangSet),
                        Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeNom) { Esp = x.NomEsp, Cat = x.NomCat, Eng = x.NomEng, Por = x.NomPor },
                        Excerpt = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeExcerpt) { Esp = x.DscEsp, Cat = x.DscCat, Eng = x.DscEng, Por = x.DscPor },
                        Duration = x.Duration == null ? null : TimeSpan.FromSeconds((int)x.Duration),
                        FchTo = x.FchTo,
                        Obsoleto = x.Obsoleto,
                        ThumbnailMime = (DTO.Media.MimeCods?)x.ThumbnailMime,
                        Tags = x.Tags == null ? new List<string>() : x.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    }).FirstOrDefault();

                if (retval != null)
                {
                    retval.Products = db.YouTubeProducts
                        .AsNoTracking()
                        .Where(x => x.YouTubeGuid == retval.Guid)
                        .Select(x => x.ProductGuid)
                        .ToList();
                }

                return retval;
            }
        }
    }


    public class YouTubeMoviesService
    {
        public static List<YouTubeMovieModel> BrandVideos(ProductBrandModel brand)
        {
            using (var db = new Entities.MaxiContext())
            {
                var products = db.YouTubeProducts
                        .AsNoTracking()
                    .Select(x => new KeyValuePair<Guid, Guid>(x.YouTubeGuid, x.ProductGuid))
                    .ToList();

                var retval = db.VwBrandVideos
                        .AsNoTracking()
                    .Where(x => x.Brand == brand.Guid)
                    .OrderByDescending(x => x.FchCreated)
                    .Select(x => new YouTubeMovieModel(x.Guid)
                    {
                        YoutubeId = x.YoutubeId,
                        FchCreated = x.FchCreated,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        LangSet = new LangDTO.Set(x.LangSet),
                        Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeNom) { Esp = x.NomEsp, Cat = x.NomCat, Eng = x.NomEng, Por = x.NomPor },
                        Excerpt = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeExcerpt) { Esp = x.DscEsp, Cat = x.DscCat, Eng = x.DscEng, Por = x.DscPor },
                        Duration = x.Duration == null ? null : TimeSpan.FromSeconds((int)x.Duration),
                        FchTo = x.FchTo,
                        Obsoleto = x.Obsoleto,
                        ThumbnailMime = (DTO.Media.MimeCods?)x.ThumbnailMime,
                        Tags = x.Tags == null ? new List<string>() : x.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    })
                    .ToList();

                foreach (var movie in retval)
                {
                    movie.Products = products.Where(x => x.Key == movie.Guid).Select(x => x.Value).ToList();
                }

                return retval;

            }
        }
        public static List<YouTubeMovieModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var products = db.YouTubeProducts
                        .AsNoTracking()
                .Select(x => new Tuple<Guid, Guid>(x.YouTubeGuid, x.ProductGuid))
                .ToList();

                var retval = db.VwYouTubes
                        .AsNoTracking()
                .Select(x => new YouTubeMovieModel(x.Guid)
                {
                    YoutubeId = x.YoutubeId,
                    FchCreated = x.FchCreated,
                    Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                    LangSet = new LangDTO.Set(x.LangSet),
                    Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeNom) { Esp = x.NomEsp, Cat = x.NomCat, Eng = x.NomEng, Por = x.NomPor },
                    Excerpt = new LangTextModel(x.Guid, LangTextModel.Srcs.YouTubeExcerpt) { Esp = x.DscEsp, Cat = x.DscCat, Eng = x.DscEng, Por = x.DscPor },
                    Duration = x.Duration == null ? null : TimeSpan.FromSeconds((int)x.Duration),
                    FchTo = x.FchTo,
                    Obsoleto = x.Obsoleto,
                    ThumbnailMime = (DTO.Media.MimeCods?)x.ThumbnailMime,
                    Tags = x.Tags == null ? new List<string>() : x.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()

                }).ToList();

                foreach (var movie in retval)
                {
                    movie.Products = products.Where(x => x.Item1 == movie.Guid).Select(x => x.Item2).ToList();
                }

                return retval;
            }
        }
    }
}
