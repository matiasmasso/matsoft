using Api.Entities;
using DTO;
using System.Linq;

namespace Api.Services
{
    public class DocFileService
    {
        private readonly MatGenContext _db;
        public DocFileService(MatGenContext db)
        {
            _db = db;
        }
        public DocfileModel? Find(string asin)
        {
                return _db.DocFiles
                    .Where(x => x.Hash == asin)
                    .Select(x => new DocfileModel
                    {
                        Hash = x.Hash,
                        Document = new Media((Media.MimeCods)x.StreamMime, null),
                        Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
                        Pags = x.Pags,
                        Size = x.Size,
                        FchCreated = x.FchCreated
                    }).FirstOrDefault();
        }
        public DocfileModel? FindBySha256(string sha256)
        {
                return _db.DocFiles
                    .Where(x => x.Sha256 == sha256)
                    .Select(x => new DocfileModel
                    {
                        Hash = x.Hash,
                        Document = new Media((Media.MimeCods)x.StreamMime, null),
                        Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
                        Pags = x.Pags,
                        Size = x.Size,
                        FchCreated = x.FchCreated
                    }).FirstOrDefault();
        }

        public  Media? Media(string asin)
        {
                return _db.DocFiles
                    .Where(x => x.Hash == asin)
                    .Select(x => new Media()
                    {
                        Data = x.Stream,
                        Mime = DTO.Media.MimeCods.Pdf
                    }).FirstOrDefault();
        }

        public  Media? Thumbnail(string asin)
        {
            Media? retval = null;
                retval = _db.DocFiles
                   .Where(x => x.Hash == asin)
                   .Select(x => new Media()
                   {
                       Data = x.Thumbnail,
                       Mime = DTO.Media.MimeCods.Jpg
                   }).FirstOrDefault();
            return retval;
        }

        public bool Update(DocfileModel value)
        {
                return Update(_db, value);
        }

        public static bool Update(Entities.MatGenContext db, DocfileModel value)
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(value.Hash))
            {
                Entities.DocFile? entity = db.DocFiles.Find(value.Hash);

                if (entity == null)
                {
                    entity = new Entities.DocFile();
                    entity.Hash = value.Hash;
                    db.DocFiles.Add(entity);
                }

                if(value.Sha256 != null) entity.Sha256 = value.Sha256;
                entity.Pags = value.Pags;
                entity.Size = (int?)value.Size;

                entity.StreamMime = value.Document == null ? 0 : (int)value.Document.Mime;
                if (value.Document?.Data != null) entity.Stream = value.Document?.Data;

                entity.ThumbnailMime = value.Thumbnail == null ? 0 : (int)value.Thumbnail.Mime;
                if (value.Thumbnail?.Data != null) entity.Thumbnail = value.Thumbnail?.Data;

                db.SaveChanges();
                retval = true;
            }
            return retval;
        }

        public  bool Delete(String asin)
        {
                var entity = _db.DocFiles.Remove(_db.DocFiles.Single(x => x.Hash.Equals(asin)));
                _db.SaveChanges();
            return true;

        }
    }

    public class DocFilesService
    {
        private readonly MatGenContext _db;
        public DocFilesService(MatGenContext db)
        {
            _db = db;
        }
        public List<DocfileModel> All()
        {
                return _db.DocFiles
                    .Select(x => new DocfileModel()
                    {
                        Hash = x.Hash,
                        Document = new Media((Media.MimeCods)x.StreamMime, null),
                        Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
                        Pags = x.Pags,
                        Size = x.Size,
                        FchCreated = x.FchCreated
                    }).ToList();
        }
    }
}
