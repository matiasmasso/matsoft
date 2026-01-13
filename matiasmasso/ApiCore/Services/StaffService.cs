using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class StaffService
    {
        public static StaffModel? Find(Guid? guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                StaffModel? retval = null;
                if (guid != null)
                    retval = Find(db, (Guid)guid);
                return retval;
            }
        }

        public static StaffModel? Find(Entities.MaxiContext db, Guid guid)
        {
            var retval = db.VwStaffs
                         .AsNoTracking()
               .Where(x => x.Guid == guid)
                .Select(x => new StaffModel(x.Guid)
                {
                    Nom = x.RaoSocial,
                    Abr = x.Abr,
                    Nif = x.Nif,
                    NumSS = x.NumSs,
                    FchFrom = x.Alta,
                    FchTo = x.Baja,
                    Birthday = x.Nac,
                    Position = new StaffModel.StaffPos((Guid)x.StaffPos!, x.StaffPosNomEsp, x.StaffPosNomCat, x.StaffPosNomEng, x.StaffPosNomPor)
                })
                .FirstOrDefault();

            if (retval != null)
            {
                retval.Address = ContactService.Address(db, guid);
                retval.Telefons = ContactService.Telefons(db, guid);
                retval.Emails = ContactService.Emails(db, guid);
                retval.Iban = IbanService.Get(db, guid, IbanModel.Cods.staff);
            }

            return retval;

        }


        public static bool Update(StaffModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.CliStaff? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CliStaff();
                    db.CliStaffs.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.CliStaffs.Find(guid);

                if (entity == null) throw new System.Exception("Staff not found");

                entity.Abr = value.Abr ?? "";
                entity.NumSs = value.NumSS;
                entity.Alta = value.FchFrom;
                entity.Baja = value.FchTo;
                entity.Nac = value.Birthday;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.CliStaffs.Find(guid);
                if (entity != null)
                {
                    db.CliStaffs.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

        public static List<StaffModel.StaffPos> Positions()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwLangTexts
                        .AsNoTracking()
                    .Where(x => x.Src == (int)LangTextModel.Srcs.StaffPos)
                    .Select(x => new StaffModel.StaffPos(x.Guid, x.Esp, x.Cat, x.Eng, x.Por)).ToList();
                return retval;
            }
        }
        public static List<JornadaLaboralModel> JornadasLaborals(BaseGuid value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return JornadasLaboralsService.All(db, value);
            }
        }

    }
    public class StaffsService
    {
        public static List<DTO.StaffModel> All()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwStaffs
                         .AsNoTracking()
                   .Select(x => new StaffModel
                    {
                        Guid = x.Guid,
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Abr = x.Abr,
                        Nom = x.RaoSocial,
                        FchFrom = x.Alta,
                        FchTo = x.Baja,
                        Birthday = x.Nac,
                        NumSS = x.NumSs,
                        Position = x.StaffPos == null ? null : new StaffModel.StaffPos((Guid)x.StaffPos, x.StaffPosNomEsp, x.StaffPosNomCat, x.StaffPosNomEng, x.StaffPosNomPor)
                    })
                    .OrderBy(y => y.Abr)
                    .ToList();
                return retval;
            }
        }

        public static List<BancModel.Transfer> PendingTransfers()
        {
            using(var db = new Entities.MaxiContext())
            {
                //var fch = new DateTime(2023, 06, 30); // TO DO: remove criteria
                //read pending accounts
                var currentYear = DateTime.Today.Year;
                var itemValuePairs = db.Ccbs
                    .Include(x => x.Cca)
                    .Include(x => x.Cta)
                    .Where(x => x.Cta.Cod == (int)PgcCtaModel.Cods.PagasTreballadors && x.ContactGuid != null && x.Cca.Yea >= currentYear-1 ) // TO DO: retira date criteria
                    .GroupBy(x => new { x.Contact!.Emp, x.ContactGuid })
                    .Select(x => new KeyValuePair<int, BancModel.Transfer.Item>(x.Key.Emp, new BancModel.Transfer.Item
                    {
                        Beneficiari = (Guid)x.Key!.ContactGuid!,
                        Amount = new Amt(x.Sum(y => y.Dh == 2 ? y.Eur : -y.Eur))
                    }))
                    .ToList();

                //group by Emp
                var retval = new List<BancModel.Transfer>();
                foreach( var emp in itemValuePairs
                    .Where(x => x.Value.Amount != null && x.Value.Amount.Eur != 0)
                    .GroupBy(x => x.Key)){
                    var transfer = new BancModel.Transfer()
                    {
                        Cca = CcaModel.Factory((EmpModel.EmpIds)emp.Key, UserModel.Wellknown(UserModel.Wellknowns.info)!, CcaModel.CcdEnum.TransferNorma34),
                        Items = emp.Select(x => x.Value).ToList()
                    };
                    retval.Add(transfer);
                }
            return retval;
            }
        }
    }
}
