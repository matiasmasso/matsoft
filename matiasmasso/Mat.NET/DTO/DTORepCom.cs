namespace DTO
{
    public class DTORepCom
    {
        public DTORep Rep { get; set; }
        public decimal Com { get; set; }

        public bool repCustom { get; set; } // Si es True vol dir que el representant o la comisió han estat assignats manualment, i els processos de validació no l'haurien de sobreescriure

        public DTORepCom() : base()
        {
        }
        public DTORepCom(DTORep oRep, decimal DcComisioPercent) : base()
        {
            Rep = oRep;
            Com = DcComisioPercent;
        }
        public static DTORepCom Factory(DTORep oRep, decimal DcComisioPercent)
        {
            DTORepCom retval = new DTORepCom();
            retval.Rep = oRep;
            retval.Com = DcComisioPercent;
            return retval;
        }

        public void Clear()
        {
            Rep = null;
            Com = 0;
            repCustom = true;
        }

        public static bool Equals(DTORepCom O1, DTORepCom O2)
        {
            bool retval = false;
            if (O1 == null)
            {
                if (O2 == null)
                    retval = true;
            }
            else if (O2 != null)
            {
                if (O1.Rep == null)
                {
                    if (O2.Rep == null)
                        retval = true;
                }
                else
                    retval = O1.Rep.Guid.Equals(O2.Rep.Guid) & O1.Com == O2.Com;
            }
            return retval;
        }
    }
}
