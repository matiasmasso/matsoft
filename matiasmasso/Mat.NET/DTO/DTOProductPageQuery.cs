namespace DTO
{
    public class DTOProductPageQuery
    {
        public DTOProduct Product { get; set; }
        public DTOProduct.Tabs Tab { get; set; }
        public DTOLocation Location { get; set; }
        public string Aux { get; set; }
    }
}
