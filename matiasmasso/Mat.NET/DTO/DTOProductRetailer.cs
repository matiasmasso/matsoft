using System.Collections.Generic;

namespace DTO
{
    public class DTOProductRetailer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCod { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Tel { get; set; }
        public List<DTOProductSku> Items { get; set; }
    }
}
