using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class TemplateService
    {

        public static TemplateModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Templates
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new TemplateModel(x.Guid)
                    {
                        Nom = x.Nom
                    }).FirstOrDefault();
            }
        }

        public static bool Update(TemplateModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Template? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Template();
                    db.Templates.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Templates.Find(value.Guid);

                if (entity == null) throw new Exception("Template not found");

                entity.Nom = value.Nom;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Templates.Remove(db.Templates.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class TemplatesService
    {
        public static List<TemplateModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Templates
                    .AsNoTracking()
                    .Select(x => new TemplateModel(x.Guid)
                    {
                        Nom = x.Nom
                    }).ToList();
            }
        }
    }
}
