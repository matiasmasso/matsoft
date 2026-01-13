using Api.Entities;
using DTO;

namespace Api.Services
{
    public class FirstnomService
    {
        private readonly MatGenContext _db;
        public FirstnomService(MatGenContext db)
        {
            _db = db;
        }
        public FirstnomModel? Find(Guid guid)
        {
            using (var _db = new Entities.MatGenContext())
            {
                return _db.Firstnoms
                    .Where(x => x.Guid == guid)
                    .Select(x => new FirstnomModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Sex = (PersonModel.Sexs)x.Sex
                    }).FirstOrDefault();
            }
        }

        public bool Update(FirstnomModel value)
        {
            Entities.Firstnom? entity;
            if (value.IsNew)
            {
                entity = new Entities.Firstnom();
                _db.Firstnoms.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = _db.Firstnoms.Find(value.Guid);

            if (entity == null) throw new Exception("Firstnom not found");

            entity.Nom = value.Nom ?? "";
            entity.Sex = (short)value.Sex;

            // Save changes in database
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.Firstnoms.Remove(_db.Firstnoms.Single(x => x.Guid.Equals(guid)));
            _db.SaveChanges();
            return true;

        }


    }
    public class FirstnomsService
    {
        private readonly MatGenContext _db;
        public FirstnomsService(MatGenContext db)
        {
            _db = db;
        }
        public List<FirstnomModel> All()
        {

            return _db.Firstnoms
                .OrderBy(x => x.Nom)
                .Select(x => new FirstnomModel(x.Guid)
                {
                    Nom = x.Nom,
                    Sex = (PersonModel.Sexs)x.Sex
                }).ToList();
        }

    }
}
