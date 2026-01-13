using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class ShoppingBasket
    {
        public ShoppingBasket()
        {
            ShoppingBasketItems = new HashSet<ShoppingBasketItem>();
        }

        public Guid Guid { get; set; }
        public DateTime Fch { get; set; }
        public string Lang { get; set; } = null!;
        public Guid UserAccount { get; set; }
        public Guid MarketPlace { get; set; }
        public string? OrderNum { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }
        public Guid? Country { get; set; }
        public string? ZipCod { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? TrpObs { get; set; }
        public decimal? Amount { get; set; }

        public virtual ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
    }
}
