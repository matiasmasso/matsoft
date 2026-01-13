using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCompactCustomerPO
    {
        public Guid User { get; set; }
        public Guid Customer { get; set; }
        public string Concept { get; set; }
        public List<Item> Items { get; set; }

        public class Item
        {
            public int Qty { get; set; }
            public string Ean { get; set; }
            public decimal Price { get; set; }
            public decimal Dto { get; set; }
        }

        public DTOCompactCustomerPO() : base()
        {
            Items = new List<Item>();
        }

        public static DTOCompactCustomerPO Factory(DTOPurchaseOrder oPurchaseOrder)
        {
            DTOCompactCustomerPO retval = new DTOCompactCustomerPO();
            {
                var withBlock = retval;
                withBlock.User = oPurchaseOrder.UsrLog.UsrCreated.Guid;
                withBlock.Customer = oPurchaseOrder.Customer.Guid;
                withBlock.Concept = oPurchaseOrder.Concept;
                foreach (var oPnc in oPurchaseOrder.Items)
                {
                    DTOCompactCustomerPO.Item item = new DTOCompactCustomerPO.Item();
                    {
                        var withBlock1 = item;
                        withBlock1.Ean = DTOEan.eanValue(oPnc.Sku.Ean13);
                        withBlock1.Qty = oPnc.Qty;
                        withBlock1.Price = oPnc.Price.Eur;
                        withBlock1.Dto = oPnc.Dto;
                    }
                    withBlock.Items.Add(item);
                }
            }
            return retval;
        }
    }
}
