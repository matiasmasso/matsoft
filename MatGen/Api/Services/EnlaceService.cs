using Api.Entities;
using DTO;

namespace Api.Services
{

    public class EnlaceService
    {
        private readonly MatGenContext _db;

        public EnlaceService(MatGenContext db)
        {
            _db = db;
        }

        public EnlaceModel? Find(Guid guid)
        {
            return _db.Enlaces
                .Where(x => x.Guid == guid)
                .Select(x => new EnlaceModel(x.Guid)
                {
                    Marit = x.Marit,
                    Muller = x.Muller,
                    NupciesMarit = x.NupciesMarit,
                    NupciesMuller = x.NupciesMuller,
                    FchLocation = new FchLocationModel(x.FchQualifier, x.Fch, x.Fch2, x.Cit)
                }).FirstOrDefault();
        }

        public bool Update(EnlaceModel value)
        {
            Entities.Enlace? entity;
            if (value.IsNew)
            {
                entity = new Entities.Enlace();
                _db.Enlaces.Add(entity);
                entity.Guid = value.Guid;
            }
            else
            {
                entity = _db.Enlaces.Find(value.Guid);
            }

            if (entity == null) throw new Exception("Enlace not found");

            entity.Marit = value.Marit;
            entity.Muller = value.Muller;
            entity.NupciesMarit = value.NupciesMarit ?? 0;
            entity.NupciesMuller = value.NupciesMuller ?? 0;

            entity.FchQualifier = value.FchLocation?.Fch?.Qualifier?.ToString();
            entity.Fch = value.FchLocation?.Fch?.Fch1?.ToString();
            entity.Fch2 = value.FchLocation?.Fch?.Fch2?.ToString();
            entity.Cit = value.FchLocation?.Location?.Guid;

            _db.SaveChanges();
            return true;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.Enlaces.Single(x => x.Guid.Equals(guid));
            _db.Enlaces.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }


    public class EnlacesService
    {

        private readonly MatGenContext _db;

        public EnlacesService(MatGenContext db)
        {
            _db = db;
        }

        public List<EnlaceModel> All()
        {
            return _db.VwEnlaces
                .Select(x => new EnlaceModel(x.Guid ?? Guid.NewGuid())
                {
                    Marit = x.Marit,
                    Muller = x.Muller,
                    NupciesMarit = x.NupciesMarit,
                    NupciesMuller = x.NupciesMuller,
                    FchLocation = new FchLocationModel(x.FchQualifier, x.Fch, x.Fch2, x.Cit),
                    IsNew = x.Guid == null
                }).ToList();

        }
    }
}
