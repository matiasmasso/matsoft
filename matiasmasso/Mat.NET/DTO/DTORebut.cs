using System;

namespace DTO
{
    public class DTORebut
    {
        public DTOLang Lang { get; set; }
        public string Id { get; set; }
        public DTOAmt Amt { get; set; }
        public DateTime Fch { get; set; }
        public DateTime Vto { get; set; }
        public string Concepte { get; set; }
        public string IbanDigits { get; set; }
        public string Nom { get; set; }
        public string Adr { get; set; }
        public string Cit { get; set; }

        public DTORebut(DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */, string sId = "", DTOAmt oAmt = null/* TODO Change to default(_) if this is not a reference type */, DateTime DtFch = default(DateTime), DateTime DtVto = default(DateTime), string sNom = "", string sAdr = "", string sCit = "", string sConcepte = "", string sIbanDigits = "") : base()
        {
            Lang = oLang;
            Id = sId;
            Amt = oAmt;
            Fch = DtFch;
            Vto = DtVto;
            Concepte = sConcepte;
            IbanDigits = sIbanDigits;
            Nom = sNom;
            Adr = sAdr;
            Cit = sCit;
        }
    }
}
