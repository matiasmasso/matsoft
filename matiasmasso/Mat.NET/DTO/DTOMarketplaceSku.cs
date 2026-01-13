using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOMarketplaceSku
    {
        public DTOGuidNom Sku { get; set; }
        public bool Enabled { get; set; }
        public string CustomId{ get; set; }
        public DTOGuidNom MarketPlace { get; set; }
        public bool HasImg { get; set; }
        public bool HasTxt { get; set; }

    }
}
