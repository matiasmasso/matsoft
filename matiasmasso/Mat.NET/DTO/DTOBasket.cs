using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBasket
    {
        public Guid customer { get; set; }
        public string customerUrl { get; set; }
        public List<string> addressLines { get; set; }
        public string concept { get; set; }
        public bool totjunt { get; set; }
        public string fchmin { get; set; }
        public string obs { get; set; }
        public bool includePendingOrders { get; set; }
        public List<DTOBasketLine> lines { get; set; }
        public decimal suma { get; set; }
        public string sumaFormatted { get; set; }
        public int Id { get; set; }
        public bool mailConfirmation { get; set; }
        public DTOBasketCatalog Catalog { get; set; }
    }

    public class DTOBasketLine
    {
        public int qty { get; set; }
        public int availableStock { get; set; }
        public int pending { get; set; }
        public Guid sku { get; set; }
        public Guid category { get; set; }
        public Guid brand { get; set; }
        public string url { get; set; }
        public string nom { get; set; }
        public decimal price { get; set; }
        public decimal dto { get; set; }
        public decimal Amount { get; set; }
        public string priceFormatted { get; set; }
        public string amountFormatted { get; set; }

        public static DTOBasketLine Factory(DTOPurchaseOrderItem oItem)
        {
            DTOBasketLine retval = new DTOBasketLine();
            {
                var withBlock = retval;
                withBlock.sku = oItem.Sku.Guid;
                withBlock.category = oItem.Sku.Category.Guid;
                withBlock.brand = oItem.Sku.Category.Brand.Guid;
                withBlock.qty = oItem.Qty;
                withBlock.nom = oItem.Sku.NomLlarg.Esp;
                withBlock.price = oItem.Price.Eur;
                withBlock.priceFormatted = DTOAmt.CurFormatted(oItem.Price);
                withBlock.dto = oItem.Dto;
                withBlock.availableStock = Math.Min(oItem.Qty, oItem.Sku.Stock);
                withBlock.pending = oItem.Qty - withBlock.availableStock;
                var oAmt = oItem.Amount();
                withBlock.Amount = oAmt.Eur;
                withBlock.amountFormatted = DTOAmt.CurFormatted(oAmt);
            }
            return retval;
        }
    }
}
