using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class DocfileSrcService
    {

        public static bool Update(DocfileSrcModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                DocfileService.Update(db, value.Docfile);

                var entity = db.DocFileSrcs.FirstOrDefault(x => x.SrcGuid == value.Target && x.Hash == value.Docfile.Hash);
                if (entity == null)
                {
                    entity = new Entities.DocFileSrc();
                    db.DocFileSrcs.Add(entity);
                    entity.SrcGuid = value.Target;
                    entity.Hash = value.Docfile.Hash!;
                }

                entity.SrcCod = (int)value.Cod;
                entity.SrcOrd = (int)value.Ord;
                db.SaveChanges();
            }
            return true;
        }

        public static bool Delete(DocfileSrcModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.DocFileSrcs.FirstOrDefault(x => x.SrcGuid == value.Target && x.Hash == value.Docfile!.Hash );
                if(entity != null)
                 db.DocFileSrcs.Remove(entity);
                db.SaveChanges();
            }
            return true;

        }


    }

    public class DocfileSrcsService
    {
        public static List<DocfileSrcModel> GetValues(Entities.MaxiContext db, IModel model)
        {
            return db.DocFileSrcs
                .AsNoTracking()
                .Where(x => x.SrcGuid == model.Guid)
                .Join(db.VwDocfiles, s => s.Hash, f => f.DocfileHash, (s, f) => new DocfileSrcModel()
                {
                    Cod = (DocfileModel.Cods)s.SrcCod,
                    Ord = s.SrcOrd,
                    Target = model.Guid,
                    Docfile = new DocfileModel(f.DocfileHash)
                    {
                        Nom = f.DocfileNom,
                        FchCreated = f.DocFileFchCreated,
                        Fch = f.DocfileFch,
                        Document = new DTO.Media((Media.MimeCods)f.DocfileMime, null),
                        Thumbnail = new DTO.Media((Media.MimeCods)f.ThumbnailMime, null),
                        Size = f.DocfileLength,
                        Pags = f.DocfilePags,
                        Height = f.DocfileHeight,
                        Width = f.DocfileWidth,
                        HRes = f.DocfileHres,
                        VRes = f.DocfileVres
                    }
                })
                .OrderBy(x => x.Ord)
            .ThenByDescending(x =>  x.Docfile.FchCreated)
            .ToList();
        }

    }
}
