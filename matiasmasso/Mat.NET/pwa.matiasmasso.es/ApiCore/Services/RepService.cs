using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class RepService
    {
        public static RepModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.CliReps
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Join(db.CliGrals, rep => rep.Guid, contact => contact.Guid, (rep, contact) => new RepModel(rep.Guid)
                    {
                        Nom = contact.RaoSocial,
                        Abr = rep.Abr,
                        Nif = contact.Nif,
                        Description = rep.Description,
                        FchFrom = rep.Desde,
                        FchTo = rep.Hasta
                    }).FirstOrDefault();

                if (retval != null)
                {
                    retval.Address = ContactService.Address(db, guid);
                    retval.Telefons = ContactService.Telefons(db, guid);
                    //retval.Emails = ContactService.Emails(db, guid);
                    retval.Iban = IbanService.Get(db, guid, IbanModel.Cods.proveidor);
                }
                return retval;
            }
        }

        public static bool Update(RepModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.CliRep? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CliRep();
                    db.CliReps.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.CliReps.Find(guid);

                if (entity == null) throw new Exception("Rep not found");

                entity.Abr = value.Abr;
                entity.Description = value.Description;
                entity.Desde = value.FchFrom;
                entity.Hasta = value.FchTo;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.CliReps.Find(guid);
                if (entity != null)
                {
                    db.CliReps.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }
    }
    public class RepsService
    {
        public static List<RepModel> All(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.CliReps
                         .AsNoTracking()
                  . Join(db.CliGrals, r => r.Guid, c => c.Guid, (r, c) => new { 
                        Emp = c.Emp,
                        Guid = r.Guid,
                        Nom = c.RaoSocial,
                        Abr = r.Abr,
                        Description = r.Description,
                        FchFrom = r.Desde,
                        FchTo = r.Hasta
                    }).Where(x => (int)x.Emp == emp).Select(x => new RepModel {
                        Guid = x.Guid,
                        Abr = x.Abr,
                        Nom = x.Nom,
                        Description = x.Description,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo
                    })
                    .OrderBy(y => y.Abr)
                    .ToList();
                return retval;
            }
        }
    }
}
