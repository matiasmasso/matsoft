using Api.Entities;
using Api.Shared;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;

namespace Api.Services
{
    public class UserService
    {
        public static UserModel? Find(Guid? guid)
        {
            UserModel? retval = null;
            if (guid != null)
            {
                using (var db = new Entities.MaxiContext())
                {
                    return Find(guid, db);
                }
            }
            return retval;
        }

        public static UserModel? Find(Guid? guid, Entities.MaxiContext db)
        {
            UserModel? retval = null;
            if (guid != null)
            {
                retval = db.Emails
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => UserFromEntity(x))
                    .FirstOrDefault();
                if (retval != null)
                {
                    retval.Emps = db.Emails
                        .AsNoTracking()
                        .Where(x => x.Adr == retval.EmailAddress)
                        .Select(x => (EmpModel.EmpIds)x.Emp)
                        .ToList();

                    if (retval.Residence != null)
                        retval.Residence = ZipService.Residence(retval.Residence.Guid, retval.Lang);

                }
            }
            return retval;
        }

        private static UserModel UserFromEntity(Entities.Email x)
        {
            return new UserModel(x.Guid)
            {
                Emp = (EmpModel.EmpIds)x.Emp,
                EmailAddress = x.Adr,
                Hash = x.Hash,
                Nickname = x.Nickname,
                Nom = x.Nom,
                Cognoms = x.Cognoms,
                Tel = x.Tel,
                Residence = x.Residence == null ? null : new GuidNom { Guid = (Guid)x.Residence },
                BirthYea = x.BirthYea,
                Rol = (UserModel.Rols)x.Rol,
                Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                DefaultContact = x.DefaultContactGuid,
                Source = (UserModel.Sources)x.Source
            };
        }

        public static UserModel? FromHash(int emp, string hash)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Emails
                        .AsNoTracking()
                              .Where(x => x.Emp == emp && x.Hash == hash)
                              .Select(x => UserFromEntity(x))
                              .FirstOrDefault();

                if (retval != null)
                {
                    retval.Emps = db.Emails
                         .AsNoTracking()
                       .Where(x => x.Adr == retval.EmailAddress)
                        .Select(x => (EmpModel.EmpIds)x.Emp)
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
                        .AsNoTracking()
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
                        .AsNoTracking()
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

                entity.Emp = (int)value.Emp;
                entity.Adr = value.EmailAddress ?? "";
                entity.Nickname = value.Nickname;
                entity.Hash = value.Hash;
                entity.Nom = value.Nom;
                entity.Cognoms = value.Cognoms;
                entity.Tel = value.Tel;
                entity.Residence = value.Residence?.Guid;
                entity.BirthYea = value.BirthYea;
                entity.Rol = (short?)value.Rol ?? (short)UserModel.Rols.guest;
                entity.Lang = value.Lang?.ToString() ?? "ESP";
                entity.DefaultContactGuid = value.DefaultContact;
                entity.Source = (int)value.Source;
                // Save changes in database
                db.SaveChanges();

                return true;
            }
        }

        public static bool UpdateUserNames(Entities.MaxiContext db, Guid userGuid, string firstname, string cognoms)
        {

            Entities.Email? entity = db.Emails.Find(userGuid);

            if (entity == null) throw new System.Exception("User not found");

            entity.Nom = firstname;
            entity.Cognoms = cognoms;
            db.SaveChanges();

            return true;

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
                        .AsNoTracking()
                          .Where(x => x.EmailGuid == user.Guid)
                          .Join(db.VwCcxOrMes, email => email.ContactGuid, ccx => ccx.Guid, (email, ccx) => new { email, ccx })
                          .Select(x => (Guid)x.ccx.Ccx!).AsQueryable();
        }
    }

    public class UsersService
    {
        public static List<UserModel> Professionals(int emp)
        {
            var nonProfessionals = UserModel.NonProfessionals;
            using (var db = new Entities.MaxiContext())
            {
                return db.Emails
                        .AsNoTracking()
                    .Where(x => x.Emp == emp && x.Rol<97 && x.Rol !=20 )
                    //.OrderBy(x => x.Adr)
                    .Select(x => new UserModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        EmailAddress = x.Adr,
                        Nickname = x.Nickname,
                        Nom = x.Nom,
                        Cognoms = x.Cognoms,
                        Rol = (UserModel.Rols)x.Rol,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        DefaultContact = x.DefaultContactGuid,
                        Source = (UserModel.Sources)x.Source
                    }).ToList();
            }
        }
        public static List<UserModel> Search(int emp, string searchTerm)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emails
                        .AsNoTracking()
                    .Where(x => x.Emp == emp && x.Adr.Contains(searchTerm))
                    .OrderBy(x => x.Adr)
                    .Select(x => new UserModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)emp,
                        EmailAddress = x.Adr,
                        Hash = x.Hash,
                        Nickname = x.Nickname,
                        Nom = x.Nom,
                        Cognoms = x.Cognoms,
                        Tel = x.Tel,
                        Residence = x.Residence == null ? null : new GuidNom { Guid = (Guid)x.Residence },
                        BirthYea = x.BirthYea,
                        Rol = (UserModel.Rols)x.Rol,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        DefaultContact = x.DefaultContactGuid,
                        Source = (UserModel.Sources)x.Source
                    }).ToList();
            }
        }
    }
}
