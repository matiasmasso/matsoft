using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProvinciaService
    {
        public static ProvinciaModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Provincia
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ProvinciaModel(x.Guid)
                    {
                        Region = x.Regio,
                        Nom = x.Nom,
                        Mod347 = x.Mod347,
                        Intrastat = x.Intrastat

                    }).FirstOrDefault();
            }
        }


        public static bool Update(ProvinciaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Provincium? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Provincium();
                    db.Provincia.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Provincia.Find(guid);

                if (entity == null) throw new Exception("Provincia not found");

                entity.Nom = value.Nom ?? "";
                entity.Regio = value.Region;
                entity.Mod347 = value.Mod347;
                entity.Intrastat = value.Intrastat;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Provincia.Find(guid);
                if (entity != null)
                {
                    db.Provincia.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class ProvinciasService
    {

        public static List<ProvinciaModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db);
            }
        }

        public static List<ProvinciaModel> All(Entities.MaxiContext db)
        {
            return db.Provincia
                        .AsNoTracking()
                            .OrderBy(x => x.Nom)
                            .Select(x => new ProvinciaModel(x.Guid)
                            {
                                Region = x.Regio == null ? null : (Guid)x.Regio,
                                Nom = x.Nom,
                                Mod347 = x.Mod347,
                                Intrastat = x.Intrastat
                            }).ToList();
        }
    }
}
