using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class OverStocksModel
    {
        public List<Brand> Brands { get; set; }

        public OverStocksModel()
        {
            Brands = new List<Brand>();
        }

        public class Brand : GuidNom
        {
            public List<Category> Categories { get; set; }
            public Brand()
            {
                Categories = new List<Category>();
            }

        }

        public class Category : GuidNom
        {
            public List<Sku> Skus { get; set; }
            public Category()
            {
                Skus = new List<Sku>();
            }

        }

        public class Sku : GuidNom
        {
            public string Id { get; set; }
            public string Ref { get; set; }
            public int Stock { get; set; }
            public decimal Sales { get; set; }
            public decimal Cost { get; set; }
        }

        public class GuidNom
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }

        }

    }
}
