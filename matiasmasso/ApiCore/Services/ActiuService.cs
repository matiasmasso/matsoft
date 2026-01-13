using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ActiuService
    {

        public static ActiuModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Actius
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ActiuModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Cta = x.Cta
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ActiuModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Actiu? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Actiu();
                    db.Actius.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Actius.Find(value.Guid);

                if (entity == null) throw new Exception("Actiu not found");

                entity.Nom = value.Nom;
                entity.Cta = value.Cta;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Actius.Remove(db.Actius.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class ActiusService
    {
        public static List<ActiuModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Actius
                    .AsNoTracking()
                    .Select(x => new ActiuModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Cta = x.Cta
                    }).ToList();
            }
        }
    }
}
