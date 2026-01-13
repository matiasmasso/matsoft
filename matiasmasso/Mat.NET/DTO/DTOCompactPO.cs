using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCompactPO // per entrada de comandes via Api
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Fch { get; set; }
        public GuidNom User { get; set; }
        public GuidNom Customer { get; set; }
        public string Nom { get; set; }
        public string Obs { get; set; }
        public decimal Eur { get; set; }

        public List<Item> Items { get; set; }

        public List<string> ValidationErrors { get; set; }

        public DTOCompactPO()
        {
            Items = new List<Item>();
            ValidationErrors = new List<string>();
        }

        public static DTOCompactPO Sample()
        {
            DTOCompactPO retval = new DTOCompactPO();
            {
                var withBlock = retval;
                withBlock.User = new GuidNom();
                withBlock.User.Guid = new Guid("9512706E-06AF-4859-B4AE-D639DEC471A7");
                withBlock.Customer = new GuidNom();
                withBlock.Customer.Guid = new Guid("34C6350A-CA3F-49B5-99B8-CD4D47B71B08"); // Zabala Hoyos
                withBlock.Fch = DTO.GlobalVariables.Today().ToString();
                withBlock.Items = new List<Item>();
                Item oItem = new Item();
                {
                    var withBlock1 = oItem;
                    withBlock1.Qty = 1;
                    withBlock1.Sku = new Item.productSku();
                    {
                        var withBlock2 = withBlock1.Sku;
                        withBlock2.Guid = new Guid("4E65539C-E3DC-4ED5-B753-01E486A044A7"); // Cocobelt Aztek
                    }
                    withBlock1.Price = 15;
                }
                withBlock.Items.Add(oItem);
            }
            return retval;
        }

        public class GuidNom
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
        }

        public class Item
        {
            public int Qty { get; set; }
            public decimal Price { get; set; }
            public productSku Sku { get; set; }

            public class productSku
            {
                public Guid Guid { get; set; }
                public string Ean { get; set; }
                public string Nom { get; set; }
                public int Moq { get; set; }
                public int Stock { get; set; }
            }
        }
    }
}
