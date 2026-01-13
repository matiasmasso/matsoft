using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class NominaService
    {
        public static void Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                Delete(db, guid);
                db.SaveChanges();
            }
        }
        public static void Delete(Entities.MaxiContext db, Guid guid)
        {
            db.Nominas.Remove(db.Nominas.Single(x => x.CcaGuid.Equals(guid)));
            CcaService.Delete(db, guid);
        }
    }

    public class NominasService
    {
        public static List<NominaModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Nominas
                        .AsNoTracking()
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new NominaModel(x.CcaGuid)
                    {
                        Cca = new CcaModel(x.CcaGuid)
                        {
                            Fch = x.Cca.Fch,
                            Docfile = x.Cca.Hash == null ? null : new DocfileModel(x.Cca.Hash)
                        },
                        Staff = x.Staff,
                        IbanDigits = x.Iban,
                        Devengat = x.Devengat,
                        Dietes = x.Dietes,
                        SegSocial = x.SegSocial,
                        IrpfBase = x.IrpfBase,
                        Irpf = x.Irpf,
                        Embargos = x.Embargos,
                        Deutes = x.Deutes,
                        Anticips = x.Anticips,
                        Liquid = x.Liquid
                    }).ToList();
            }
        }
        public static List<NominaModel> All() //TO DEPRECATE
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Nominas
                        .AsNoTracking()
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new NominaModel(x.CcaGuid)
                    {
                        Cca = new CcaModel(x.CcaGuid)
                        {
                            Fch = x.Cca.Fch,
                            Docfile = x.Cca.Hash == null ? null : new DocfileModel(x.Cca.Hash)
                        },
                        Staff = x.Staff,
                        IbanDigits = x.Iban,
                        Devengat = x.Devengat,
                        Dietes = x.Dietes,
                        SegSocial = x.SegSocial,
                        IrpfBase = x.IrpfBase,
                        Irpf = x.Irpf,
                        Embargos = x.Embargos,
                        Deutes = x.Deutes,
                        Anticips = x.Anticips,
                        Liquid = x.Liquid
                    }).ToList();
            }
        }
        public static List<NominaModel> All(UserModel user) //TO DEPRECATE
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Nominas
                        .AsNoTracking()
                    .Where(x => x.Staff == user.DefaultContact)
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new NominaModel(x.CcaGuid)
                    {
                        Cca = new CcaModel(x.CcaGuid)
                        {
                            Fch = x.Cca.Fch,
                            Docfile = x.Cca.Hash == null ? null : new DocfileModel(x.Cca.Hash)
                        },
                        Staff = x.Staff,
                        IbanDigits = x.Iban,
                        Devengat = x.Devengat,
                        Dietes = x.Dietes,
                        SegSocial = x.SegSocial,
                        IrpfBase = x.IrpfBase,
                        Irpf = x.Irpf,
                        Embargos = x.Embargos,
                        Deutes = x.Deutes,
                        Anticips = x.Anticips,
                        Liquid = x.Liquid
                    }).ToList();
            }
        }
        public static List<NominaModel> All(Guid staff) //TO DEPRECATE
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Nominas
                        .AsNoTracking()
                    .Where(x => x.Staff == staff)
                    .OrderByDescending(z => z.Fch)
                    .Select(x => new NominaModel(x.CcaGuid)
                    {
                        Cca = new CcaModel(x.CcaGuid)
                        {
                            Fch = x.Cca.Fch,
                            Docfile = x.Cca.Hash == null ? null : new DocfileModel(x.Cca.Hash)
                        },
                        Staff = x.Staff,
                        IbanDigits = x.Iban,
                        Devengat = x.Devengat,
                        Dietes = x.Dietes,
                        SegSocial = x.SegSocial,
                        IrpfBase = x.IrpfBase,
                        Irpf = x.Irpf,
                        Embargos = x.Embargos,
                        Deutes = x.Deutes,
                        Anticips = x.Anticips,
                        Liquid = x.Liquid
                    }).ToList();
            }
        }

        public static void Update(List<NominaModel> nominas)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, nominas);
                db.SaveChanges();
            }

        }
        public static void Update(Entities.MaxiContext db, List<NominaModel> nominas)
        {
            foreach (var value in nominas)
            {
                CcaService.Update(db, value.Cca!);

                Entities.Nomina? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Nomina();
                    entity.CcaGuid = value.Guid;
                    db.Nominas.Add(entity);
                }
                else
                    entity = db.Nominas.Find(value.Guid);

                if (entity == null) throw new KeyNotFoundException();

                entity.Staff = (Guid)value.Staff!;
                entity.Fch = (DateOnly)value.Cca!.Fch!;
                entity.Iban = value.IbanDigits;
                entity.Devengat = value.Devengat ?? 0;
                entity.Dietes = value.Dietes ?? 0;
                entity.SegSocial = value.SegSocial ?? 0;
                entity.IrpfBase = value.IrpfBase ?? 0;
                entity.Irpf = value.Irpf ?? 0;
                entity.Embargos = value.Embargos ?? 0;
                entity.Deutes = value.Deutes ?? 0;
                entity.Anticips = value.Anticips ?? 0;
                entity.Liquid = value.Liquid ?? 0;
            }
        }

        public static void Delete(List<Guid> guids)
        {
            using (var db = new Entities.MaxiContext())
            {
                Delete(db, guids);
                db.SaveChanges();
            }
        }

        public static void Delete(Entities.MaxiContext db, List<Guid> guids)
        {
            var nominasToRemove = db.Nominas.Where(x => guids.Any(y => x.CcaGuid == y)).ToList();
            db.Nominas.RemoveRange(nominasToRemove);
            CcasService.Delete(db, guids);
        }

    }
}
