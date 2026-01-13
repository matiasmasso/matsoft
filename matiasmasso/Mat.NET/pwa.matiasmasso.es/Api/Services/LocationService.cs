using DocumentFormat.OpenXml.Vml.Office;
using DTO;

namespace Api.Services
{
    public class LocationService
    {
        public static LocationModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Locations
                    .Where(x => x.Guid == guid)
                    .Select(x => new LocationModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Zona = x.Zona

                    }).FirstOrDefault();
            }
        }


        public static bool Update(LocationModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db,value);
            }
        }

        public static bool Update(Entities.MaxiContext db, LocationModel value)
        {
                var guid = value.Guid;
                Entities.Location? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Location();
                    db.Locations.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Locations.Find(guid);

                if (entity == null) throw new Exception("Location not found");

                entity.Nom = value.Nom ?? "";
                entity.Zona = value.Zona;

                db.SaveChanges();
                return true;
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Locations.Find(guid);
                if (entity != null)
                {
                    db.Locations.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class LocationsService
    {
        public static List<LocationModel> All(Entities.MaxiContext db)
        {
            return db.Locations
                            .OrderBy(x => x.Nom)
                            .Select(x => new LocationModel(x.Guid)
                            {
                                Zona = x.Zona,
                                Nom = x.Nom
                            }).ToList();
        }

    }
}
