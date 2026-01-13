using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class CredencialModel:BaseGuid, IModel
    {
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? Referencia { get; set; } = "(nova credencial)";

        [StringLength(100, ErrorMessage = "Url is too long.")]
        public string? Url { get; set; }

        [StringLength(100, ErrorMessage = "User name is too long.")]
        public string? Usuari { get; set; }

        [StringLength(100, ErrorMessage = "Password is too long.")]
        public string? Password { get; set; }

        public List<UserModel.Rols> Rols { get; set; } = new();
        public List<UserModel> Owners { get; set; } = new();


        public string? Obs { get; set; }
        public UsrLogModel UsrLog { get; set; } = new();

        public CredencialModel() : base() { }
        public CredencialModel(Guid guid) : base(guid) { }

        public static CredencialModel Factory(UserModel user)
        {
            var retval = new CredencialModel();
            retval.UsrLog = UsrLogModel.Factory(user);
            retval.Owners.Add(user);
            return retval;
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Referencia ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string PropertyPageUrl() => string.Format("/PgCredencial/{0}",Guid.ToString());
        public string Caption() => Referencia ?? "?";
        public override string ToString() => string.Format("CredencialModel: {0}", Referencia);

    }
}
