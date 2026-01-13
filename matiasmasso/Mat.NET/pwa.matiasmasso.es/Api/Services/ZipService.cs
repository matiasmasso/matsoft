using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Services
{
    public class ZipService
    {
        public static ZipModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Zips
                    .Where(x => x.Guid == guid)
                    .Select(x => new ZipModel(x.Guid)
                    {
                        ZipCod = x.ZipCod,
                        Location = x.Location

                    }).FirstOrDefault();
            }
        }

        public static GuidNom? Lookup(string countryIso, string zipcod, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwZips
                    .Where(x => x.CountryIso == countryIso && x.ZipCod == zipcod)
                    .Select(x => new GuidNom
                    {
                        Guid = (Guid)x.ZipGuid!,
                        Nom = ZipModel.FullNom(x.ZipCod, x.LocationNom, x.ZonaNom, x.ProvinciaNom, x.CountryIso, lang.Tradueix(x.CountryEsp, x.CountryCat, x.CountryEng, x.CountryPor))
                    }).FirstOrDefault();
            }
        }

        public static GuidNom? Residence(Guid zipGuid, LangDTO? lang = null)
        {
            if (lang == null) lang = LangDTO.Esp();
            using (var db = new Entities.MaxiContext())
            {
                return db.VwZips
                    .Where(x => x.ZipGuid == zipGuid)
                    .Select(x => new GuidNom
                    {
                        Guid = (Guid)x.ZipGuid!,
                        Nom = ZipModel.FullNom(x.ZipCod, x.LocationNom, x.ZonaNom, x.ProvinciaNom, x.CountryIso, lang.Tradueix(x.CountryEsp, x.CountryCat, x.CountryEng, x.CountryPor))
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ZipModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db,value);
            }
        }
        public static bool Update(Entities.MaxiContext db, ZipModel value)
        {
            var guid = value.Guid;
            Entities.Zip? entity;
            if (value.IsNew)
            {
                entity = new Entities.Zip();
                db.Zips.Add(entity);
                entity.Guid = guid;
            }
            else
                entity = db.Zips.Find(guid);

            if (entity == null) throw new Exception("Zip not found");

            entity.ZipCod = value.ZipCod ?? "";
            entity.Location = value.Location;

            db.SaveChanges();
            return true;
        }

        public static ZipModel? UpdateFromGeocode(GoogleApi.GeoCodeModel geocode)
        {
            var cache = CacheService.Request(CacheDTO.Table.TableIds.Country, CacheDTO.Table.TableIds.Zona, CacheDTO.Table.TableIds.Provincia, CacheDTO.Table.TableIds.Location, CacheDTO.Table.TableIds.Zip);
            var country = cache.Countries.FirstOrDefault(x => x.ISO == geocode.CountryIso());
            ZonaModel? zona = null;
            LocationModel? location = null;
            ZipModel? retval = null;
            if (country != null)
            {
                zona = cache.Zonas.FirstOrDefault(x => x.Nom == geocode.Zona() && x.Country == country.Guid);
                if (zona == null)
                {
                    var regions = cache.Regions.Where(x => x.Country == country.Guid).ToList();
                    var provincias = cache.Provincias.Where(x => regions.Any(y => x.Region == y.Guid)).ToList();
                    var provincia = provincias.Where(x => x.Nom == geocode.Zona()).FirstOrDefault();
                    if (provincia != null)
                    {
                        var zonas = cache.Zonas.Where(x => x.Provincia == provincia!.Guid).ToList();
                        location = cache.Locations.FirstOrDefault(x => zonas.Any(y => x.Zona == y.Guid) && x.Nom == geocode.Location());
                    }
                }
                else
                {
                    location = cache.Locations.FirstOrDefault(x => x.Zona == zona.Guid && x.Nom == geocode.Location());
                }

                using (var db = new Entities.MaxiContext())
                {
                    if (location == null)
                    {
                        if (zona == null)
                        {
                            zona = ZonaModel.Factory(country, geocode.Zona());
                            ZonaService.Update(db, zona);
                        }
                        location = LocationModel.Factory(zona, geocode.Location());
                        LocationService.Update(db, location);
                    }
                    var zip = ZipModel.Factory(location, geocode.ZipCod());
                    if( ZipService.Update(db, zip)) retval = zip;
                }
            }
            return retval;
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Zips.Find(guid);
                if (entity != null)
                {
                    db.Zips.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class ZipsService
    {
        public static List<ZipModel> All(Entities.MaxiContext db)
        {
            return db.Zips
                            .Select(x => new ZipModel(x.Guid)
                            {
                                Location = x.Location,
                                ZipCod = x.ZipCod
                            }).ToList();
        }

    }
}
