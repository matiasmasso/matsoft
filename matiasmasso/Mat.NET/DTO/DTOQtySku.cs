namespace DTO
{
    public class DTOQtySku
    {
        public int Qty { get; set; }
        public DTOProductSku Sku { get; set; } = null/* TODO Change to default(_) if this is not a reference type */;

        public DTOQtySku(int iQty, DTOProductSku oSku) : base()
        {
            Qty = iQty;
            Sku = oSku;
        }
    }
}
