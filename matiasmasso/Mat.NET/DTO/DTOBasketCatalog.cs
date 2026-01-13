using System;
using System.Collections.Generic;

namespace DTO
{

    public class DTOBasketCatalog
    {
        public List<Brand> brands { get; set; }

        public DTOBasketCatalog()
        {
            brands = new List<Brand>();
        }

        public class Brand
        {
            public Guid Guid { get; set; }
            public string nom { get; set; }
            public List<Category> categories { get; set; }

            public Brand(Guid oguid, string sNom)
            {
                Guid = oguid;
                nom = sNom;
                categories = new List<Category>();
            }
        }

        public class Category
        {
            public Guid Guid { get; set; }
            public string nom { get; set; }
            public List<Sku> skus { get; set; }

            public Category(Guid oguid, string sNom)
            {
                Guid = oguid;
                nom = sNom;
                skus = new List<Sku>();
            }
        }

        public class Sku
        {
            public Guid Guid { get; set; }
            public string nomCurt { get; set; }
            public string nomLlarg { get; set; }
            public decimal price { get; set; }
            public decimal dto { get; set; }
            public int moq { get; set; }
            public int stock { get; set; }
            public Sku(Guid oguid, string sNomCurt, string sNomLlarg)
            {
                Guid = oguid;
                nomCurt = sNomCurt;
                nomLlarg = sNomLlarg;
            }
        }
    }
}

