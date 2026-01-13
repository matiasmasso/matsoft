using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class CatalogModel
    {
        public List<Brand> Brands { get; set; }
        public List<Models.SkuStock> Stocks { get; set; }
        public Dictionary<Guid, String> Prices { get; set; }


        public CatalogModel()
        {
            Brands = new List<Brand>();
            Stocks = new List<Models.SkuStock>();
            Prices = new Dictionary<Guid, String>();
        }

        public void AddProduct(DTOProduct value)
        {
            Brand brand = null;
            Category category = null;
            switch (value.SourceCod)
            {
                case DTOProduct.SourceCods.Brand:
                    BrandOrCreate(value.Guid, value.Nom.Esp);
                    break;
                case DTOProduct.SourceCods.Category:
                    DTOProductCategory oCategory = (DTOProductCategory)value;
                    brand = BrandOrCreate(oCategory.Brand.Guid, oCategory.Brand.Nom.Esp);
                    category = CategoryOrCreate(brand, value.Guid, value.Nom.Esp);
                    //brand.Categories.Add(category);
                    break;
                case DTOProduct.SourceCods.Sku:
                    DTOProductSku oSku = (DTOProductSku)value;
                    brand = BrandOrCreate(oSku.Category.Brand.Guid, oSku.Category.Brand.Nom.Esp);
                    category = CategoryOrCreate(brand, oSku.Category.Guid, oSku.Category.Nom.Esp);
                    Sku sku = SkuOrCreate(category, oSku.Guid, oSku.Nom.Esp);
                    //category.Skus.Add(sku);
                    break;
            }
        }

        public Brand BrandOrCreate(Guid guid, string nom)
        {
            Brand retval = Brands.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
            {
                retval = new Brand(guid, nom);
                Brands.Add(retval);
            }
            return retval;
        }
        public Category CategoryOrCreate(Brand brand, Guid guid, string nom)
        {
            Category retval = brand.Categories.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
            {
                retval = new Category(guid, nom);
                brand.Categories.Add(retval);
            }
            return retval;
        }
        public Sku SkuOrCreate(Category category, Guid guid, string nom)
        {
            Sku retval = category.Skus.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
            {
                retval = new Sku(guid, nom);
                category.Skus.Add(retval);
            }
            return retval;
        }

        public string ProductFullNom(Guid guid)
        {
            List<string> parameters = new List<string>();
            string brandNom = BrandNom(guid);
            string categoryNom = CategoryNom(guid);
            string skuNom = SkuNom(guid);
            if (!string.IsNullOrEmpty(brandNom))
                parameters.Add(brandNom);
            if (!string.IsNullOrEmpty(categoryNom))
                parameters.Add(categoryNom);
            if (!string.IsNullOrEmpty(skuNom))
                parameters.Add(skuNom);
            string retval = string.Join(" ", parameters);
            return retval;
        }

        public string BrandNom(Guid guid)
        {
            Brand brand = Brands.FirstOrDefault(x => x.Guid.Equals(guid)
            || x.Categories.Any(y => y.Guid.Equals(guid))
            || x.Categories.SelectMany(z => z.Skus).Any(q => q.Guid.Equals(guid))
            );

            string retval = "";
            if (brand != null)
                retval = brand.Nom;
            return retval;
        }

        public string CategoryNom(Guid guid)
        {
            string retval = "";
            Category category = Brands.SelectMany(x => x.Categories).FirstOrDefault(y => y.Guid.Equals(guid)
            || y.Skus.Any(z => z.Guid.Equals(guid))
            );

            if (category != null)
                retval = category.Nom;
            return retval;
        }

        public string SkuNom(Guid guid)
        {
            string retval = "";
            Sku sku = Brands.SelectMany(x => x.Categories).SelectMany(y => y.Skus).FirstOrDefault(z => z.Guid.Equals(guid));
            if (sku != null)
                retval = sku.Nom;
            return retval;
        }

        public class Brand : DTOGuidNom.Compact
        {
            public List<Category> Categories { get; set; }

            public Brand()
            {
                Categories = new List<Category>();
            }

            public Brand(Guid guid, string nom = "")
            {
                Guid = guid;
                Nom = nom;
                Categories = new List<Category>();
            }

        }
        public class Category : DTOGuidNom.Compact
        {
            public List<Sku> Skus { get; set; }
            public Category()
            {
                Skus = new List<Sku>();
            }

            public Category(Guid guid, string nom = "")
            {
                Guid = guid;
                Nom = nom;
                Skus = new List<Sku>();
            }

        }

        public class Sku : DTOGuidNom.Compact
        {
            public Sku()
            {
            }

            public Sku(Guid guid, string nom = "")
            {
                Guid = guid;
                Nom = nom;
            }
        }

        public class SkuExtended : Sku
        {
            public Decimal Rrpp { get; set; }
            public Decimal Price { get; set; }
            public int Moq { get; set; }
            public SkuExtended()
            {
            }

            public SkuExtended(Guid guid, string nom = "")
            {
                Guid = guid;
                Nom = nom;
            }
        }

    }
}
