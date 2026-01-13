using MatHelperStd;
using Newtonsoft.Json;
using System;

namespace DTO
{
    public class DTOBank : DTOBaseGuid
    {
        public DTOCountry Country { get; set; }
        public string Id { get; set; }
        public string RaoSocial { get; set; }
        public string NomComercial { get; set; }
        public string Swift { get; set; }
        public string Tel { get; set; }
        public string Web { get; set; }
        [JsonIgnore]
        public Byte[] Logo { get; set; }
        public bool SEPAB2B { get; set; }
        public bool Obsoleto { get; set; }

        public DTOBank() : base()
        {
        }

        public DTOBank(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOBank Factory(DTOCountry oCountry = null/* TODO Change to default(_) if this is not a reference type */, string Id = "")
        {
            DTOBank retval = new DTOBank();
            {
                var withBlock = retval;
                withBlock.Country = oCountry;
                withBlock.Id = Id;
            }
            return retval;
        }

        public static string NomComercialORaoSocial(DTOBank oBank)
        {
            string retval = "";
            if (oBank != null)
            {
                if (oBank.NomComercial == "")
                    retval = oBank.RaoSocial;
                else
                    retval = oBank.NomComercial;
            }
            return retval;
        }

        public bool isSepa()
        {
            bool retval = false;
            if (this.Country != null)
            {
                switch (this.Country.ExportCod)
                {
                    case DTOInvoice.ExportCods.nacional:
                    case DTOInvoice.ExportCods.intracomunitari:
                        {
                            retval = true;
                            break;
                        }
                }
            }
            return retval;
        }

        public static bool validateBIC(string src)
        {
            string pattern = "[A-Z]{6,6}[A-Z2-9][A-NP-Z0-9]([A-Z0-9]{3,3}){0,1}";
            bool retval = TextHelper.RegexMatch(src, pattern);
            return retval;
        }
    }

}
