using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBasicCatalog : List<DTOBasicCatalog.Brand>
    {

        public class Brand
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Category> Categories { get; set; }

            public Brand() : base()
            {
                this.Categories = new List<Category>();
            }
            public Brand(Guid guid, string nom) : base()
            {
                this.Guid = guid;
                this.Nom = nom;
                this.Categories = new List<Category>();
            }

            //public string Serialized()
            //{
            //    var serializer = new JavaScriptSerializer();
            //    string retval = serializer.Serialize(this);
            //    return retval;
            //}

        }
        public class Category
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Sku> Skus { get; set; }

            public Category() : base()
            {
                this.Skus = new List<Sku>();
            }
            public Category(Guid guid, string nom) : base()
            {
                this.Guid = guid;
                this.Nom = nom;
                this.Skus = new List<Sku>();
            }
        }

        public class Sku
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }

            public Sku() : base() { }
            public Sku(Guid guid, string nom) : base()
            {
                this.Guid = guid;
                this.Nom = nom;
            }
        }
    }
}
