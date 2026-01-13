using DTO;

namespace Api.Services
{

    public class DocfileService
    {
        public static DocFileModel? Media(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFiles
                              where x.Hash == hash
                              select new DocFileModel()
                              {
                                    StreamMime = x.Mime,
                                  Document = x.Doc
                              }).FirstOrDefault();
                return retval;
            }
        }

        public static Byte[]? Thumbnail(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DocFiles.
                    Where(x => x.Hash == hash)
                    .Select(x => x.Thumbnail)
                    .FirstOrDefault();
                return retval;
            }
        }

        public static DocFileModel? Fetch(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFiles
                              where x.Hash == hash
                              select new DocFileModel()
                              {
                                  Hash = x.Hash,
                                  Nom = x.Nom,
                                  Fch = x.Fch,
                                  StreamMime = x.Mime,
                                  Size = x.Size,
                                  Pags = x.Pags,
                                  Width = x.Width,
                                  Height = x.Height,
                                  HRes = x.Hres,
                                  VRes = x.Vres,
                                  //Obsoleto = x.Obsolet,
                                  FchCreated = x.FchCreated
                              }).FirstOrDefault();
                return retval;
            }
        }

        public static async Task<bool> UpdateAsync(Entities.MaxiContext db, DocFileModel value, IFormFile? file, IFormFile? thumbnail)
        {
            bool retval = false;
            if (value.Hash != null)
            {
                var entity = db.DocFiles.Find(value.Hash);
                if (entity == null)
                {
                    entity = new Entities.DocFile();
                    entity.Hash = value.Hash;
                    db.DocFiles.Add(entity);
                }

                entity.Pags = value.Pags ?? 0;
                entity.Size = (int?)value.Size ?? 0;
                entity.Width = (int?)value.Width ?? 0;
                entity.Height = (int?)value.Height ?? 0;
                entity.Hres = (int?)value.HRes ?? 0;
                entity.Vres = (int?)value.VRes ?? 0;

                if (file != null)
                {
                    entity.Mime = (int)value.StreamMime;
                    entity.Doc = await file.BytesAsync();
                }

                if (thumbnail != null)
                {
                    entity.ThumbnailMime = (int)value.ThumbnailMime;
                    entity.Thumbnail = await thumbnail.BytesAsync();
                }

                db.SaveChanges();
                retval = true;
            }
            return retval;
        }

        public static bool Delete(String hash)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.DocFiles.Remove(db.DocFiles.Single(x => x.Hash.Equals(hash)));
                db.SaveChanges();
            }
            return true;

        }
    }

    public class DocfilesService
    {

        public static List<DocFileModel> FromSrc(Guid srcGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromSrc(db, srcGuid);
            }
        }
        public static List<DocFileModel> FromSrc(Entities.MaxiContext db, Guid srcGuid)
        {
                return db.DocFileSrcs.
                    Where(x => x.SrcGuid == srcGuid).
                    Join(db.VwDocfiles, s => s.Hash, f => f.DocfileHash, (s, f) => new DocFileModel
                    {
                        Hash = f.DocfileHash,
                        Nom = f.DocfileNom,
                        FchCreated = f.DocFileFchCreated,
                        Fch = f.DocfileFch,
                        StreamMime = f.DocfileMime,
                        Size = f.DocfileLength,
                        Pags = f.DocfilePags,
                        Height = f.DocfileHeight,
                        Width = f.DocfileWidth,
                        HRes = f.DocfileHres,
                        VRes = f.DocfileVres
                    })
                .OrderByDescending(x => x.Fch ?? x.FchCreated)
                .ToList();
        }

        public static List<DocFileModel> FromTarget(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromTarget(db, target);
            }
        }

        public static List<DocFileModel> FromTarget(Entities.MaxiContext db, Guid target)
        {
            var retval = db.VwDownloadTargets.
                Where(x => x.Target == target).
                Select(y => new DocFileModel
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
                })
                .OrderByDescending(x => x.Fch ?? x.FchCreated)
                .ToList();
            return retval;
        }



        public static List<DocFileModel> Fetch(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFileSrcs
                              join y in db.DocFiles on x.Hash equals y.Hash
                              where x.SrcGuid == target
                              orderby x.SrcOrd, y.Fch descending
                              select new DocFileModel()
                              {
                                  Hash = x.Hash,
                                  Nom = y.Nom,
                                  Fch = y.Fch,
                                  StreamMime = y.Mime,
                                  Size = y.Size,
                                  Pags = y.Pags,
                                  Width = y.Width,
                                  Height = y.Height,
                                  HRes = y.Hres,
                                  VRes = y.Vres,
                                  //Obsoleto = y.Obsolet,
                                  FchCreated = y.FchCreated
                              }).ToList();
                return retval;
            }
        }
    }
}
