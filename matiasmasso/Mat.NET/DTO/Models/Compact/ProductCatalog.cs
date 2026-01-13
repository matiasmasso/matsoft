using System;
using System.Collections.Generic;

namespace DTO.Models.Compact
{
    public class ProductCatalog
    {
        public DateTime FchCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<ProductBrand> Brands { get; set; }

        public ProductCatalog()
        {
            Brands = new List<ProductBrand>();
            FchCreated = DTO.GlobalVariables.Now();
        }
    }
}
