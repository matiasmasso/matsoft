using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Components.Services
{
    public class GravatarService
    {

        private HttpClient http;
        private string? defaultHash;

        public GravatarService(HttpClient http)
        {
            this.http = http;
            Task task = Task.Run(async () => await LoadDefaultAvatarAsync());
        }

        public async Task<string> Url(UserModel user, int size)
        {
            string retval;
            var hasGravatar = await HasGravatar(user);
            if (hasGravatar)
                retval = UserUrl(user.EmailAddress ?? "", size);
            else
                retval = MonsterUrl(user.Guid, size);
            return retval;
        }

        private async Task<bool> HasGravatar(UserModel user)
        {
            var url = UserUrl(user.EmailAddress);
            var bytes = await http.GetByteArrayAsync(url);
            var hash = CryptoHelper.HashMD5(bytes);
            return hash != defaultHash;
        }

        private string DefaultUrl() => "https://www.gravatar.com/avatar";
        private string UserUrl(string emailAddress, int? size = null)
        {
            string? hash = HashEmailForGravatar(emailAddress);
            var retval = DefaultUrl() + "/" + hash;
            if (size != null)
            {
                var param = new Dictionary<string, string?>() { { "s", size.ToString() } };
                retval = new Uri(QueryHelpers.AddQueryString(retval, param)).ToString();
            }
            return retval;
        }
        private string MonsterUrl(Guid guid, int size) => $"https://www.gravatar.com/avatar/{guid.ToString("n")}?s={size}&d=monsterid";


        public async Task LoadDefaultAvatarAsync()
        {
            var defaultBytes = await http.GetByteArrayAsync(DefaultUrl());
            defaultHash = CryptoHelper.HashMD5(defaultBytes);
        }


        /// Hashes an email with MD5.  Suitable for use with Gravatar profile
        /// image urls
        private static string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}
