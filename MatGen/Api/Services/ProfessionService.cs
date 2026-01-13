using Api.Entities;
using DTO;

namespace Api.Services
{
    public class ProfessionService
    {
        private readonly MatGenContext _db;
        public ProfessionService(MatGenContext db)
        {
            _db = db;
        }
        public  ProfessionModel? Find(Guid guid)
        {
                 return _db.Professions
                    .Where(x => x.Guid == guid)
                    .Select(x => new ProfessionModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Llati = x.Llati,
                        Obs = x.Obs
                    }).FirstOrDefault();
        }

        public  bool Update(ProfessionModel value)
        {
                Entities.Profession? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Profession();
                    _db.Professions.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.Professions.Find(value.Guid);

                if (entity == null) throw new Exception("Profession not found");

                entity.Nom = value.Nom ?? "";
                entity.Llati = value.Llati ?? "";
                entity.Obs = value.Obs;

                // Save changes in database
                    _db.SaveChanges();
                return true;
        }

        public  bool Delete(Guid guid)
        {
                var entity = _db.Professions.Remove(_db.Professions.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }

    }
    public class ProfessionsService
    {
        private readonly MatGenContext _db;
        public ProfessionsService(MatGenContext db)
        {
            _db = db;
        }   
        public  List<ProfessionModel> All()
        {
 
                return _db.Professions
                    .OrderBy(x => x.Nom)
                    .Select(x => new ProfessionModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Llati = x.Llati,
                        Obs = x.Obs
                    }).ToList();
        }

    }
}
