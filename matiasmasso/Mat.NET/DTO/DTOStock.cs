namespace DTO
{
    public class DTOStock
    {
        public DTOProductSku Sku { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsInClients { get; set; }
        public int UnitsInPot { get; set; }
        public int UnitsInProveidor { get; set; }
        public int UnitsInPrevisio { get; set; }
    }
}
