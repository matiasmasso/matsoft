using MatHelperStd;
using System;

namespace DTO
{
    public class DTOVisaCard : DTOBaseGuid
    {
        public DTOContact Titular { get; set; }
        public DTOVisaEmisor Emisor { get; set; }
        public string Digits { get; set; }
        public DTOBanc Banc { get; set; }
        public string Caduca { get; set; }
        public DateTime Baja { get; set; }
        public DTOAmt Limit { get; set; }
        public string Nom { get; set; }
        public string CCID { get; set; } // Card Security Code

        public DTOVisaCard() : base()
        {
        }

        public DTOVisaCard(Guid oGuid) : base(oGuid)
        {
        }

        public bool IsActive()
        {
            bool retval = true;
            string sCaducitat = Caduca;
            if (sCaducitat.Length == 4)
            {
                if (TextHelper.VbIsNumeric(sCaducitat) & sCaducitat != "0000")
                {
                    int iMes = sCaducitat.Substring(0, 2).toInteger();
                    if (iMes > 0 & iMes < 12)
                    {
                        int iYea = System.Convert.ToInt32("20" + sCaducitat.Substring(2));
                        if (iYea > 1985 & iYea < 2150)
                        {
                            DateTime FchTo = (new DateTime(iYea, iMes, 1)).AddMonths(1).AddDays(-1);
                            if (FchTo < DTO.GlobalVariables.Today())
                                retval = false;
                        }
                    }
                }
            }
            if (Baja != default(DateTime))
            {
                if (Baja < DTO.GlobalVariables.Today())
                    retval = false;
            }
            return retval;
        }
    }
}
