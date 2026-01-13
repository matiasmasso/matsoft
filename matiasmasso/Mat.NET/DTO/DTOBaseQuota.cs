using System;

namespace DTO
{
    public class DTOBaseQuota
    {
        public object source { get; set; }
        public DTOAmt baseImponible { get; set; }
        public DTOAmt quota { get; set; }
        public decimal tipus { get; set; }

        public DTOBaseQuota(DTOAmt oBase, decimal DcTipus = 0, DTOAmt oQuota = null/* TODO Change to default(_) if this is not a reference type */) : base()
        {
            baseImponible = oBase;
            if (DcTipus != 0)
            {
                tipus = DcTipus;
                if (oQuota == null)
                    quota = oBase.Percent(DcTipus);
                else
                    quota = oQuota;
            }
        }

        public void calcTipus()
        {
            if (baseImponible == null)
                tipus = 0;
            else if (baseImponible.Eur == 0)
                tipus = 0;
            else
                tipus = 100 * Math.Round(quota.Eur / baseImponible.Eur, baseImponible.Cur.Decimals, MidpointRounding.AwayFromZero);
        }
    }
}
