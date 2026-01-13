using Newtonsoft.Json;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserModel:BaseGuid, IModel
    {
        public string? EmailAddress { get; set; }
        public string? Hash { get; set; }
        public string? Nickname { get; set; }
        public Rols Rol { get; set; }
        public Guid? RootPerson { get; set; }

        //[JsonIgnore] //just for login component
        //public bool Persist { get; set; }

        public UserModel() { }
        public UserModel(Guid guid) : base(guid) { }
        public static UserModel Factory() => new UserModel { Nickname = "(nou usuari)" };

        public enum Rols
        {
            Guest,
            Admin
        }

        //public static UserModel? FromClaimsPrincipal(System.Security.Claims.ClaimsPrincipal? claimsPrincipal)
        //{
        //    UserModel? retval = null;
        //    if (claimsPrincipal != null)
        //    {
        //        var claims = claimsPrincipal.Claims.ToList();

        //        string? sGuid = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        //        Guid userGuid;
        //        if (Guid.TryParse(sGuid, out userGuid))
        //        {
        //            var sRol = claims.FirstOrDefault(x => x.Type == "Rol")?.Value;
        //            var rol = Enum.TryParse(sRol, out UserModel.Rols myRol) ? myRol : UserModel.Rols.Guest;

        //            retval = new UserModel(userGuid)
        //            {
        //                Nickname = claims.FirstOrDefault(x => x.Type == "DisplayName")?.Value,
        //                EmailAddress = claims.FirstOrDefault(x => x.Type == "Email")?.Value,
        //                Rol = rol,
        //                Hash = claims.FirstOrDefault(x => x.Type == "Hash")?.Value,
        //                RootPerson = claims.FirstOrDefault(x => x.Type == "RootPerson") == null ? null : new Guid(claims.FirstOrDefault(x => x.Type == "RootPerson")!.Value)
        //            };

        //        }
        //    }
        //    return retval;
        //}

        public string Caption() => String.Format("{0} ({1})", Nickname, EmailAddress);
        //public string PropertyPageUrl()=> Globals.PageUrl("User",Guid.ToString());
        //public string CreatePageUrl() => Globals.PageUrl("User");
        //public static string CollectionPageUrl() => Globals.PageUrl("Users");


        public static string GetHash(string emailAddress, string password) => DTO.Helpers.CryptoHelper.Hash(emailAddress, password);

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nickname + " " + EmailAddress;
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public override string ToString() => Caption();

        //public Claim[] Claims()
        //{
        //    var retval = new[] {
        //                //new Claim(JwtRegisteredClaimNames.Sub, "M+O"),
        //                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                new Claim("UserId", Guid.ToString()),
        //                new Claim("DisplayName", Nickname  ?? ""),
        //                new Claim("Email", EmailAddress ?? ""),
        //                new Claim("Rol",Rol.ToString() ),
        //                new Claim("Hash",Hash?.ToString() ?? "" ),
        //                new Claim("RootPerson", RootPerson == null ? "": RootPerson.ToString()! ),
        //            };
        //    return retval;
        //}

        //public ClaimsPrincipal ClaimsPrincipal() => new ClaimsPrincipal(new ClaimsIdentity(Claims(), "CustomAuth"));


    }
}
