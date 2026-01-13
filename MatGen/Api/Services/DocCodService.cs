using Api.Entities;
using DTO;

namespace Api.Services
{
    public class DocCodService
    {
        private readonly MatGenContext _db;
        public DocCodService(MatGenContext db)
        {
            _db = db;
        }

        public DocCodModel? Find(Guid guid)
        {
            return _db.DocCods
                .Where(x => x.Guid == guid)
                .Select(x => new DocCodModel(x.Guid)
                {
                    Nom = x.Nom,
                    Ord = x.Ord,
                    Id = x.Id
                }).FirstOrDefault();
        }

        public bool Update(DocCodModel value)
        {
            Entities.DocCod? entity;
            if (value.IsNew)
            {
                entity = new Entities.DocCod();
                _db.DocCods.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = _db.DocCods.Find(value.Guid);

            if (entity == null) throw new Exception("DocCod not found");

            entity.Nom = value.Nom ?? "";
            entity.Ord = value.Ord ?? "";
            entity.Id = value.Id;

            // Save changes in database
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.DocCods.Remove(_db.DocCods.Single(x => x.Guid.Equals(guid)));
            _db.SaveChanges();
            return true;

        }


    }
    public class DocCodsService
    {
        private readonly MatGenContext _db;
        public DocCodsService(MatGenContext db)
        {
            _db = db;
        }

        public List<DocCodModel> All()
        {

            return _db.DocCods
                .OrderBy(x => x.Nom)
                .Select(x => new DocCodModel(x.Guid)
                {
                    Nom = x.Nom,
                    Ord = x.Ord,
                    Id = x.Id
                }).ToList();
        }

    }
}
