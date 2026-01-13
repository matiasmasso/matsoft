using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class RegionService
    {
        public static RegionModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Regios
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new RegionModel(x.Guid)
                    {
                        Country = x.Country,
                        Nom = x.Nom

                    }).FirstOrDefault();
            }
        }


        public static bool Update(RegionModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Regio? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Regio();
                    db.Regios.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Regios.Find(guid);

                if (entity == null) throw new Exception("Region not found");

                entity.Nom = value.Nom ?? "";
                entity.Country = value.Country;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Regios.Find(guid);
                if (entity != null)
                {
                    db.Regios.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class RegionsService
    {
        public static List<RegionModel> All(Entities.MaxiContext db)
        {
            return db.Regios
                        .AsNoTracking()
                            .OrderBy(x => x.Nom)
                            .Select(x => new RegionModel(x.Guid)
                            {
                                Country = x.Country,
                                Nom = x.Nom,
                            }).ToList();
        }
    }

}
