using Api.Entities;
using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Data.SqlTypes;

namespace Api.Services
{
    public class ContactService
    {
        public static ContactModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.CliGrals
                    .Include(x => x.ContactClassNavigation)
                    .Include(x => x.CliClient)
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ContactModel(guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        FullNom = x.FullNom,
                        Id = x.Cli,
                        RaoSocial = x.RaoSocial,
                        NomComercial = x.NomCom,
                        SearchKey = x.NomKey,
                        Rol = x.Rol,
                        Nifs = DTO.ContactModel.NifList.Factory(x.NifCod, x.Nif, x.Nif2Cod, x.Nif2),
                        ContactClass = x.ContactClass,
                        SuProveedorNum = x.CliClient == null ? null : x.CliClient.SuProveedorNum,
                        Ccx = x.CliClient == null ? null : x.CliClient.CcxGuid,
                        Req = x.CliClient == null ? false : x.CliClient.Req,
                        Obsoleto = x.Obsoleto
                    })
                 .FirstOrDefault();

                if (retval != null)
                {
                    retval.Address = Address(db, guid);
                    retval.Telefons = Telefons(db, guid);
                    retval.Emails = Emails(db, guid);
                }
                return retval;
            }
        }

        public static bool Update(ContactModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.CliGral? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CliGral();
                    SetNextId(db, value, ref entity);
                    db.CliGrals.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                {
                    entity = db.CliGrals.Find(value.Guid);
                    if (entity == null) throw new System.Exception("Contact not found");

                    if ((int)value.Emp != entity.Emp)
                        SetNextId(db, value, ref entity);
                }


                entity.RaoSocial = value.RaoSocial ?? "";
                entity.NomCom = value.NomComercial ?? ""; //defaults to non null to keep compatibility with mat.net
                entity.FullNom = value.FullNom ?? "";
                entity.NomKey = value.SearchKey;
                entity.ContactClass = value.ContactClass;
                entity.Obsoleto = value.Obsoleto;

                if (value.Address != null)
                    AddressService.Update(db, value.Address);

                if (value.Nifs != null)
                {
                    if (value.Nifs.Count > 0)
                    {
                        entity.NifCod = value.Nifs[0].Cod;
                        entity.Nif = value.Nifs[0].Value ?? "";
                    }
                    if (value.Nifs.Count > 1)
                    {
                        entity.Nif2Cod = value.Nifs[1].Cod;
                        entity.Nif2 = value.Nifs[1].Value ?? "";
                    }
                }

                db.Clls.RemoveRange(db.Clls.Where(x => x.ContactGuid.Equals(value.Guid)));

                if (!string.IsNullOrEmpty(value.RaoSocial))
                {
                    var entityNom = new Entities.Cll();
                    entityNom.ContactGuid = value.Guid;
                    entityNom.Cll1 = value.RaoSocial.Truncate(40);
                    //entityNom.Cod = 0;
                    db.Clls.Add(entityNom);
                }
                if (!string.IsNullOrEmpty(value.NomComercial))
                {
                    var entityCll = new Entities.Cll();
                    entityCll.ContactGuid = value.Guid;
                    entityCll.Cll1 = value.NomComercial.Truncate(40);
                    entityCll.Cod = 4;
                    db.Clls.Add(entityCll);
                }
                if (!string.IsNullOrEmpty(value.SearchKey))
                {
                    var entitySk = new Entities.Cll();
                    entitySk.ContactGuid = value.Guid;
                    entitySk.Cll1 = value.SearchKey.Truncate(40);
                    entitySk.Cod = 26;
                    db.Clls.Add(entitySk);
                }
                if (value.Address?.ZipGuid != null)
                {
                    var entityZip = db.Zips.FirstOrDefault(x => x.Guid == value.Address.ZipGuid);
                    if(entityZip != null)
                    {
                        var entityLocation = db.Locations.FirstOrDefault(x => x.Guid == entityZip.Location);
                        if(entityLocation != null)
                        {
                            var entityAdr = new Entities.Cll();
                            entityAdr.ContactGuid = value.Guid;
                            entityAdr.Cll1 = entityLocation.Nom.Truncate(40);
                            entityAdr.Cod = 3;
                            db.Clls.Add(entityAdr);
                        }
                    }
                }

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var value = GetValue(guid);
                if (value?.Address != null)
                    AddressService.Delete(db, value.Address);
                db.Clls.Remove(db.Clls.Single(x => x.ContactGuid == guid));
                db.CliGrals.Remove(db.CliGrals.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

        public static void SetNextId(MaxiContext db, ContactModel value, ref Entities.CliGral entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;

            int lastId;
            if (db.CliGrals.Any(x => x.Emp == emp))
                lastId = db.CliGrals.Where(x => x.Emp == emp).Max(x => x.Cli);
            else
                lastId = 0;

            entity.Guid = value.Guid;
            entity.Emp = emp;
            entity.Cli = lastId + 1;
        }

        public static AddressModel? Address(Entities.MaxiContext db, Guid srcGuid, int? srcCod = 1)
        {
            return AddressService.Address(db, srcGuid, srcCod);
        }

        public static List<ContactModel.Telefon> Telefons(Entities.MaxiContext db, Guid contact)
        {
            return db.CliTels
                    .AsNoTracking()
                .Where(x => x.CliGuid == contact)
                .Include(x => x.CountryNavigation)
                .OrderBy(x => x.Ord)
                .Select(x => new DTO.ContactModel.Telefon
                {
                    Prefix = x.CountryNavigation.PrefixeTelefonic,
                    Number = x.Num,
                    Obs = x.Obs
                })
                .ToList();
        }
        public static List<UserModel> Emails(Entities.MaxiContext db, Guid contact)
        {
            return db.EmailClis
                    .AsNoTracking()
                    .Include(x => x.Email)
                    .Where(x => x.ContactGuid == contact)
                    .OrderBy(x => x.Ord)
                    .Select(x => new UserModel(x.EmailGuid)
                    {
                        EmailAddress = x.Email.Adr,
                        Obs = x.Email.Obs,
                        Rol = (UserModel.Rols?)x.Email.Rol,
                        Nickname = x.Email.Nickname
                    })
                    .ToList();
        }

        public static List<ContactModel.Telefon> Telefons(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliTels
                    .AsNoTracking()
                    .Where(x => x.CliGuid == contact)
                    .OrderBy(x => x.Ord)
                    .Join(db.Countries, tel => tel.Country, country => country.Guid, (tel, country) => new ContactModel.Telefon
                    {
                        Prefix = country.PrefixeTelefonic,
                        Number = tel.Num,
                        Obs = tel.Obs
                    })
                    .ToList();
            }
        }

        public static List<UserModel> Emails(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Emails(db, contact);
            }
        }
    }

    public class ContactsService
    {
        public static List<ContactModel> Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliGrals
                    .AsNoTracking()
                    .Where(x => x.Emp == 6 || x.Emp == 10)
                    .Select(x => new ContactModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Id = x.Cli,
                        FullNom = x.FullNom,
                        SearchKey = x.NomKey ?? "",
                        RaoSocial = x.RaoSocial,
                        NomComercial = x.NomCom,
                        Rol = x.Rol,
                        Nifs = DTO.ContactModel.NifList.Factory(x.NifCod, x.Nif, x.Nif2Cod, x.Nif2),
                        ContactClass = x.ContactClass,
                        SuProveedorNum = x.CliClient == null ? null : x.CliClient.SuProveedorNum,
                        Ccx = x.CliClient == null ? null : x.CliClient.CcxGuid,
                        Obsoleto = x.Obsoleto
                    })
                    .OrderBy(x => x.FullNom)
                    .ToList();
            }
        }

        public static List<ContactModel> FetchAll()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }

        public static List<ContactModel> GetValues(Entities.MaxiContext db)
        {
            return db.CliGrals
                .AsNoTracking()
                .Select(x => new ContactModel(x.Guid)
                {
                    Emp = (EmpModel.EmpIds)x.Emp,
                    Id = x.Cli,
                    FullNom = x.FullNom,
                    SearchKey = x.NomKey ?? "",
                    RaoSocial = x.RaoSocial,
                    NomComercial = x.NomCom,
                    Rol = x.Rol,
                    Nifs = DTO.ContactModel.NifList.Factory(x.NifCod, x.Nif, x.Nif2Cod, x.Nif2),
                    ContactClass = x.ContactClass,
                    SuProveedorNum = x.CliClient == null ? null : x.CliClient.SuProveedorNum,
                    Ccx = x.CliClient == null ? null : x.CliClient.CcxGuid,
                    Obsoleto = x.Obsoleto
                })
                .OrderBy(x => x.FullNom)
                .ToList();
        }



        private static ContactModel.NifList Nifs(Entities.CliGral x)
        {
            ContactModel.NifList retval = new();
            if (!string.IsNullOrEmpty(x.Nif))
                retval.Add(new ContactModel.Nif() { Cod = (int)x.NifCod!, Value = x.Nif });
            if (!string.IsNullOrEmpty(x.Nif2))
                retval.Add(new ContactModel.Nif() { Cod = (int?)x.Nif2Cod, Value = x.Nif2 });
            return retval;
        }
        public static List<GuidNom> Cache(Entities.MaxiContext db, EmpModel.EmpIds empId)
        {
            var retval = (from contact in db.CliGrals
                          where contact.Emp == (int)empId
                          select new GuidNom()
                          {
                              Guid = contact.Guid,
                              Nom = contact.FullNom
                          }).ToList();
            return retval;
        }
    }
}
