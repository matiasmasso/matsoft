using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ContractService
    {

        public static ContractModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Contracts
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ContractModel(x.Guid)
                    {
                        Emp= (EmpModel.EmpIds)x.Emp!,
                        Nom = x.Nom,
                        Num = x.Num,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        Codi = x.CodiGuid,
                        Contact = x.ContactGuid,
                        Docfile = x.Hash == null ? null : new DocfileModel { Hash=x.Hash}
                    }).FirstOrDefault();
            }
        }
        public static Byte[]? Stream(Guid guid)
        {
            Byte[]? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                retval = (from item in db.Contracts
                          join docfiles in db.DocFiles
                          on item.Hash equals docfiles.Hash
                          where item.Guid.Equals(guid)
                          select docfiles.Doc).FirstOrDefault();
            }
            return retval;
        }

        public static bool Update(ContractModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, value);
                db.SaveChanges();
                return true;
            }
        }
        public static bool Update(MaxiContext db, ContractModel value)
        {
            if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

            Entities.Contract? entity;
            if (value.IsNew)
            {
                entity = new Entities.Contract();
                entity.Guid = value.Guid;
                db.Contracts.Add(entity);
            }
            else
            {
                entity = db.Contracts.Find(value.Guid);
                if (entity == null) throw new System.Exception("Contract not found");
            }

            entity.Emp = (int)value.Emp!;
            entity.Nom = value.Nom ?? "";
            entity.Num = value.Num ?? "";
            entity.FchFrom = (DateOnly)value.FchFrom!;
            entity.FchTo= value.FchTo;
            entity.CodiGuid = value.Codi;
            entity.ContactGuid = value.Contact;
            //entity.Codi = db.ContractCodis.FirstOrDefault(x => x.Guid == value.Codi);
            //entity.Contact = db.CliGrals.FirstOrDefault(x => x.Guid == value.Contact);
            entity.Hash = value.Docfile?.Hash;
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
            db.Contracts.Remove(db.Contracts.Single(x => x.Guid.Equals(guid)));
        }


    }

    public class ContractsService
    {
        public static List<ContractModel> Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Contracts
                                    .Select(x => new ContractModel(x.Guid)
                                    {
                                        Emp= (EmpModel.EmpIds)x.Emp!,
                                        Nom = x.Nom,
                                        Num = x.Num,
                                        FchFrom = x.FchFrom,
                                        FchTo = x.FchTo,
                                        Codi = x.CodiGuid,
                                        Contact = x.ContactGuid,
                                        Docfile = x.Hash == null ? null : new DocfileModel { Hash = x.Hash }
                                    }).ToList();

                return retval;
            }
        }

        public static List<ContractModel.CodiClass> Codis()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.ContractCodis
                                    .Select(x => new ContractModel.CodiClass(x.Guid)
                                    {
                                        Nom = x.Nom,
                                        Cta = x.Cta
                                    }).ToList();

                return retval;
            }
        }

        public static List<GuidNom> FromCodi(Guid guid)
        {

            using (var db = new Entities.MaxiContext())
            {
                return db.Contracts
                    .AsNoTracking()
                    .Where(x => x.CodiGuid == guid)
                    .Select(x => new GuidNom
                    {
                        Guid = x.Guid,
                        Nom =( x.Contact == null?"":string.IsNullOrEmpty(x.Contact.NomCom)?x.Contact.RaoSocial: x.Contact.NomCom ?? "") + " " + x.Nom + " " + x.Num
                    })
                    .ToList();
            }
        }

    }
}
