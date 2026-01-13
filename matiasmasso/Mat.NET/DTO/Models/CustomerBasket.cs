using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class CustomerBasket
    {
        public DTOCustomer Customer { get; set; }
        public CatalogModel Catalog { get; set; }
        public Dictionary<string, Decimal> TarifaDtos { get; set; }

        public CustomerBasket()
        {
            Catalog = new CatalogModel();
            TarifaDtos = new Dictionary<string, Decimal>();
        }
    }
}
