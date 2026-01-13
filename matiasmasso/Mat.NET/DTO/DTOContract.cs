using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOContract : DTOBaseGuid
    {
        public DTOContractCodi Codi { get; set; }
        public string Nom { get; set; }
        public DTOContact Contact { get; set; } = null/* TODO Change to default(_) if this is not a reference type */;
        public string Num { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public bool Privat { get; set; } = false;
        public DTODocFile DocFile { get; set; }


        public DTOContract() : base()
        {
        }

        public DTOContract(Guid oGuid) : base(oGuid)
        {
        }

        public static string FullText(DTOContract oContract)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            {
                var withBlock = oContract;
                if (withBlock.Num.isNotEmpty())
                    sb.Append(withBlock.Num + " ");
                sb.Append(withBlock.FchFrom.ToShortDateString() + " ");
                sb.Append(withBlock.Contact.NomComercialOrDefault());
            }
            string retval = sb.ToString();
            return retval;
        }

        public string ContactNom()
        {
            string retval = "";
            if (Contact != null)
                retval = Contact.FullNom;
            return retval;
        }

        public class Collection : List<DTOContract>
        {

        }
    }
}
