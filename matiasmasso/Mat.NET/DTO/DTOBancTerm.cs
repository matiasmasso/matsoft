using MatHelperStd;
using System;

namespace DTO
{
    public class DTOBancTerm : DTOBaseGuid
    {
        public DTOBanc Banc { get; set; }
        public Targets Target { get; set; }
        public DateTime Fch { get; set; }
        public bool IndexatAlEuribor { get; set; }
        public decimal Diferencial { get; set; }
        public decimal MinimDespesa { get; set; }
        public decimal EuriborValue { get; set; }


        public enum Targets
        {
            notSet,
            anticips,
            gestioCobrament
        }

        public DTOBancTerm() : base()
        {
        }

        public DTOBancTerm(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOAmt cost(DTOCsb oCsb, DTOBancTerm oTerm, DateTime DtFch = default(DateTime))
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            int iDays = TimeHelper.daysdiff(DtFch, oCsb.Vto);

            decimal DcTipus = oTerm.Diferencial;
            if (oTerm.IndexatAlEuribor)
                DcTipus += oTerm.EuriborValue;
            decimal DcCost = oCsb.Amt.Eur * (DcTipus / 100) * (iDays / 360);
            if (DcCost < oTerm.MinimDespesa)
                DcCost = oTerm.MinimDespesa;

            DTOAmt retval = DTOAmt.Factory(DcCost);
            return retval;
        }
    }
}
