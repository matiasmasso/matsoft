using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ProjecteService
    {

        public static ProjecteModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Projectes
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ProjecteModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds?)x.Emp,
                        Nom = x.Nom
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ProjecteModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Projecte? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Projecte();
                    db.Projectes.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Projectes.Find(value.Guid);

                if (entity == null) throw new Exception("Project not found");

                entity.Emp = (int?)value.Emp;
                entity.Nom = value.Nom ?? "";

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Projectes.Remove(db.Projectes.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class ProjectsService
    {
        public static List<ProjecteModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Projectes
                    .AsNoTracking()
                    .Select(x => new ProjecteModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds?)x.Emp,
                        Nom = x.Nom
                    }).ToList();
            }
        }
    }
}
