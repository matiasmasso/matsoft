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

        //used by my custom implementation
        public static string Hash(string username, string password)
        {
            //salt generated on https://www.randomtextgenerator.com/
            var salt = "Si onda pero perico ocHo es cabe. Ola habia fue verde largo las ver quiso debia. Favoritas gas por ser redundara cincuenta. Tarda sento creia mayor monja sus ano. Un tropezando alumbraban proteccion conciertos tu id dilettante aficionado. Romantica esa eso orgullosa asi enamorado negarselo";
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}{2}", salt, password, username);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var retval = Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
                return retval;
            }
        }

        //used by Microsoft identity tables
        public static string GetSha512Hash(string input)
        {
            using (var hashAlgorithm = SHA512.Create())
            {
                var byteValue = System.Text.Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }


        //used by Redsys Tpv REST
        public static string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                var byteValue = System.Text.Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }

        public static byte[] GetHMACSHA256(string msg, byte[] SignatureKey)
        {
            byte[] retval = null;
            try
            {
                // Obtain byte[] from input string
                byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(msg);

                // Initialize the keyed hash object.
                using (HMACSHA256 hmac = new HMACSHA256(SignatureKey))
                {

                    // Compute the hash of the input file.
                    retval = hmac.ComputeHash(msgBytes, 0, msgBytes.Length);
                }
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException(ex.Message);
            }
            return retval;
        }

        public static string HashMD5(string src)=> HashMD5(System.Text.ASCIIEncoding.ASCII.GetBytes(src));

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

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static Dictionary<string, string>? FromUrlFriendlyBase64Json(string? source)
        {
            Dictionary<string, string>? retval = null;
            if (source != null)
            {
                var sFromUrlFriendly = FromUrFriendlyBase64(source ?? "");
                var oBytes = System.Convert.FromBase64String(sFromUrlFriendly);
                if(oBytes != null)
                {
                    var sDecodedSource = System.Text.Encoding.UTF8.GetString(oBytes);
                    retval = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(sDecodedSource);
                }
            }
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

        #region "Cifrado 3DES for TPV Redsys"

        //https://stackoverflow.com/questions/11413576/how-to-implement-triple-des-in-c-sharp-complete-example
        //https://pagosonline.redsys.es/conexion-rest.html

        public static byte[] Encrypt3DES(string plainText, byte[] SignatureKey)
        {
            byte[]? retval = null;

            if (string.IsNullOrEmpty(plainText))
                throw new FormatException("pedido vacio en Encrypt3DES");
            else
            {
                byte[] toEncryptArray = System.Text.Encoding.UTF8.GetBytes(plainText);
                var tdes = TripleDES.Create(); // new TripleDESCryptoServiceProvider();
                try
                {
                    // SALT used in 3DES encryptation process
                    byte[] SALT = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                    // Block size 64 bit (8 bytes)
                    tdes.BlockSize = 64;

                    // Key Size 192 bit (24 bytes)
                    tdes.KeySize = 192;
                    tdes.Mode = CipherMode.CBC;
                    tdes.Padding = PaddingMode.Zeros;

                    tdes.IV = SALT;
                    tdes.Key = SignatureKey;

                    ICryptoTransform cTransform = tdes.CreateEncryptor();

                    // transform the specified region of bytes array to resultArray
                    retval = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    // Release resources held by TripleDes Encryptor
                    tdes.Clear();
                }
                catch (CryptographicException ex)
                {
                    throw new CryptographicException(ex.Message);
                }
            }

            return retval;
        }



        public static string TripleDesEncrypt(string key, string plainText)
        {
            var des = CreateDes(key);
            var ct = des.CreateEncryptor();
            var input = Encoding.UTF8.GetBytes(plainText);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(output);
        }

        public static string TripleDesDecrypt(string key, string cypherText)
        {
            var des = CreateDes(key);
            var ct = des.CreateDecryptor();
            var input = Convert.FromBase64String(cypherText);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(output);
        }

        public static TripleDES CreateDes(string key)
        {
            MD5 md5 = MD5.Create(); // new MD5CryptoServiceProvider();
            TripleDES des = TripleDES.Create(); // new TripleDESCryptoServiceProvider();
            var desKey = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            des.Key = desKey;
            des.IV = new byte[des.BlockSize / 8];
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            return des;
        }

        #endregion



    }
}
