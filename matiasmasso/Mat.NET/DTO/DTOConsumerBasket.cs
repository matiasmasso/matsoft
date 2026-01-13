using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOConsumerBasket : DTOBaseGuid
    {
        public Sites Site { get; set; }
        public DateTime Fch { get; set; }
        public DTOUser User { get; set; }
        public List<DTOConsumerBasketItem> Items { get; set; }

        public enum Sites
        {
            NotSet,
            MMO,
            Britax,
            Inglesina,
            Thorley
        }

        public DTOConsumerBasket() : base()
        {
        }

        public DTOConsumerBasket(Guid oGuid) : base(oGuid)
        {
        }
    }

    public class DTOConsumerBasketItem
    {
        public int Qty { get; set; }
        public DTOProductSku Sku { get; set; }
        public DTOAmt Price { get; set; }
    }
}
