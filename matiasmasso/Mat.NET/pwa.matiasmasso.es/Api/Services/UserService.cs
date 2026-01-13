using Api.Entities;
using Api.Shared;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;

namespace Api.Services
{
    public class UserService
    {
        public static UserModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Emails
                    .Where(x => x.Guid == guid)
                    .Select(x => UserFromEntity(x))
                    .FirstOrDefault();
                if (retval != null)
                {
                    retval.Emps = db.Emails
                        .Where(x => x.Adr == retval.EmailAddress)
                        .Select(x => x.Emp)
                        .ToList();

                    if (retval.Residence != null)
                        retval.Residence = ZipService.Residence(retval.Residence.Guid, retval.Lang);

                }
                return retval;
            }

        }

        private static UserModel UserFromEntity(Entities.Email x)
        {
            return new UserModel(x.Guid)
            {
                Emp = x.Emp,
                EmailAddress = x.Adr,
                Hash = x.Hash,
                Nickname = x.Nickname,
                Nom = x.Nom,
                Cognoms = x.Cognoms,
                Residence = x.Residence == null ? null : new GuidNom { Guid = (Guid)x.Residence },
                BirthYea = x.BirthYea,
                Rol = x.Rol,
                Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                DefaultContact = x.DefaultContactGuid
            };
        }

        public static UserModel? FromHash(int emp, string hash)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Emails
                              .Where(x => x.Emp == emp && x.Hash == hash)
                              .Select(x => UserFromEntity(x))
                              .FirstOrDefault();

                if (retval != null)
                {
                    retval.Emps = db.Emails
                        .Where(x => x.Adr == retval.EmailAddress)
                        .Select(x => x.Emp)
                        .ToList();
                }

                return retval;
            }
        }
        public static UserModel? Login(LoginRequestDTO request) // deprecated
        {
            using (var db = new Entities.MaxiContext())
            {
                UserModel? retval = null;
                Guid? guid = db.Emails
                              .Where(x => x.Adr == request.Email && x.Pwd == request.Hash)
                              .Select(x => x.Guid)
                              .FirstOrDefault();
                if (guid != null && guid != Guid.Empty)
                    retval = Find((Guid)guid);
                return retval;
            }
        }

        public static UserModel? FromEmailAddress(int emp, string emailAddress)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Emails
                    .Where(x => x.Emp == emp && x.Adr == emailAddress)
                    .Select(x => UserFromEntity(x))
                    .FirstOrDefault();
                return retval;
            }
        }

        public static bool Update(UserModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Email? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Email();
                    db.Emails.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Emails.Find(value.Guid);

                if (entity == null) throw new System.Exception("User not found");

                entity.Emp = value.Emp;
                entity.Adr = value.EmailAddress ?? "";
                entity.Nickname = value.Nickname;
                entity.Hash = value.Hash;
                entity.Nom = value.Nom;
                entity.Cognoms = value.Cognoms;
                entity.Residence = value.Residence?.Guid;
                entity.BirthYea = value.BirthYea;
                entity.Rol = (short)value.Rol;
                entity.Lang = value.Lang?.ToString() ?? "ESP";
                entity.DefaultContactGuid = value.DefaultContact;

                // Save changes in database
                db.SaveChanges();

                // Update cache
                Cache.Users.RemoveAll(x => x.Guid == value.Guid);
                Cache.Users.Add(value);
                return true;
            }
        }

        public static bool ResetPwd(Guid guid, string hash)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Email? entity = db.Emails
                    .Where(x => x.Guid == guid)
                    .FirstOrDefault();
                if (entity == null) throw new System.Exception("User not found");
                entity.Hash = hash;

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }


        public static UserModel? PwdReset(LoginRequestDTO payload)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Email? entity = db.Emails
                    .Where(x => x.Emp == payload.Emp && x.Adr == payload.Email)
                    .FirstOrDefault();
                if (entity == null) throw new System.Exception("User not found");
                entity.Hash = payload.Hash;
                var guid = entity.Guid;
                // Save changes in database
                db.SaveChanges();

                // Update cache
                var retval = Find(guid);
                return retval;
            }
        }


        /// <summary>
        /// Lists customers parent or self where the user has access
        /// </summary>
        public static List<Guid> Ccxs(Entities.MaxiContext db, UserModel user) => CcxsQuery(db, user).ToList();

        public static IQueryable<Guid> CcxsQuery(Entities.MaxiContext db, UserModel user)
        {
            return db.EmailClis
                          .Where(x => x.EmailGuid == user.Guid)
                          .Join(db.VwCcxOrMes, email => email.ContactGuid, ccx => ccx.Guid, (email, ccx) => new { email, ccx })
                          .Select(x => (Guid)x.ccx.Ccx!).AsQueryable();
        }
    }

    public class UsersService
    {
        public static List<UserModel> Search(int emp, string searchTerm)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emails
                    .Where(x => x.Emp == emp && x.Adr.Contains(searchTerm))
                    .OrderBy(x => x.Adr)
                    .Select(x => new UserModel
                    {
                        Emp = emp,
                        Guid = x.Guid,
                        EmailAddress = x.Adr,
                        Nickname = x.Nickname,
                        Nom = x.Nom,
                        Cognoms = x.Cognoms,
                        Rol = x.Rol
                    }).ToList();
            }
        }
    }
}
