using Api.Entities;
using DTO;

namespace Api.Services
{
    public class CognomService
    {
        private readonly MatGenContext _db;
        public CognomService(MatGenContext db)
        {
            _db = db;
        }

        public CognomModel? Find(Guid guid)
        {
                return _db.Cognoms
                    .Where(x => x.Guid == guid)
                    .Select(x => new CognomModel(x.Guid)
                    {
                        Nom = x.Nom,
                    }).FirstOrDefault();
        }

        public  bool Update(CognomModel value)
        {
                Entities.Cognom? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Cognom();
                    _db.Cognoms.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.Cognoms.Where(x=>x.Guid == value.Guid).FirstOrDefault();

                if (entity == null) throw new Exception("Cognom not found");

                entity.Nom = value.Nom ?? "";

                // Save changes in database
                _db.SaveChanges();
                return true;
            }

        public  bool Delete(Guid guid)
        {
                var entity = _db.Cognoms.Remove(_db.Cognoms.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }


    }
    public class CognomsService
    {
        private readonly MatGenContext _db;
        public CognomsService(MatGenContext db)
        {
            _db = db;
        }

        public  List<CognomModel> All()
        {

                return _db.Cognoms
                    .OrderBy(x => x.Nom)
                    .Select(x => new CognomModel(x.Guid)
                    {
                        Nom = x.Nom,
                    }).ToList();
        }

    }
}
