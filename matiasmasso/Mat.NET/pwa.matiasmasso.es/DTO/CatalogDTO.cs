using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CatalogDTO
    {
        public List<Brand> Brands { get; set; }
        public List<Dept> Depts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Sku> Skus { get; set; }

        public class Request
        {
            public int EmpId { get; set; }
        }

        public class Product
        {
            public Guid Guid { get; set; }
            public LangTextDTO Nom { get; set; }
            public bool Obsoleto { get; set; }
            public Cods Cod { get; set; }
            public enum Cods:int
            {
                Brand,
                Dept,
                Category,
                Sku
            }

            public override string ToString()
            {
                return Nom?.Esp ?? "{CatalogDTO.Product}";
            }
        }
        public class Brand:Product
        {
            public Brand() : base() { 
                Cod = Cods.Brand;
            }
        }

        public class Dept:Product
        {
            public Guid Brand { get; set; }
            public Dept() : base()
            {
                Cod = Cods.Dept;
            }
        }

        public class Category:Product
        {
            public Guid Brand { get; set; }
            public Guid? Dept { get; set; }
            public bool NoStk { get; set; }
            public Category() : base()
            {
                Cod = Cods.Category;
            }

        }

        public class Sku:Product
        {
            public Guid Category { get; set; }
            public LangTextDTO NomLlarg { get; set; }
            public bool IsBundle { get; set; }
            public bool NoStk { get; set; }
            public Sku() : base()
            {
                Cod = Cods.Sku;
            }

            public string ThumbnailUrl() => Globals.ApiUrl("productsku/thumbnail", Guid.ToString() +".jpg");
         }
    }

}
