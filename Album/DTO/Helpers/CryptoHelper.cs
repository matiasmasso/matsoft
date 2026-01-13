using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DTO.Helpers
{
    public class CryptoHelper
    {

        public static byte[] ComputePasswordHash(string password, byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
            return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static byte[] Hash(string email, string password, byte[] salt)
        {
            var combined = $"{email}:{password}";
            var bytes = Encoding.UTF8.GetBytes(combined);

            return Rfc2898DeriveBytes.Pbkdf2(
                bytes,
                salt,
                100_000,
                HashAlgorithmName.SHA256,
                32
            );
        }

        public static string Sha256(byte[] oByteArray)
        {
            string retval = "";

            var Sha256 = SHA256.Create();
            //Compute the hash value from the source

            byte[] hashBytes = Sha256.ComputeHash(oByteArray);

            // And convert it to String format for return
            retval = Convert.ToBase64String(hashBytes);
            return retval;
        }

        public static string Sha256(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sha = SHA256.Create();
            var hashBytes = sha.ComputeHash(fs);
            var retval = Convert.ToBase64String(hashBytes);
            return retval;
        }


    }
}
