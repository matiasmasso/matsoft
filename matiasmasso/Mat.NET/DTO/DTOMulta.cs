using System;

namespace DTO
{
    public class DTOMulta : DTOBaseGuid
    {
        public DTOContact Emisor { get; set; }
        public string Expedient { get; set; }
        public DTOGuidNom Subjecte { get; set; }
        public DateTime Fch { get; set; }
        public DateTime Vto { get; set; }
        public DateTime Pagat { get; set; }
        public DTOAmt Amt { get; set; }

        public DTOMulta() : base()
        {
        }

        public DTOMulta(Guid oGuid) : base(oGuid)
        {
        }
    }
}
