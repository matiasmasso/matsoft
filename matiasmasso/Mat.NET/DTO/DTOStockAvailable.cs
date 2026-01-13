namespace DTO
{
    public class DTOStockAvailable
    {
        public DTOProductSku Sku { get; set; }
        public int OriginalStock { get; set; }
        public int AvailableStock { get; set; }
        public int Clients { get; set; }
        public int Pendent { get; set; }
    }
}
