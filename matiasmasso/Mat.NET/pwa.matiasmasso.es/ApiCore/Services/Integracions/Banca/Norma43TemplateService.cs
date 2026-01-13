using DTO;
using DTO.Integracions.Banca;
using Microsoft.EntityFrameworkCore;
using System;
namespace Api.Services.Integracions.Banca

{
    public class Norma43TemplateService
    {

        public static Norma43.Template? GetValue(EmpModel.EmpIds emp, string pattern)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.N43templates
                    .AsNoTracking()
                    .Where(x => x.Emp == (int)emp && string.Compare(x.Pattern, pattern, StringComparison.InvariantCultureIgnoreCase) == 0)
                    .Select(x => new Norma43.Template(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Pattern = x.Pattern,
                        Concepte = x.Concepte,
                        Cta = x.Cta,
                        Contact = x.Contact,
                        Projecte = x.Projecte
                    }).FirstOrDefault();
            }
        }

        public static Norma43.Template? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.N43templates
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new Norma43.Template(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Pattern = x.Pattern,
                        Concepte = x.Concepte,
                        Cta = x.Cta,
                        Contact = x.Contact,
                        Projecte = x.Projecte
                    }).FirstOrDefault();
            }
        }

        public static bool Update(Norma43.Template value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.N43template? entity;
                if (value.IsNew)
                {
                    entity = new Entities.N43template();
                    db.N43templates.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.N43templates.Find(value.Guid);

                if (entity == null) throw new Exception("Template not found");

                entity.Emp = (int)value.Emp!;
                entity.Pattern = value.Pattern ?? "";
                entity.Concepte = value.Concepte;
                entity.Cta = value.Cta;
                entity.Contact = value.Contact;
                entity.Projecte = value.Projecte;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.N43templates.Remove(db.N43templates.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }
    }

    public class Norma43TemplatesService
    {
        public static List<Norma43.Template> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.N43templates
                    .AsNoTracking()
                    .Select(x => new Norma43.Template(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Pattern = x.Pattern,
                        Concepte = x.Concepte,
                        Cta = x.Cta,
                        Contact = x.Contact,
                        Projecte = x.Projecte
                    }).ToList();
            }
        }
    }
}

