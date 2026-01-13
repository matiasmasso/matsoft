using DTO;
using System.IO.Compression;

namespace Api.Services
{
    public class PortadaImgService
    {
        public static ImageMime? ImageMime(string id)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PortadaImgs
                    .Where(x => x.Id == id)
                    .Select(x => new ImageMime
                    {
                        Image = x.Img ?? new byte[] { },
                        Mime = x.Mime == null ? MimeHelper.MimeCods.NotSet : (MimeHelper.MimeCods)x.Mime
                    })
                    .FirstOrDefault();
            }
        }
        public static async Task<bool> UpdateAsync(PortadaImgModel value, IFormFile? file)
        {
            bool retval = false;
            if (value.Id != null)
            {
                using (var db = new Entities.MaxiContext())
                {
                    var entity = db.PortadaImgs.Find(value.Id);
                    if (entity == null)
                    {
                        entity = new Entities.PortadaImg();
                        entity.Id = value.Id.ToString();
                        db.PortadaImgs.Add(entity);
                    }

                    entity.Title = value.Title;
                    entity.Url = value.NavigateTo ?? "";

                    if (file != null)
                    {
                        entity.Mime = (int)value.Mime;
                        entity.Img = await file.BytesAsync();
                    }

                    db.SaveChanges();
                    retval = true;
                }
            }
            return retval;
        }
    }

    public class PortadaImgsService
    {
        public static List<PortadaImgModel> All(LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db, lang);
            }
        }
        public static List<PortadaImgModel> All(Entities.MaxiContext db, LangDTO lang)
        {
            return db.PortadaImgs
                .OrderBy(x => x.Id)
                .Select(x => new PortadaImgModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    NavigateTo = x.Url
                })
                .ToList();
        }
    }
}
