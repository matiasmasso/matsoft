using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RepModel:BaseGuid,IModel
    {
        public string? Nom { get; set; }
        public string? Abr { get; set; }
        public string? Nif { get; set; }
        public string? Description { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }
        public AddressModel? Address { get; set; }
        public IbanModel? Iban { get; set; }
        public List<ContactModel.Telefon> Telefons { get; set; } = new();
        public List<UserModel> Emails { get; set; } = new();


        public RepModel() : base() { }
        public RepModel(Guid guid) : base(guid) { }

        public override bool Matches(string searchTerm)
        {
            return Abr?.Contains(searchTerm) ?? false;
        }

        public NavDTO ContextMenu()
        {
            var retval = new NavDTO();
            retval.AddItem(Globals.PageUrl("PgRep", Guid.ToString()), "Ficha", "Fitxa", "Details");
            retval.AddItem(Globals.PageUrl("CertificatsIrpf", Guid.ToString()), "Certs.Irpf", "Certs.Irpf", "Irpf certs.");
            return retval;
        }

        public string AbrOrNom() => string.IsNullOrEmpty(Abr) ? Nom ?? "" : Abr;
        public string PropertyPageUrl() => Globals.PageUrl("Rep",Guid.ToString());
        public override string ToString() => string.Format("Rep: {0}", Abr);

    }
}
