using DocumentFormat.OpenXml.Drawing.Diagrams;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class TokenService
    {

        public static string? Token(IConfiguration _configuration, int emp, string hash)
        {
            string? retval = null;
            var user = UserService.FromHash(emp, hash);
            if (user != null)
            {
                //var empsList = user.Emps.Select(x => (int)x).ToList();
                //string empsJson = JsonConvert.SerializeObject(empsList);

                //var claims = new[] {
                //        new Claim(JwtRegisteredClaimNames.Sub, "M+O"), // _configuration["Jwt:Subject"]),
                //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                //        new Claim("UserId", user.Guid.ToString()),
                //        new Claim("DisplayName", user.Nickname ?? user.NomiCognoms() ?? ""),
                //        new Claim("Email", user.EmailAddress ?? ""),
                //        new Claim("Rol",user.Rol?.ToString() ?? "" ),
                //        new Claim("Hash",user.Hash?.ToString() ?? "" ),
                //        new Claim("Emps",empsJson, JsonClaimValueTypes.JsonArray)
                //    };

                var claims = user.Claims();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")); // _configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                retval = new JwtSecurityTokenHandler().WriteToken(token);
            }
            return retval;
        }
    }
}
