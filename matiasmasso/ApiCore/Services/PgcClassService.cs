using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class PgcClassService
    {

        public static PgcClassModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PgcClasses
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new PgcClassModel(x.Guid)
                    {
                        Parent = x.Parent,
                        Cod = (PgcClassModel.Cods?)x.Cod,
                        Plan = x.Plan,
                        Ord = x.Ord,
                        HideFigures = x.HideFigures
                    }).FirstOrDefault();
            }
        }

        public static bool Update(PgcClassModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.PgcClass? entity;
                if (value.IsNew)
                {
                    entity = new Entities.PgcClass();
                    db.PgcClasses.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.PgcClasses.Find(value.Guid);

                if (entity == null) throw new System.Exception("PgcClass not found");

                entity.Parent = value.Parent;
                entity.Cod = (int?)value.Cod;
                entity.Plan = value.Plan;
                entity.Ord = value.Ord;
                entity.HideFigures = value.HideFigures;
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.PgcClasses.Remove(db.PgcClasses.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class PgcClasssService
    {
        public static List<PgcClassModel> GetValues(Guid plan)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PgcClasses
                    .AsNoTracking()
                    .Where(x=>x.Plan == plan)
                    .OrderBy(x=>x.Ord)
                    .Select(x => new PgcClassModel(x.Guid)
                    {
                        Parent = x.Parent,
                        Cod = (PgcClassModel.Cods?)x.Cod,
                        Plan = x.Plan,
                        Ord = x.Ord,
                        HideFigures = x.HideFigures
                    }).ToList();
            }
        }
    }
}

