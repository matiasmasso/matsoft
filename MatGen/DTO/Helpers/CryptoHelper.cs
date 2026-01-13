using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Helpers
{
    public class CryptoHelper
    {
        //used by my custom implementation
        public static string Hash(string username, string password)
        {
            //salt generated on https://www.randomtextgenerator.com/
            var salt = "Si onda pero perico ocHo es cabe. Ola habia fue verde largo las ver quiso debia. Favoritas gas por ser redundara cincuenta. Tarda sento creia mayor monja sus ano. Un tropezando alumbraban proteccion conciertos tu id dilettante aficionado. Romantica esa eso orgullosa asi enamorado negarselo";
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}{2}", salt, password, username.ToLower());
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

        //used by Microsoft identity tables
        public static string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = SHA512.Create())
            {
                var byteValue = System.Text.Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }

        /// <summary>
        /// Media file signature
        /// </summary>
        /// <param name="oByteArray"></param>
        /// <returns></returns>
        public static string HashMD5(byte[] oByteArray)
        {
            //MD5CryptoServiceProvider Md5 = new System.Security.Cryptography.MD5CryptoServiceProvider(); //deprecated => MD5.Create
            var Md5 = MD5.Create();
            // Compute the hash value from the source
            byte[] ByteHash = Md5.ComputeHash(oByteArray);
            // And convert it to String format for return
            string retval = Convert.ToBase64String(ByteHash);
            return retval;
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


        public static string FromUrFriendlyBase64(string source)
        {
            string retval = source.Replace("-", "+").Replace("_", "/").Replace("~", "=");
            return retval;
        }

        public static string UrlFriendlyBase64(string source)
        {
            string retval = "";
            if (!string.IsNullOrEmpty(source))
                retval = source.Replace("+", "-").Replace("/", "_").Replace("=", "~");
            return retval;
        }

    }
}
