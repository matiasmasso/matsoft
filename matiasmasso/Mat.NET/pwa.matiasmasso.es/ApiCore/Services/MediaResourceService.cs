using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api.Services
{
    public class MediaResourceService
    {
        public static MediaResourceModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwMediaResources
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => ModelFromEntity(x))
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Targets = db.MediaResourceTargets
                        .AsNoTracking()
                        .Where(x => x.Parent == retval.Guid)
                        .Select(x => x.Target)
                        .ToList();
                }

                return retval;
            }
        }

        public static MediaResourceModel? FromFilename(string filename)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwMediaResources
                        .AsNoTracking()
                        .Where(x => x.Nom == filename)
                        .Select(x => ModelFromEntity(x))
                        .FirstOrDefault();

                if (retval != null)
                {
                    retval.Targets = db.MediaResourceTargets
                        .AsNoTracking()
                        .Where(x => x.Parent == retval.Guid)
                        .Select(x => x.Target)
                        .ToList();
                }

                return retval;
            }
        }

        public static ImageMime? Thumbnail(string id)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (id.IsGuid())
                {
                return db.MediaResources
                        .AsNoTracking()
                       .Where(x => x.Guid == new Guid(id))
                        .Select(x => new ImageMime
                        {
                            Image = x.Thumbnail ?? new byte[] { },
                            Mime = Media.MimeCods.Jpg
                        })
                        .FirstOrDefault();

                } else
                {
                    return db.MediaResources
                        .AsNoTracking()
                       .Where(x => x.Nom == id)
                            .Select(x => new ImageMime
                            {
                                Image = x.Thumbnail ?? new byte[] { },
                                Mime = Media.MimeCods.Jpg
                            })
                            .FirstOrDefault();
                }
            }
        }

        public static MediaResourceModel ModelFromEntity(Entities.VwMediaResource x)
        {
            return new MediaResourceModel(x.Guid)
            {
                Hash = x.Hash,
                Ord = x.Ord,
                Mime = (DTO.Media.MimeCods)x.Mime,
                Cod = (MediaResourceModel.Cods)x.Cod,
                Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                LangSet = new LangDTO.Set(x.LangSet),
                Filename = x.Nom,
                Description = new LangTextModel(x.Esp, x.Cat, x.Eng, x.Por),
                Width = x.Width,
                Height = x.Height,
                Size = x.Size,
                HRes = x.Hres,
                VRes = x.Vres,
                Pags = x.Pags,
                Obsoleto = x.Obsoleto,
                UsrLog = new UsrLogModel()
                {
                    FchCreated = x.FchCreated,
                    FchLastEdited = x.FchLastEdited
                },
            };
        }
    }
    public class MediaResourcesService
    {
        public static List<MediaResourceModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwMediaResources
                        .AsNoTracking()
                        .Select(x => MediaResourceService.ModelFromEntity(x))
                        .ToList();

                    var targets = db.MediaResourceTargets
                        .AsNoTracking()
                        .Select(x => new KeyValuePair<Guid,Guid>(x.Parent, x.Target))
                        .ToList();

                foreach(var media in retval)
                {
                    media.Targets = targets.Where(x=>x.Key == media.Guid).Select(x=>x.Value).ToList();
                }

                return retval;
            }

        }

        public static List<MediaResourceModel> FromBrand(ProductBrandModel brand)
        {
            var retval = new List<MediaResourceModel>();
            using (var db = new Entities.MaxiContext())
            {
                List<Guid> guids = db.VwMediaResourceTargets
                        .AsNoTracking()
                        .Where(x => x.Brand == brand.Guid && !x.Obsoleto)
                        .Select(x => x.MediaResource)
                        .Distinct()
                        .ToList();

                retval = db.VwMediaResources
                        .AsNoTracking()
                        .Where(x => guids.Contains(x.Guid))
                        .Select(x => MediaResourceService.ModelFromEntity(x))
                        .ToList();
            }
            return retval;
        }



    }
}
