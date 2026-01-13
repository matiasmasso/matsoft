using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class LocationService
    {
        public static LocationModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Locations
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new LocationModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Zona = x.Zona

                    }).FirstOrDefault();
            }
        }


        public static LocationModel? FromName(string countryIso, string locationName)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Locations
                        .AsNoTracking()
                    .Where(x => x.Nom.ToLower() == locationName.ToLower() && x.ZonaNavigation.CountryNavigation.Iso == countryIso)
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


        public static List<Guid> Contacts(Guid location)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.CliAdrs
                        .AsNoTracking()
                    .Where(x => db.Zips.Any(y => y.Guid == x.Zip && y.Location == location))
                    .Select(x => x.SrcGuid)
                    .ToList();
            }
        }

    }



    public class LocationsService
    {
        public static List<LocationModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<LocationModel> GetValues(Entities.MaxiContext db)
        {
            return db.Locations
                        .AsNoTracking()
                            .OrderBy(x => x.Nom)
                            .Select(x => new LocationModel(x.Guid)
                            {
                                Zona = x.Zona,
                                Nom = x.Nom
                            }).ToList();
        }
    }

}
