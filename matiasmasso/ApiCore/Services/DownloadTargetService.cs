using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class DownloadTargetService
    {

        public static Media? Thumbnail(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DownloadTargets
                    .AsNoTracking()
                    .Where(x => x.Target == target)
                    .Select(x => new Media
                    {
                        Data = x.DownloadNavigation.HashNavigation.Thumbnail,
                        Mime = Media.MimeCods.Jpg
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        public static bool Update(DownloadTargetModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                //var download = db.ProductDownloads
                //    .FirstOrDefault(x => x.Hash == value.DocFile!.Hash);

                //if(download == null)
                //{
                //    download = new Entities.ProductDownload();
                //    download.Hash = value.DocFile!.Hash!;
                //    download.Product = value.
                //}
                //var entity = db.DownloadTargets
                //    .Where(x => x.Target == value.Target
                //    && x.DownloadNavigation.HashNavigation.Hash == value.DocFile!.Hash )
                //    .FirstOrDefault();


                //if (entity == null)
                //{
                //    entity = new Entities.DownloadTarget();
                //    db.DownloadTargets.Add(entity);
                //    entity.Target = value.Target;
                //    entity.Download = download.Guid;
                //}
                //else
                //    entity = db.DownloadTargets.Find(value.Guid);

                //if (entity == null) throw new System.Exception("VisaCard not found");

                //entity.Emp = (int)value.Emp;
                //entity.Emisor = (Guid)value.Emisor!;
                //entity.Banc = (Guid)value.Banc!;
                //entity.Titular = (Guid)value.Titular!;
                //entity.Alias = value.Nom ?? "";
                //entity.Digits = value.Digits ?? "";
                //entity.Caduca = value.Caduca ?? "";
                //entity.FchCanceled = value.FchTo;

                //db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                //TO DO:
            }
            return true;
        }


    }

    public class DownloadTargetsService
    {

        public static List<DownloadTargetModel> FromTarget(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwDownloadTargets
                    .AsNoTracking()
                    .Where(x => x.Target == target)
                    .Select(y => new DownloadTargetModel
                    {
                        Target = target,
                        PublicarAlDistribuidor = y.PublicarAlDistribuidor ?? false,
                        PublicarAlConsumidor = y.PublicarAlConsumidor ?? false,
                        Langset = new LangDTO.Set(y.Langset),
                        Obsoleto = y.Obsoleto,
                        DocFile = new DocfileModel
                        {
                            Hash = y.DocfileHash,
                            Nom = y.DocfileNom,
                            FchCreated = y.DocFileFchCreated,
                            Fch = y.DocfileFch,
                            Document = new DTO.Media((Media.MimeCods)y.DocfileMime, null),
                            Thumbnail = new DTO.Media((Media.MimeCods)y.ThumbnailMime, null),
                            Size = y.DocfileLength,
                            Pags = y.DocfilePags,
                            Height = y.DocfileHeight,
                            Width = y.DocfileWidth,
                            HRes = y.DocfileHres,
                            VRes = y.DocfileVres,
                        }
                    })
                    .ToList();
                return retval;
            }
        }

        public static List<DownloadTargetModel> All(Guid target)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.DownloadTargets
                    .AsNoTracking()
                    .Where(x => x.Target == target)
                    .Select(x => new DownloadTargetModel
                    {
                        Target = x.Target,
                        Src = (DownloadTargetModel.Srcs)x.Cod,
                        DocFile = new DocfileModel
                        {
                            Hash = x.DownloadNavigation.Hash,
                            Nom = x.DownloadNavigation.HashNavigation.Nom,
                            Fch = x.DownloadNavigation.HashNavigation.Fch,
                            Document = new DTO.Media((Media.MimeCods)x.DownloadNavigation.HashNavigation.Mime, null),
                            Thumbnail = new DTO.Media((Media.MimeCods)x.DownloadNavigation.HashNavigation.ThumbnailMime, null),
                            Size = x.DownloadNavigation.HashNavigation.Size,
                            Pags = x.DownloadNavigation.HashNavigation.Pags,
                            Width = x.DownloadNavigation.HashNavigation.Width,
                            Height = x.DownloadNavigation.HashNavigation.Height,
                            HRes = x.DownloadNavigation.HashNavigation.Hres,
                            VRes = x.DownloadNavigation.HashNavigation.Vres,
                            FchCreated = x.DownloadNavigation.HashNavigation.FchCreated
                        }
                    })
                    .ToList();
            }
        }
    }
}
