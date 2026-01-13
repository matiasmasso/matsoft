using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class CcaSchedService
    {

        public static CcaSchedModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CcaScheds
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new CcaSchedModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Concept = x.Concept,
                        Ccd = (CcaModel.CcdEnum?)x.Ccd,
                        Projecte = x.Projecte,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        LastTime = x.LastTime,
                        FreqMod = (CcaSchedModel.FreqMods?)x.FreqMod,
                        FreqDue = x.FreqDue,
                        Items = x.CcaSchedItems.Select(y => new CcaSchedModel.Item(y.Guid)
                        {
                            Cta = y.Cta,
                            Contact = y.Contact,
                            Eur = y.Eur,
                            Dh = (CcaModel.Item.DhEnum)y.Dh
                        }).ToList()
                    }).FirstOrDefault();
            }
        }

        public static bool Update(CcaSchedModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.CcaSched? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CcaSched();
                    db.CcaScheds.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.CcaScheds
                        .Include(x => x.CcaSchedItems)
                        .First(x => x.Guid == value.Guid);

                if (entity == null) throw new Exception("CcaSched not found");

                entity.Emp = (int)value.Emp;
                entity.Concept = value.Concept;
                entity.Ccd = (int?)value.Ccd;
                entity.Projecte = value.Projecte;
                entity.FchFrom = value.FchFrom;
                entity.FchTo = value.FchTo;
                entity.LastTime = value.LastTime;
                entity.FreqMod = (int?)value.FreqMod;
                entity.FreqDue = value.FreqDue;

                db.SaveChanges();

                entity.CcaSchedItems = value.Items.Select(x => new Entities.CcaSchedItem()
                {
                    Lin = value.Items.IndexOf(x) + 1,
                    Cta = x.Cta,
                    Contact = x.Contact,
                    Eur = (decimal)x.Eur!,
                    Dh = (byte)x.Dh
                }).ToList();

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.CcaSchedItems.RemoveRange(db.CcaSchedItems.Where(x => x.Parent == guid));
                db.CcaScheds.Remove(db.CcaScheds.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class CcaSchedsService
    {
        public static List<CcaSchedModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<CcaSchedModel> GetValues(Entities.MaxiContext db)
        {
            return db.CcaScheds
                .AsNoTracking()
                //.Include(x=>x.CcaSchedItems)
                .Select(x => new CcaSchedModel(x.Guid)
                {
                    Emp = (EmpModel.EmpIds)x.Emp,
                    Concept = x.Concept,
                    Ccd = (CcaModel.CcdEnum?)x.Ccd,
                    Projecte = x.Projecte,
                    FchFrom = x.FchFrom,
                    FchTo = x.FchTo,
                    LastTime = x.LastTime,
                    FreqMod = (CcaSchedModel.FreqMods?)x.FreqMod,
                    FreqDue = x.FreqDue,
                    Items = x.CcaSchedItems.Select(y => new CcaSchedModel.Item(y.Guid)
                    {
                        Cta = y.Cta,
                        Contact = y.Contact,
                        Eur = y.Eur,
                        Dh = (CcaModel.Item.DhEnum)y.Dh
                    }).ToList()
                }).ToList();
        }

        public static bool ExecuteDueScheds(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var allScheds = GetValues(db);
                var dueScheds = allScheds.Where(x => x.IsDue()).ToList();
                foreach(var value in  dueScheds)
                {
                    var cca = value.Cca(user);
                    cca.Fch = value.DueDate();
                    CcaService.Update(db,cca);

                    Entities.CcaSched? entity =  db.CcaScheds.Find(value.Guid);
                    entity!.LastTime = cca.Fch.ToDateTime();
                    db.SaveChanges();
                }
                return true;
            }

        }

    }
}

