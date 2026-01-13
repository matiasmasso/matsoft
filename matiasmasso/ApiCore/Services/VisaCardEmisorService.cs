using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class VisaCardEmisorService
    {

        public static GuidNom? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VisaEmisors
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new GuidNom
                    {
                        Guid = x.Guid,
                        Nom = x.Nom
                    })
                    .FirstOrDefault();
            }
        }

        public static bool Update(GuidNom value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.VisaEmisor? entity;
                if (value.IsNew)
                {
                    entity = new Entities.VisaEmisor();
                    db.VisaEmisors.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.VisaEmisors.Find(value.Guid);

                if (entity == null) throw new Exception("VisaCardEmisor not found");

                entity.Nom = value.Nom ?? "";

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.VisaEmisors.Remove(db.VisaEmisors.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class VisaCardEmisorsService
    {
        public static List<GuidNom> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VisaEmisors
                    .AsNoTracking()
                    .Select(x => new GuidNom
                    {
                        Guid = x.Guid,
                        Nom = x.Nom
                    })
                    .OrderBy(x => x.Nom)
                    .ToList();
            }
        }
    }
}
