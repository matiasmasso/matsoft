using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ContactService
    {
        public static ContactModel? ContactModel(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var x = db.CliGrals
                    .Include(x => x.ContactClassNavigation)
                    .Where(x => x.Guid == guid)
                    .FirstOrDefault();

                var retval = new ContactModel
                {
                    Guid = x.Guid,
                    FullNom = x.FullNom,
                    Id = x.Cli,
                    RaoSocial = x.RaoSocial,
                    NomComercial = x.NomCom,
                    Rol = x.Rol,
                    Nifs = DTO.ContactModel.NifList.Factory(x.NifCod, x.Nif, x.Nif2Cod, x.Nif2),
                    ContactClass = ContactClassModel.Factory(x.ContactClass
                                  , x.ContactClassNavigation?.Esp
                                  , x.ContactClassNavigation?.Cat
                                  , x.ContactClassNavigation?.Eng
                                  , x.ContactClassNavigation?.Por)
                };

                if (retval != null)
                {
                    retval.Address = Address(db, guid);
                    retval.Telefons = Telefons(db, guid);
                    retval.Emails = Emails(db, guid);
                }
                return retval;
            }
        }

        public static AddressDTO? Address(Entities.MaxiContext db, Guid srcGuid, int? srcCod = 1)
        {
            return AddressService.Address(db, srcGuid, srcCod);
        }

        public static List<ContactModel.Telefon> Telefons(Entities.MaxiContext db, Guid contact)
        {
            return db.CliTels
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
        public static List<ContactModel.Email> Emails(Entities.MaxiContext db, Guid contact)
        {
            return db.EmailClis
                    .Where(x => x.ContactGuid == contact)
                    .OrderBy(x => x.Ord)
                    .Join(db.Emails, cli => cli.EmailGuid, email => email.Guid, (cli, email) => new ContactModel.Email
                    {
                        EmailAddress = email.Adr,
                        Obs = email.Obs
                    })
                    .ToList();
        }
        public static ContactDTO ContactDTO(Guid guid, UserModel user, LangDTO? lang = null)
        {
            var retval = new ContactDTO();
            retval.Contact = ContactModel(guid);
            return retval;
        }

        public static List<ContactModel.Telefon> Telefons(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliTels
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

        public static List<ContactModel.Email> Emails(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.EmailClis
                    .Where(x => x.ContactGuid == contact)
                    .OrderBy(x => x.Ord)
                    .Join(db.Emails, cli => cli.EmailGuid, email => email.Guid, (cli, email) => new ContactModel.Email
                    {
                        EmailAddress = email.Adr,
                        Obs = email.Obs
                    })
                    .ToList();
                return retval;
            }
        }
    }

    public class ContactsService
    {
        public static List<ContactModel> Fetch(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from contact in db.CliGrals
                              where contact.Emp == empId
                              && contact.Obsoleto == false
                              orderby contact.FullNom descending
                              select new ContactModel()
                              {
                                  Guid = contact.Guid,
                                  FullNom = contact.FullNom,
                                  Id = contact.Cli,
                                  SearchKey = contact.NomKey ?? ""
                              }).ToList();
                return retval;
            }
        }
        public static List<GuidNom> Cache(Entities.MaxiContext db, int empId)
        {
            var retval = (from contact in db.CliGrals
                          where contact.Emp == empId
                          select new GuidNom()
                          {
                              Guid = contact.Guid,
                              Nom = contact.FullNom
                          }).ToList();
            return retval;
        }
    }
}
