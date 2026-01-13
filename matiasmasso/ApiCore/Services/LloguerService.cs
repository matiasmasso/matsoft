using DTO;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class LloguerService
    {
        public static LloguerModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Lloguers
                    .Where(x => x.Guid == guid)
                    .Select(x => new LloguerModel(x.Guid)
                    {
                        EmpId = (EmpModel.EmpIds)x.Emp,
                        Customer = new ContactModel(x.Customer)
                        {
                            RaoSocial = x.CustomerNavigation.RaoSocial,
                            NomComercial = x.CustomerNavigation.NomCom,
                        },
                        Immoble = x.ImmobleNavigation == null ? null : new ImmobleModel((Guid)x.Immoble!)
                        {
                            Nom = x.ImmobleNavigation.Nom,
                            Cadastre = x.ImmobleNavigation.Cadastre
                        },
                        Cod = (LloguerModel.Cods?)x.Cod,
                        Contract = x.ContractNavigation == null ? null : new ContractModel((Guid)x.Contract!)
                        {
                            Nom = x.ContractNavigation.Nom,
                            Num = x.ContractNavigation.Num,
                            FchFrom = x.ContractNavigation.FchFrom,
                            FchTo = x.ContractNavigation.FchTo
                        },
                        Base = x.Base,
                        Iva = x.Iva,
                        Irpf = x.Irpf,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo
                    })
                    .FirstOrDefault();
            }
        }

        public static bool Update(LloguerModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, value);
                db.SaveChanges();
                return true;
            }
        }
        public static bool Update(MaxiContext db, LloguerModel value)
        {

            Entities.Lloguer? entity;
            if (value.IsNew)
            {
                entity = new Entities.Lloguer();
                entity.Guid = value.Guid;
                db.Lloguers.Add(entity);
            }
            else
            {
                entity = db.Lloguers
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Lloguer not found");
            }

            entity.Emp = (int)value.EmpId;
            entity.Customer = value.Customer!.Guid;
            entity.Immoble = value.Immoble?.Guid;
            entity.Cod = (int?)value.Cod ?? 0;
            entity.Contract = value.Contract?.Guid;
            entity.Base = value.Base;
            entity.Iva = value.Iva;
            entity.Irpf = value.Irpf;
            entity.FchFrom = value.FchFrom;
            entity.FchTo = value.FchTo;
            return true;
        }

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
            db.Lloguers.Remove(db.Lloguers.Single(x => x.Guid.Equals(guid)));
        }

        public static List<InvoiceSentModel> GetQuotes(Guid lloguerGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Lloguers
                    .Include(x => x.Invoices)
                    .AsNoTracking()
                    .Where(x => x.Guid == lloguerGuid)
                    .SelectMany(x => x.Invoices)
                    .Select(x => new InvoiceSentModel(x.Guid)
                    {
                        Contact = x.CliGuid,
                        Id = x.Fra1,
                        Fch = x.Fch,
                        Amt = new Amt(x.EurBase),
                    })
                    .OrderByDescending(x => x.Fch).ThenByDescending(x => x.Id)
                    .ToList();
            }
        }
        public static bool Invoice(Guid guid, InvoiceSentModel invoice)
        {
            var lloguer = GetValue(guid);
            using(var db = new Entities.MaxiContext())
            {
                //InvoiceSentService.Update(db, invoice); mes Cca
                //desar la factura amb LloguerQuotes

                db.SaveChanges();
            }
            return false;
        }

    }

    public class LloguersService
    {
        public static List<LloguerModel> GetValues(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.Lloguers
                    .Include(x => x.CustomerNavigation)
                    .Include(x => x.ImmobleNavigation)
                    .Include(x => x.ContractNavigation)
                    .AsNoTracking()
                     .Where(x => x.Emp == (int)emp)
                     .Select(x => new LloguerModel(x.Guid)
                     {
                         EmpId = (EmpModel.EmpIds)x.Emp,
                         Customer = new ContactModel(x.Customer)
                         {
                             RaoSocial = x.CustomerNavigation.RaoSocial,
                             NomComercial = x.CustomerNavigation.NomCom,
                         },
                         Immoble = x.ImmobleNavigation == null ? null : new ImmobleModel((Guid)x.Immoble!)
                         {
                             Nom = x.ImmobleNavigation.Nom,
                             Cadastre = x.ImmobleNavigation.Cadastre
                         },
                         Cod = (LloguerModel.Cods?)x.Cod,
                         Contract = x.ContractNavigation == null ? null : new ContractModel((Guid)x.Contract!)
                         {
                             Nom = x.ContractNavigation.Nom,
                             Num = x.ContractNavigation.Num,
                             FchFrom = x.ContractNavigation.FchFrom,
                             FchTo = x.ContractNavigation.FchTo
                         },
                         Base = x.Base,
                         Iva = x.Iva,
                         Irpf = x.Irpf,
                         FchFrom = x.FchFrom,
                         FchTo = x.FchTo
                     }).ToList();
            }
        }
    }
}
