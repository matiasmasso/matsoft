using Api.Entities;
using Api.Extensions;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Api.Services
{

    public class DocfileService
    {
        public static DocfileModel? GetValue(string hash)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DocFiles
                              .Where(x=> x.Hash == hash)
                              .Select(x=> new DocfileModel(hash)
                              {
                                  Nom = x.Nom,
                                  Fch = x.Fch,
                                  Document = new Media((Media.MimeCods)x.Mime, null),
                                  Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
                                  Size = x.Size,
                                  Pags = x.Pags,
                                  Width = x.Width,
                                  Height = x.Height,
                                  HRes = x.Hres,
                                  VRes = x.Vres,
                                  FchCreated = x.FchCreated
                              }).FirstOrDefault();
                return retval;
            }
        }

        public static DocfileModel? Media(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFiles
                              where x.Hash == hash
                              select new DocfileModel()
                              {
                                  Document = new Media((Media.MimeCods)x.Mime, x.Doc)
                              }).FirstOrDefault();
                return retval;
            }
        }

        public static Byte[]? Thumbnail(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DocFiles
                    .AsNoTracking()
                    .Where(x => x.Hash == hash)
                    .Select(x => x.Thumbnail)
                    .FirstOrDefault();
                return retval;
            }
        }

        public static DocfileModel? Fetch(string base64Hash)
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(base64Hash);
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFiles
                              where x.Hash == hash
                              select new DocfileModel()
                              {
                                  Hash = x.Hash,
                                  Nom = x.Nom,
                                  Fch = x.Fch,
                                  Document = new Media((Media.MimeCods)x.Mime, null),
                                  Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
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

        public static bool Update(Entities.MaxiContext db, DocfileModel value)
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

                    entity.Pags = value.Pags ?? 0;
                    entity.Size = (int?)value.Size ?? 0;
                    entity.Width = (int?)value.Width ?? 0;
                    entity.Height = (int?)value.Height ?? 0;
                    entity.Hres = (int?)value.HRes ?? 0;
                    entity.Vres = (int?)value.VRes ?? 0;
                    entity.Mime = (int)(value.Document?.Mime ?? DTO.Media.MimeCods.NotSet);
                    entity.ThumbnailMime = (int)(value.Thumbnail?.Mime ?? DTO.Media.MimeCods.NotSet);

                    if (value.Document?.Data != null)
                    {
                        entity.Doc = value.Document.Data;
                    }

                    if (value.Thumbnail?.Data != null)
                    {
                        entity.Thumbnail = value.Thumbnail.Data;
                    }
                }

                entity.Nom = (string.IsNullOrEmpty(value.Nom) ? value.Filename : value.Nom)?.Truncate(50);
                if(value.Fch != null)
                    entity.Fch = (DateTime)value.Fch;
                db.SaveChanges();
                retval = true;
            }
            return retval;
        }

        public static async Task<bool> UpdateAsync(Entities.MaxiContext db, DocfileModel value, IFormFile file, IFormFile? thumbnail)
        {
            if (file != null)
                value.Document = new DTO.Media(file.ContentType, file.ToByteArray());
                //value.Document = new DTO.Media(file.ContentType, await file.BytesAsync());


            if (thumbnail != null)
                value.Thumbnail = new DTO.Media(thumbnail.ContentType, thumbnail.ToByteArray());
                //value.Thumbnail = new DTO.Media(thumbnail.ContentType, await thumbnail.BytesAsync());

            bool retval = Update(db, value);
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

        public static List<DocfileSrcModel> FromSrc(Guid srcGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromSrc(db, srcGuid);
            }
        }
        public static List<DocfileSrcModel> FromSrc(Entities.MaxiContext db, Guid srcGuid)
        {
            return db.DocFileSrcs
                .AsNoTracking()
                .Where(x => x.SrcGuid == srcGuid)
                .Join(db.VwDocfiles, s => s.Hash, f => f.DocfileHash, (s, f) => new DocfileSrcModel
                {
                    Cod = (DocfileModel.Cods)s.SrcCod,
                    Target = srcGuid,
                    Docfile = new DocfileModel
                    {
                        Hash = f.DocfileHash,
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
            .OrderByDescending(x => x.Docfile!.FchCreated)
            .ToList();
        }

        public static List<DocfileModel> FromTarget(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromTarget(db, target);
            }
        }

        public static List<DocfileModel> FromTarget(Entities.MaxiContext db, Guid target)
        {
            var retval = db.VwDownloadTargets
                .AsNoTracking()
                .Where(x => x.Target == target)
                .Select(y => new DocfileModel
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
                    VRes = y.DocfileVres
                })
                .OrderByDescending(x => x.Fch ?? x.FchCreated)
                .ToList();
            return retval;
        }



        public static List<DocfileModel> Fetch(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFileSrcs
                              join y in db.DocFiles on x.Hash equals y.Hash
                              where x.SrcGuid == target
                              orderby x.SrcOrd, y.Fch descending
                              select new DocfileModel()
                              {
                                  Hash = x.Hash,
                                  Nom = y.Nom,
                                  Fch = y.Fch,
                                  Document = new DTO.Media((Media.MimeCods)y.Mime, null),
                                  Thumbnail = new DTO.Media((Media.MimeCods)y.ThumbnailMime, null),
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
