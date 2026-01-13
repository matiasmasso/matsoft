
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserModel : BaseGuid, IModel
    {
        public string? EmailAddress { get; set; }
        public string? Nickname { get; set; }
        public Rols Rol { get; set; }
        public string? Password { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }


        public UserModel() { }
        public UserModel(Guid guid) : base(guid) { }
        public static UserModel Factory() => new UserModel { Nickname = "(nou usuari)", Rol = Rols.Guest };

        public enum Rols
        {
            Guest,
            Admin
        }


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

        public override string ToString() => String.Format("{0} ({1})", Nickname, EmailAddress);

    }
}
