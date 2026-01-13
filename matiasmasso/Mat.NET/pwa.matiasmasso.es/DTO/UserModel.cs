using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DTO.Helpers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DTO
{
    public class UserModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Hash { get; set; }
        public string? Nickname { get; set; }
        public string? Nom { get; set; }
        public string? Cognoms { get; set; }
        public string? Tel { get; set; }

        public UserModel.Sources Source { get; set; }

        public LangDTO? Lang { get; set; }
        public int? BirthYea { get; set; }
        public GuidNom? Residence { get; set; }

        public ZipModel? Zip { get; set; }
        public UserModel.Rols? Rol { get; set; }
        public CountryDTO? Country { get; set; }
        public Guid? DefaultContact { get; set; }
        public string? Obs { get; set; }
        public List<EmpModel.EmpIds> Emps { get; set; } = new();

        public enum Wellknowns
        {
            info,
            portugal,
            matias,
            victoria,
            toni,
            zabalaHoyos,
            rosillo,
            carlosRuiz,
            enric,
            xavi,
            eric,
            traquinaPerfeito
        }
        public enum Rols : int
        {
            notSet,
            superUser,
            admin,
            operadora,
            accounts,
            salesManager,
            comercial,
            taller,
            rep = 8,
            cliFull = 9,
            manufacturer = 10,
            marketing = 11,
            cliLite = 12,
            auditor = 14,
            banc = 15,
            transportista = 16,
            logisticManager = 17,
            pr = 18,
            guest = 20,
            employee = 21,
            lead = 98,
            unregistered = 99,
            denied = 100
        }

        public enum Sources
        {
            notSet,
            website,
            blog,
            wpFollower,
            wpComment,
            manual,
            webComment,
            raffle,
            facebook,
            fb4moms,
            iMat,
            external,
            spv,
            erp,
            shop4moms,
            marketPlace
        }
        public UserModel() : base() { }
        public UserModel(Guid guid) : base(guid) { }

        public static UserModel? FromToken(string? token)
        {
            UserModel? retval = null;
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();

                string? sGuid = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                Guid userGuid;
                if (Guid.TryParse(sGuid, out userGuid))
                    retval = new UserModel(userGuid)
                    {
                        Nickname = claims.FirstOrDefault(x => x.Type == "DisplayName")?.Value,
                        EmailAddress = claims.FirstOrDefault(x => x.Type == "Email")?.Value,
                        Rol = (Rols?)Convert.ToInt32(claims.FirstOrDefault(x => x.Type == "Rol")?.Value),
                        Hash = claims.FirstOrDefault(x => x.Type == "Hash")?.Value
                        //Emps = claims.FirstOrDefault(x => x.Type == "Emps")?.Value
                    };
            }
            return retval;
        }


        public static UserModel? FromClaimsPrincipal(System.Security.Claims.ClaimsPrincipal? claimsPrincipal)
        {
            UserModel? retval = null;
            if (claimsPrincipal != null)
            {
                var claims = claimsPrincipal.Claims.ToList();

                string? sGuid = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                Guid userGuid;
                if (Guid.TryParse(sGuid, out userGuid))
                {
                    var sRol = claims.FirstOrDefault(x => x.Type == "Rol")?.Value;
                    var rol = Enum.TryParse(sRol, out UserModel.Rols myRol) ? myRol: UserModel.Rols.notSet;

                retval = new UserModel(userGuid)
                    {
                        Nickname = claims.FirstOrDefault(x => x.Type == "DisplayName")?.Value,
                        EmailAddress = claims.FirstOrDefault(x => x.Type == "Email")?.Value,
                        Rol = rol,
                        Hash = claims.FirstOrDefault(x => x.Type == "Hash")?.Value
                        //Emps = claims.FirstOrDefault(x => x.Type == "Emps")?.Value
                    };

                }
            }
            return retval;
        }

        public static UserModel? Wellknown(Wellknowns id)
        {
            UserModel? retval = null;
            string sGuid = "";
            switch (id)
            {
                case Wellknowns.info:
                    {
                        sGuid = "A117BF28-CADF-439E-B3B2-575B9AC615B4";
                        break;
                    }

                case Wellknowns.portugal:
                    {
                        sGuid = "CB110644-92A9-404A-99B8-79D7C48A6502";
                        break;
                    }

                case Wellknowns.victoria:
                    {
                        sGuid = "B166BB33-C277-4FC4-B1FF-F7713C101215";
                        break;
                    }

                case Wellknowns.matias:
                    {
                        sGuid = "961297AF-BC62-44ED-932A-2C445B7D69C3";
                        break;
                    }

                case Wellknowns.toni:
                    {
                        sGuid = "5FA9EE85-02D2-415A-AB30-A015A240CD13";
                        break;
                    }

                case Wellknowns.zabalaHoyos:
                    {
                        sGuid = "9512706E-06AF-4859-B4AE-D639DEC471A7";
                        break;
                    }

                case Wellknowns.rosillo:
                    {
                        sGuid = "7AC3B5CD-C0EB-40C3-820B-5D3FE44ABF05";
                        break;
                    }

                case Wellknowns.carlosRuiz:
                    {
                        sGuid = "0BFC6E6C-1E78-48ED-B105-B16A19869840";
                        break;
                    }

                case Wellknowns.enric:
                    {
                        sGuid = "38D5EC9D-B830-478E-9CB6-6C7945F4BA82";
                        break;
                    }

                case Wellknowns.xavi:
                    {
                        sGuid = "FA89CF75-71C9-48EA-BC20-B882C6C6FED7";
                        break;
                    }

                case Wellknowns.eric:
                    {
                        sGuid = "79C24788-A7E4-4520-9804-D95DDFDE915F";
                        break;
                    }

                case Wellknowns.traquinaPerfeito:
                    {
                        sGuid = "8501C51D-EEC8-44C3-B3CE-1468F673354D";
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(sGuid))
            {
                Guid oGuid = new Guid(sGuid);
                retval = new UserModel(oGuid);
                retval.Emp = EmpModel.EmpIds.MatiasMasso;
            }
            return retval;
        }

        public EmpModel.EmpIds? DefaultEmp()
        {
            var defaultEmp = EmpModel.EmpIds.MMC;
            return Emps.Contains(defaultEmp) ? defaultEmp : Emps.FirstOrDefault();
        }

        public EmpModel.EmpIds? DefaultEmp(List<EmpModel.EmpIds>? emps)
        {
            EmpModel.EmpIds? retval = null;
            if (Emps.Count > 0 && emps?.Count>0)
            {
                var defaultEmp = (EmpModel.EmpIds)DefaultEmp()!;
                if (emps.Contains(defaultEmp))
                    retval = defaultEmp;
                else
                    retval = emps.FirstOrDefault(x => Emps.Any(y=> x == y));
            }
            return retval;
        }

        public string GravatarUrl()
        {
            string retval = "";
            if (EmailAddress != null)
            {
                var src = EmailAddress.ToLower().Trim();
                var hash = HashEmailForGravatar(src);
                retval = $"https://www.gravatar.com/avatar/{hash}";
            }
            return retval;
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
        public bool IsLocal() => EmailAddress?.Contains("matiasmasso", StringComparison.CurrentCultureIgnoreCase) ?? false;
        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption() => EmailAddress?.ToString() ?? "?";
        public override string ToString() => EmailAddress?.ToString() ?? "{UserModel}";

        public string? NomiCognoms()
        {
            StringBuilder sb = new StringBuilder();
            if (!String.IsNullOrEmpty(Nom))
                sb.Append(Nom);
            if (!String.IsNullOrEmpty(Nom) && !String.IsNullOrEmpty(Cognoms))
                sb.Append(" ");
            sb.Append(Cognoms ?? "");
            return sb.ToString();
        }
        public string? NomiCognomsOrNickname() => string.IsNullOrEmpty(NomiCognoms()) ? Nickname : NomiCognoms();
        public string? NomiCognomsOrNicknameOrEmailAddress() => string.IsNullOrEmpty(NomiCognomsOrNickname()) ? EmailAddress : NomiCognomsOrNickname();

        public static string DisplayNom(string? emailAddress, string? nickname)
        {
            var retval = nickname;
            if (string.IsNullOrEmpty(retval))
                retval = emailAddress;
            return retval ?? "";
        }

        public static string DefaultPassword(string emailAddress) => Helpers.CryptoHelper.GetSha512Hash(emailAddress).Substring(0, 6);
        public static string GetHash(string emailAddress, string password) => Helpers.CryptoHelper.Hash(emailAddress, password);


        #region Rols

        public bool isSuperAdmin()
        {
            bool Retval = false;
            switch ((UserModel.Rols)Rol!)
            {
                case Rols.superUser:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isAdmin()
        {
            bool Retval = false;
            switch ((UserModel.Rols)Rol!)
            {
                case Rols.superUser:
                case Rols.admin:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isMainboard()
        {
            bool Retval = false;
            switch ((UserModel.Rols)Rol!)
            {
                case Rols.superUser:
                case Rols.admin:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isMainboardOrAccounts()
        {
            bool Retval = false;
            switch ((UserModel.Rols)Rol!)
            {
                case Rols.superUser:
                case Rols.admin:
                case Rols.accounts:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isStaff(Rols rol = Rols.notSet)
        {
            if (rol == Rols.notSet && Rol != null)
                rol = (Rols)Rol;

            bool oRetVal = false;
            switch (rol)
            {
                case Rols.superUser:
                case Rols.admin:
                case Rols.salesManager:
                case Rols.accounts:
                case Rols.logisticManager:
                case Rols.marketing:
                case Rols.operadora:
                case Rols.comercial:
                case Rols.taller:
                    {
                        oRetVal = true;
                        break;
                    }
            }
            return oRetVal;
        }

        public bool isRep(Rols rol = Rols.notSet)
        {
            if (rol == Rols.notSet && Rol != null)
                rol = (Rols)Rol;

            bool oRetVal = false;
            switch (rol)
            {
                case Rols.rep:
                case Rols.comercial:
                    {
                        oRetVal = true;
                        break;
                    }
            }

            return oRetVal;
        }

        public bool IsCustomer(Rols? rol = Rols.notSet)
        {
            if (rol == Rols.notSet && Rol != null)
                rol = (Rols)Rol;

            bool oRetVal = false;
            switch (rol)
            {
                case Rols.cliFull:
                case Rols.cliLite:
                    {
                        oRetVal = true;
                        break;
                    }
            }

            return oRetVal;
        }
        public bool isManufacturer(Rols rol = Rols.notSet)
        {
            if (rol == Rols.notSet && Rol != null)
                rol = (Rols)Rol;

            bool oRetVal = false;
            switch (rol)
            {
                case Rols.manufacturer:
                    {
                        oRetVal = true;
                        break;
                    }
            }

            return oRetVal;
        }

        public bool isProfesional()
        {
            bool retval = false;
            switch ((UserModel.Rols)Rol!)
            {
                case Rols.denied:
                case Rols.guest:
                case Rols.lead:
                case Rols.notSet:
                case Rols.unregistered:
                    {
                        break;
                    }

                default:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }

        #endregion

        public Claim[] Claims()
        {
            var empsList = Emps.Select(x => (int)x).ToList();
            string empsJson = JsonConvert.SerializeObject(empsList);
            var retval = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, "M+O"), 
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", Guid.ToString()),
                        new Claim("DisplayName", Nickname ?? NomiCognoms() ?? ""),
                        new Claim("Email", EmailAddress ?? ""),
                        new Claim("Rol",Rol?.ToString() ?? "" ),
                        new Claim("Hash",Hash?.ToString() ?? "" ),
                        new Claim("Emps",empsJson, JsonClaimValueTypes.JsonArray)
                    };
            return retval;
        }

        public ClaimsPrincipal ClaimsPrincipal() => new ClaimsPrincipal(new ClaimsIdentity(Claims(), "CustomAuth"));
    }
}
