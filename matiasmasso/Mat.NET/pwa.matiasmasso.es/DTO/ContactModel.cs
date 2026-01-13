using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DTO
{
    public class ContactModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Id { get; set; }
        public string? FullNom { get; set; }
        public string? RaoSocial { get; set; }
        public string? NomComercial { get; set; }
        public string? SearchKey { get; set; }

        public string? SuProveedorNum { get; set; }
        public AddressModel? Address { get; set; } // TO DEPRECATE

        public string? Adr { get; set; }
        public Guid? Zip { get; set; }
        public NifList Nifs { get; set; } = new();
        public int Rol { get; set; }
        public Guid? ContactClass { get; set; }
        public Guid? Ccx { get; set; }
        public List<Telefon> Telefons { get; set; } = new();
        public List<UserModel> Emails { get; set; } = new();

        public bool Req { get; set; }

        public bool Obsoleto { get; set; }

        public ContactModel() : base() { }
        public ContactModel(Guid guid) : base(guid) { }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = FullNom + " " + SearchKey;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public GuidNom ToGuidNom() => new GuidNom(Guid,FullNom);

        public static string? RaoSocialAndNomComercial(string? raosocial, string? nomComercial)
        {
            var retval = string.Empty;
            if (string.IsNullOrEmpty(raosocial))
                retval = nomComercial;
            else if (string.IsNullOrEmpty(nomComercial))
                retval = raosocial;
            else
                retval = string.Format("{0} ({1})", raosocial, nomComercial);
            return retval;
        }

        public string? RaoSocialAndNomComercial() => RaoSocialAndNomComercial(RaoSocial,NomComercial);
        public string NomComercialOrRaoSocial() => string.IsNullOrEmpty(NomComercial) ? RaoSocial : NomComercial;
        public string EditorUrl() => String.Format("/contact/{0}", Guid.ToString());
        public override string ToString()
        {
            string? retval = FullNom;
            if (string.IsNullOrEmpty(retval)) retval = RaoSocial;
            if (string.IsNullOrEmpty(retval)) retval = NomComercial;
            if (string.IsNullOrEmpty(retval)) retval = "Contact?";
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("Contact", Guid.ToString());



        public class Telefon
        {
            public string? Prefix { get; set; }
            public string? Number { get; set; }
            public string? Obs { get; set; }

            public string Value() => string.Format("+{0}{1}", Prefix, Number);
            public  string Caption() => string.Format("+({0}) {1} {2}", Prefix, Number, Obs);
        }

        public class Email
        {
            public string? EmailAddress { get; set; }
            public string? Obs { get; set; }
            public  string Caption() => string.Format("{0} {1}", EmailAddress, Obs);
        }


        public class NifList:List<Nif>
        {
            public static NifList Factory(int? cod, string? nif, int? nif2cod, string? nif2)
            {
                var retval = new NifList();
                if (!string.IsNullOrEmpty(nif))
                    retval.Add(new Nif
                    {
                        Cod = (int)cod!,
                        Value = nif
                    });
                if (!string.IsNullOrEmpty(nif2))
                    retval.Add(new Nif
                    {
                        Cod = (int?)nif2cod,
                        Value = nif2
                    });
                return retval;
            }
            public override string ToString()
            {
                var retval = string.Empty;
                if (base.Count == 1)
                    retval = base[0].Value;
                else if (base.Count == 2)
                    retval =String.Format("{0}/{1}", base[0].Value, base[1].Value);
                return retval;
            }
        }

        public class Nif
        {
            public string? Value { get; set; }
            public int? Cod { get; set; }
            public enum Cods
            {
                Unknown,
                Nif,
                EORI,
                NRT, //Num.de registre Tributari Andorra
                NCM, //num.de comerç
                College,
                CR, //Certificat de Residencia (SII ID 4 per Windeln alemanya)
                Passport
            }


        }
    }


}
