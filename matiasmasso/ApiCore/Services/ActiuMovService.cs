
using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ActiuMovService
    {

        public static ActiuModel.Mov? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ActiuMovs
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ActiuModel.Mov(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Actiu = x.Actiu,
                        Fch= x.Fch,
                        Contraparte= x.Contraparte,
                        Qty= x.Qty,
                        Eur= x.Eur
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ActiuModel.Mov value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.ActiuMov? entity;
                if (value.IsNew)
                {
                    entity = new Entities.ActiuMov();
                    db.ActiuMovs.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.ActiuMovs.Find(value.Guid);

                if (entity == null) throw new System.Exception("Actiu not found");

                entity.Emp = (int)value.Emp;
                entity.Actiu = value.Actiu;
                entity.Fch = value.Fch;
                entity.Contraparte = value.Contraparte;
                entity.Qty   = value.Qty; 
                entity.Eur = value.Eur;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.ActiuMovs.Remove(db.ActiuMovs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class ActiuMovsService
    {
        public static List<ActiuModel.Mov> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ActiuMovs
                    .AsNoTracking()
                    .Select(x => new ActiuModel.Mov(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Actiu = x.Actiu,
                        Fch = x.Fch,
                        Contraparte = x.Contraparte,
                        Qty = x.Qty,
                        Eur = x.Eur
                    }).ToList();
            }
        }
    }
}
