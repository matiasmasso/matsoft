using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOXec : DTOBaseGuid
    {
        public DTOContact Lliurador { get; set; }
        public DTOIban Iban { get; set; }
        public string XecNum { get; set; }
        public DateTime Vto { get; set; } = DateTime.MinValue;
        public DTOAmt Amt { get; set; }
        public List<DTOPnd> Pnds { get; set; }
        public List<DTOImpagat> Impagats { get; set; }
        public StatusCods StatusCod { get; set; }
        public DateTime FchRecepcio { get; set; }
        public DTOCca CcaRebut { get; set; }
        public ModalitatsPresentacio CodPresentacio { get; set; }
        public DTOCca CcaPresentacio { get; set; }
        public DTOBanc NBanc { get; set; }
        public DTOCca CcaVto { get; set; }
        public Formats Format { get; set; }

        public enum StatusCods
        {
            EnCartera,
            EnCirculacio,
            Vençut
        }

        public enum Formats
        {
            Xec,
            Pagare
        }

        public enum ModalitatsPresentacio
        {
            NotSet,
            A_la_Vista,
            Al_Cobro,
            Al_Descompte
        }

        public DTOXec() : base()
        {
        }

        public DTOXec(Guid oGuid) : base(oGuid)
        {
        }

    }
}
