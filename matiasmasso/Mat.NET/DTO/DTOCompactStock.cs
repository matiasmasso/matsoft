namespace DTO
{
    public class DTOCompactStock
    {
        public string Ref { get; set; }
        public string Ean { get; set; }
        public int Stock { get; set; }
        public decimal Cost { get; set; }
        public decimal Pvp { get; set; }
    }

    public class DTOCompactStockOnly
    {
        public string Ref { get; set; }
        public string Ean { get; set; }
        public int Stock { get; set; }
    }
}
