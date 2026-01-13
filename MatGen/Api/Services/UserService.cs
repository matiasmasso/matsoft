using Api.Entities;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class UserService
    {
        private readonly MatGenContext _db;
        public UserService(MatGenContext db)
        {
            _db = db;
        }
        public UserModel? Find(Guid guid)
        {
            return _db.UserAccounts
               .Where(x => x.Guid == guid)
               .Select(x => new UserModel(guid)
               {
                   EmailAddress = x.EmailAddress,
                   Hash = x.Hash,
                   Nickname = x.Nickname,
                   Rol = (UserModel.Rols)x.Rol,
                   RootPerson = x.RootPerson
               })
               .FirstOrDefault();
        }

        public UserModel? FromHash(string hash)
        {
            var retla = _db.UserAccounts
                .Where(x => x.Hash == hash)
                .Select(x => new UserModel(x.Guid)
                {
                    EmailAddress = x.EmailAddress,
                    Hash = x.Hash,
                    Nickname = x.Nickname,
                    Rol = (UserModel.Rols)x.Rol,
                    RootPerson = x.RootPerson
                })
                .FirstOrDefault();
            return retla;
        }

        public bool Update(UserModel value)
        {
            Entities.UserAccount? entity;
            if (value.IsNew)
            {
                entity = new Entities.UserAccount();
                _db.UserAccounts.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = _db.UserAccounts.Where(x => x.Guid == value.Guid).FirstOrDefault();

            if (entity == null) throw new Exception("User not found");

            entity.EmailAddress = value.EmailAddress;
            entity.Nickname = value.Nickname;
            entity.Hash = value.Hash;
            entity.Rol = (int)value.Rol;
            entity.RootPerson = value.RootPerson;

            // Save changes in database
            _db.SaveChanges();
            return true;
        }

        public bool Delete(UserModel value)
        {
            var entity = _db.UserAccounts.Remove(_db.UserAccounts.Single(x => x.Guid.Equals(value.Guid)));
            _db.SaveChanges();
            return true;

        }

    }

    public class UsersService
    {
        private readonly MatGenContext _db;
        public UsersService(MatGenContext db)
        {
            _db = db;
        }
        public List<UserModel> All()
        {

            return _db.UserAccounts
                .OrderBy(x => x.Nickname)
                .Select(x => new UserModel(x.Guid)
                {
                    EmailAddress = x.EmailAddress,
                    Nickname = x.Nickname,
                    Hash = x.Hash,
                    Rol = (UserModel.Rols)x.Rol,
                    RootPerson = x.RootPerson
                }).ToList();
        }
    }
}
