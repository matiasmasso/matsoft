namespace DTO
{
    public class DTOTaxBaseQuota
    {
        public DTOTax tax { get; set; }
        public DTOAmt baseImponible { get; set; }

        public DTOAmt quota { get; set; }

        public DTOTaxBaseQuota(DTOTax.Codis oTaxCodi, decimal DcBase, decimal DcTipus, decimal DcQuota = 0) : base()
        {
            tax = new DTOTax();
            tax.codi = oTaxCodi;
            tax.tipus = DcTipus;
            baseImponible = DTOAmt.Factory(DcBase);
            if (DcQuota == 0)
                quota = baseImponible.Percent(tax.tipus);
            else
                quota = DTOAmt.Factory(DcQuota);
        }

        public DTOTaxBaseQuota(DTOTax oTax, DTOAmt oBaseImponible) : base()
        {
            tax = oTax;
            baseImponible = oBaseImponible.Clone();
            quota = baseImponible.Percent(tax.tipus);
        }

        public DTOTaxBaseQuota() : base()
        {
        }

        public void addBase(DTOAmt oBase)
        {
            baseImponible.Add(oBase);
            quota = baseImponible.Percent(tax.tipus);
        }
    }
}
