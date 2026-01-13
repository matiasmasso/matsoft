using Api.Data;
using Api.Entities;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography;

namespace Api.Services
{

    public class UsersService
    {
        private readonly AppDbContext _db;
        public UsersService(AppDbContext db)
        {
            _db = db;
        }

        public UserModel? GetValue(Guid guid)
        {

            return _db.UserAccounts
                .Where(x => x.Guid == guid)
                .Select(x => new UserModel(x.Guid)
                {
                    EmailAddress = x.EmailAddress,
                    Nickname = x.Nickname,
                    Rol = (UserModel.Rols)x.Rol
                }).FirstOrDefault();
        }
        public List<UserModel> GetAllValues()
        {

            return _db.UserAccounts
                .OrderBy(x => x.Nickname)
                .Select(x => new UserModel(x.Guid)
                {
                    EmailAddress = x.EmailAddress,
                    Nickname = x.Nickname,
                    Rol = (UserModel.Rols)x.Rol
                }).ToList();
        }

        public bool IsDuplicated(UserModel value)
        {
            return _db.UserAccounts.Any(x => x.Guid != value.Guid && x.EmailAddress == value.EmailAddress);
        }

        public void Update(UserModel user)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.PasswordSalt = RandomNumberGenerator.GetBytes(16);

                user.PasswordHash = CryptoHelper.ComputePasswordHash(user.Password, user.PasswordSalt!);

                //user.PasswordHash = CryptoHelper.Hash(user.EmailAddress!, user.Password, user.PasswordSalt);
            }

            var entity = _db.UserAccounts.Where(x => x.Guid == user.Guid).FirstOrDefault();
            if(entity == null)
            {
                entity = new UserAccount { Guid = user.Guid };
                _db.UserAccounts.Add(entity);
            }

            entity.EmailAddress = user.EmailAddress;
            entity.Nickname = user.Nickname;
            entity.Rol = (int)user.Rol;
            if(user.PasswordSalt != null && user.PasswordHash != null)
            {
                entity.PasswordSalt = user.PasswordSalt;
                entity.PasswordHash = user.PasswordHash;
            }

            //_db.UserAccounts.Add(entity);
            _db.SaveChanges();
        }
    }
}
