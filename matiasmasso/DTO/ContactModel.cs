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
        public List<ContactModel.Email> Emails { get; set; } = new();

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



        public class Telefon:BaseGuid, IModel
        {
            public CountryModel? Country { get; set; }
            //public string? Prefix { get; set; }
            public string? Number { get; set; }
            public string? Obs { get; set; }

            public const string DefaultPrefix = "34";

            public string? Prefixe() => Country?.PrefixeTelefonic?.Trim();

            public string? DialNumber()
            {
                var retval = string.Empty;
                if (string.IsNullOrEmpty(Prefixe()) || Prefixe() == DefaultPrefix)
                    retval = Number;
                else
                    retval = string.Format("+{0}{1}", Prefixe(), Number);
                return retval;
            }
            public override string ToString()
            {
                var retval = string.Empty;
                if (string.IsNullOrEmpty(Number))
                    retval = "{tel}";
                else if (string.IsNullOrEmpty(Prefixe()) || Prefixe() == DefaultPrefix)
                    retval = Number;
                else
                    retval = string.Format("+{0} {1}", Prefixe(), Number);

                if (!string.IsNullOrEmpty(Obs))
                    retval = $"{retval} ({Obs})";
                return retval;
            }

            public Telefon() : base() { }
            public Telefon(Guid guid) : base(guid) { }

        }

        public class Email : BaseGuid, IModel
        {
            public string? EmailAddress { get; set; }
            public UserModel.Rols? Rol { get; set; }
            public string? Obs { get; set; }
            public bool Obsoleto { get; set; }

            public Email() : base() { }
            public Email(Guid guid) : base(guid) { }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Obs))
                    return EmailAddress ?? "{ContactModel.Email}";
                else
                    return $"{EmailAddress} ({Obs})";
            }

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

        public string? BuildContactFullNom(List<ZipModel>? zips, List<LocationModel>? locations, List<ZonaModel>? zonas, List<CountryModel>? countries)
        {
            var retval = RaoSocial;
            if (!string.IsNullOrEmpty(NomComercial))
                retval = $"{RaoSocial} \"{NomComercial}\"";
            if (Address?.ZipGuid != null)
            {
                var zip = zips?.FirstOrDefault(x => x.Guid == Address.ZipGuid);
                var location = locations?.FirstOrDefault(x => x.Guid == zip?.Location);
                if (location != null)
                {
                    var zona = zonas?.FirstOrDefault(x => x.Guid == location.Zona);
                    var country = countries?.FirstOrDefault(x => x.Guid == zona?.Country);
                    if (country?.IsSpain() ?? true)
                    {
                        if (!string.IsNullOrEmpty(zona?.Nom) && zona.Nom != location!.Nom)
                            retval = $"{retval} ({location!.Nom},{zona!.Nom})";
                        else
                            retval = $"{retval} ({location!.Nom})";
                    }
                    else
                        retval = $"{retval} ({location!.Nom},{country!.Nom?.Tradueix(LangDTO.Cat())})";
                }
            }
            return retval;
        }

    }


}
