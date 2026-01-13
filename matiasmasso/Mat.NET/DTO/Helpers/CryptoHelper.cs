using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DTO.Helpers
{
    public class CryptoHelper
    {
        public static string Hash(string username, string password)
        {
            //salt generated on https://www.randomtextgenerator.com/
            var salt = "Si onda pero perico ocHo es cabe. Ola habia fue verde largo las ver quiso debia. Favoritas gas por ser redundara cincuenta. Tarda sento creia mayor monja sus ano. Un tropezando alumbraban proteccion conciertos tu id dilettante aficionado. Romantica esa eso orgullosa asi enamorado negarselo";
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}{2}", salt, password, username);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

        //used to generate unique consumer ticket numbers
        public static string AsinFactory() => RandomString(10);
        public static string RandomString(int maxLen)
        {
            String range = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int rangeLength = range.Length;
            Random random = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < maxLen; i++)
            {
                int idx = random.Next(rangeLength);
                sb.Append(range.ElementAt(idx));
            }
            return sb.ToString();
        }
    }
}
