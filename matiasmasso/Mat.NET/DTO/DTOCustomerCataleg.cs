using MatHelperStd;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCustomerCataleg
    {
        public string UTC { get; set; }
        public string Customer { get; set; }
        public List<DTOCustomerCatalegItem> Items { get; set; }

        public DTOCustomerCataleg(DTOCustomer oCustomer) : base()
        {
            UTC = TextHelper.VbFormat(DTO.GlobalVariables.Now().ToUniversalTime(), "u");
            Customer = oCustomer.FullNom;
            Items = new List<DTOCustomerCatalegItem>();
        }
    }

    public class DTOCustomerCatalegItem
    {
        public Guid SkuGuid { get; set; }
        public int SkuId { get; set; }
        public string Ref { get; set; }
        public string Ean { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal RRPP { get; set; }
        public string Image { get; set; }
    }
}
