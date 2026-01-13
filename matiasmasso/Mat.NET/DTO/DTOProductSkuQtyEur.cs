using System;

namespace DTO
{
    public class DTOProductSkuQtyEur : DTOProductSku
    {
        public int Qty { get; set; }
        public DTOAmt Amt { get; set; }

        public DTOProductSkuQtyEur(Guid oGuid) : base(oGuid)
        {
        }
    }
}
