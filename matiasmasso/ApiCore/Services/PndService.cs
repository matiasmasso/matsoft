using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class PndService
    {

        public static PndModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Pnds
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new PndModel(x.Guid)
                    {
                        //Nom = x.Nom
                    }).FirstOrDefault();
            }
        }

        public static bool Update(PndModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db, value);
            }
        }
        public static bool Update(Entities.MaxiContext db, PndModel value)
        {
            Entities.Pnd? entity;
            if (value.IsNew)
            {
                entity = new Entities.Pnd();
                db.Pnds.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = db.Pnds.Find(value.Guid);

            if (entity == null) throw new Exception("Pnd not found");

            entity.Emp = (short)value.Emp;
            entity.ContactGuid = value.Contact;
            entity.CcaGuid = value.Cca;
            entity.CtaGuid = value.Cta;
            entity.Fch = value.Fch;
            entity.Vto = value.Vto;
            entity.Eur = value.Eur;
            entity.Fra = value.Fra;
            entity.Fpg = value.Fpg;
            entity.Cfp = (short)value.Cfp;
            entity.Ad = value.AD == PndModel.ADs.Deutor ? "D" : "A";
            entity.Status = (short)value.Status;
            entity.StatusGuid = value.Result;

            db.SaveChanges();
            return true;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Pnds.Remove(db.Pnds.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class PndsService
    {
        public static List<PndModel> PendingValues(Guid target)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Pnds
                    .AsNoTracking()
                    .Where(x => x.ContactGuid == target && x.Status == (int)PndModel.Statuses.pendent)
                    .Select(x => new PndModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Contact = x.ContactGuid,
                        Cca = x.CcaGuid,
                        Cta = x.CtaGuid,
                        Fch = x.Fch,
                        Vto = x.Vto,
                        Eur = x.Eur,
                        Fra = x.Fra,
                        Fpg = x.Fpg,
                        Cfp = (PndModel.Cfps)x.Cfp,
                        AD = x.Ad == "D" ? PndModel.ADs.Deutor : PndModel.ADs.Creditor,
                        Status = (PndModel.Statuses)x.Status,
                        Result = x.StatusGuid
                    }).ToList();
            }
        }
    }
}
