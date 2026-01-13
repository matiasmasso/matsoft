using Api.Entities;
using DTO;

namespace Api.Services
{
    public class PubService
    {
        private readonly MatGenContext _db;
        public PubService(MatGenContext db)
        {
            _db = db;
        }
        public PubModel? Find(Guid guid)
        {
            return _db.Pubs
                .Where(x => x.Guid == guid)
                .Select(x => new PubModel(x.Guid)
                {
                    Nom = x.Nom ?? "",
                    FchCreated = x.FchCreated,
                    Docfile = x.Hash == null ? null : new DocfileModel
                    {
                        Hash = x.Hash
                    }
                }).FirstOrDefault();
        }

        public Media? Media(Guid guid)
        {
            return _db.Pubs
                .Where(x => x.Guid == guid && x.HashNavigation != null)
                .Select(x => new Media()
                {
                    Data = x.HashNavigation!.Stream,
                    Mime = (Media.MimeCods)x.HashNavigation.StreamMime
                }).FirstOrDefault();
        }
        public Media? Thumbnail(Guid guid)
        {
            var retval = _db.Pubs
                .Where(x => x.Guid == guid && x.HashNavigation != null)
                .Select(x => new Media()
                {
                    Data = x.HashNavigation!.Thumbnail,
                    Mime = (Media.MimeCods)x.HashNavigation.ThumbnailMime
                }).FirstOrDefault();
            return retval;
        }

        public bool Update(PubModel value)
        {
            bool retval = false;
            var entity = _db.Pubs.Find(value.Guid);
            if (entity == null)
            {
                entity = new Entities.Pub();
                entity.Guid = value.Guid;
                _db.Pubs.Add(entity);
            }

            entity.Nom = value.Nom;
            entity.FchCreated = value.FchCreated;

            if (value.Docfile != null)
            {
                entity.Hash = value.Docfile.Hash;
                DocFileService.Update(_db, value.Docfile);
            }

            _db.SaveChanges();
            retval = true;
            return retval;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.Pubs.Remove(_db.Pubs.Single(x => x.Guid.Equals(guid)));
            _db.SaveChanges();
            return true;
        }


    }
    public class PubsService
    {
        private readonly MatGenContext _db;
        public PubsService(MatGenContext db)
        {
            _db = db;
        }
        public List<PubModel> All()
        {

            return _db.Pubs
                .OrderBy(x => x.Nom)
                .Select(x => new PubModel(x.Guid)
                {
                    Nom = x.Nom ?? "",
                    FchCreated = x.FchCreated,
                    Docfile = x.Hash == null ? null : new DocfileModel
                    {
                        Hash = x.Hash
                    }
                }).ToList();
        }

    }
}
