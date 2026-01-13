using DTO;

namespace Api.Services
{
    public class CountryService
    {
        public static CountryModel? Find(Guid guid)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.Countries
                    .Where(x => x.Guid == guid)
                    .Select(x => new CountryModel(x.Guid)
                    {
                        ISO = x.Iso,
                        Nom = new LangTextModel(x.NomEsp, x.NomCat, x.NomEng, x.NomPor),
                        Lang = new LangDTO(x.LangIso),
                        ExportCod= x.ExportCod,
                        PrefixeTelefonic= x.PrefixeTelefonic
                    }).FirstOrDefault();
            }
        }


        public static bool Update(CountryModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Country? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Country();
                    db.Countries.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Countries.Find(guid);

                if (entity == null) throw new Exception("Country not found");

                entity.NomEsp = value.Nom?.Esp ?? "";
                entity.NomCat = value.Nom?.Cat ?? "";
                entity.NomEng = value.Nom?.Eng ?? "";
                entity.NomPor = value.Nom?.Por ?? "";
                entity.Iso = value.ISO ?? "";
                entity.LangIso = value.Lang?.Id.ToString();
                entity.ExportCod = (short)value.ExportCod;
                entity.PrefixeTelefonic = value.PrefixeTelefonic;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Countries.Find(guid);
                if (entity != null)
                {
                    db.Countries.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class CountriesService
    {
        public static List<CountryModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }

        public static List<CountryModel> GetValues(Entities.MaxiContext db)
        {
            return db.Countries
                .OrderBy(x => x.NomEsp)
                .Select(x => new CountryModel(x.Guid)
                {
                    Nom = new LangTextModel(x.NomEsp, x.NomCat, x.NomEng, x.NomPor),
                    ISO = x.Iso,
                    Lang = new LangDTO(x.LangIso),
                    ExportCod = x.ExportCod,
                    PrefixeTelefonic = x.PrefixeTelefonic
                }).ToList();
        }
    }
}
