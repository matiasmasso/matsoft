using DTO;

namespace Api.Services
{
    public class DownloadTargetService
    {
        public static DownloadTargetModel FromTarget(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwDownloadTargets.
                    Where(x => x.Target == target).
                    Select(y => new DownloadTargetModel
                    {
                        Target = target,
                        DocFile = new DocFileModel
                        {
                            Hash = y.DocfileHash,
                            Nom = y.DocfileNom,
                            FchCreated = y.DocFileFchCreated,
                            Fch = y.DocfileFch,
                            StreamMime = y.DocfileMime,
                            Size = y.DocfileLength,
                            Pags = y.DocfilePags,
                            Height = y.DocfileHeight,
                            Width = y.DocfileWidth,
                            HRes = y.DocfileHres,
                            VRes = y.DocfileVres
                        }
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        public static Media? Thumbnail(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DownloadTargets.
                    Where(x => x.Target == target).
                    Select(x => new Media
                    {
                        Data = x.DownloadNavigation.HashNavigation.Thumbnail,
                        Mime = (int)Media.MimeCods.Jpg
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        
    }

    public class DownloadTargetsService
    {
        public static List<DownloadTargetModel> All(Guid target)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.DownloadTargets
                    .Where(x => x.Target == target)
                    .Select(x => new DownloadTargetModel
                    {
                        Target = x.Target,
                        Cod= x.Cod,
                        DocFile = new DocFileModel
                        {
                            Hash = x.DownloadNavigation.Hash,
                            Nom = x.DownloadNavigation.HashNavigation.Nom,
                            Fch = x.DownloadNavigation.HashNavigation.Fch,
                            StreamMime = x.DownloadNavigation.HashNavigation.Mime,
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
