namespace DTO
{
    public class DTOSkuAmtOrigin
    {
        // per declaracio ranking Inem dels productes mes comprats al mes

        public DTOProductSku Sku { get; set; }
        public int Qty { get; set; }
        public DTOAmt Amt { get; set; }
        public DTOCountry Country { get; set; }
    }
}
