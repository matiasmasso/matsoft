using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{

    public class TaxService
    {
        public static TaxModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValue(db, guid);
            }
        }
        public static TaxModel? GetValue(Entities.MaxiContext db, Guid guid)
        {
            return db.Taxes
                        .AsNoTracking()
                .Where(x => x.Guid == guid)
                            .Select(x => new TaxModel(x.Guid)
                            {
                                Codi = (TaxModel.Codis)x.Codi,
                                Fch = x.Fch,
                                Tipus = x.Tipus
                            }).FirstOrDefault();
        }

        public static bool Update(TaxModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Tax? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Tax();
                    db.Taxes.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Taxes.Find(db, value.Guid);

                if (entity == null) throw new Exception("Tax not found");

                entity.Fch = value.Fch;
                entity.Codi = (int)value.Codi;
                entity.Tipus = value.Tipus;

                // Save changes in database
                db.SaveChanges();
                return true;

            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Taxes.Remove(db.Taxes.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }
    }
    public class TaxesService
    {
        public static List<TaxModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<TaxModel> GetValues(Entities.MaxiContext db)
        {
            return db.Taxes
                         .AsNoTracking()
                           .OrderByDescending(x => x.Fch)
                            .Select(x => new TaxModel(x.Guid)
                            {
                                Codi = (TaxModel.Codis)x.Codi,
                                Fch = x.Fch,
                                Tipus= x.Tipus
                            }).ToList();
        }
    }
}
