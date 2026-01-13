using Api.Entities;
using DTO;

namespace Api.Services
{
    public class LocationService
    {
        private readonly MatGenContext _db;
        public LocationService(MatGenContext db)
        {
            _db = db;
        }
        public LocationModel? Find(Guid guid)
        {
                return _db.Locations
                    .Where(x => x.Guid == guid)
                    .Select(x => new LocationModel(x.Guid)
                    {
                        Parent = x.Parent,
                        Nom = x.Nom,
                        NomLlarg = x.NomLlarg
                    }).FirstOrDefault();
        }

        public  bool Update(LocationModel value)
        {
                Entities.Location? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Location();
                    _db.Locations.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.Locations.Find(value.Guid);

                if (entity == null) throw new Exception("Location not found");

                entity.Parent = value.Parent;
                entity.Nom = value.Nom;
                entity.NomLlarg = value.NomLlarg;

                // Save changes in database
                _db.SaveChanges();
                return true;
        }

        public  bool Delete(Guid guid)
        {
                var entity = _db.Locations.Remove(_db.Locations.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }

    }
    public class LocationsService
    {
        private readonly MatGenContext _db;
        public LocationsService(MatGenContext db)
        {
            _db = db;
        }
        public List<LocationModel> All()
        {
 
                return _db.Locations
                    .OrderBy(x => x.Nom)
                    .Select(x => new LocationModel(x.Guid)
                    {
                        Parent = x.Parent,
                        Nom = x.Nom,
                        NomLlarg = x.NomLlarg
                    }).ToList();
        }

    }
}
