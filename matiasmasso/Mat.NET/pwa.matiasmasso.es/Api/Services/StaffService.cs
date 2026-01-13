using DTO;
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

                if (entity == null) throw new Exception("Staff not found");

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
                    .Where(x => x.Src == (int)LangTextModel.Srcs.StaffPos)
                    .Select(x => new StaffModel.StaffPos(x.Guid, x.Esp, x.Cat, x.Eng, x.Por)).ToList();
                return retval;
            }
        }
        public static StaffDTO JornadasLaborals(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new StaffDTO();
                retval.Properties = Find(user.DefaultContact);
                retval.JornadasLaborals = JornadasLaboralsService.All(db, user);
                return retval;
            }
        }
    }
    public class StaffsService
    {
        public static List<DTO.StaffModel> All(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwStaffs
                    .Where(x => x.Emp == emp)
                    .Select(x => new StaffModel
                    {
                        Guid = x.Guid,
                        Emp = x.Emp,
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
    }
}
