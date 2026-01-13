using System;

namespace DTO
{
    public class DTOProductSkuExtended : DTOProductSku
    {
        public int UnitsInStock { get; set; }
        public int UnitsInClients { get; set; }
        public int UnitsInPot { get; set; }
        public int UnitsInProveidor { get; set; }
        public int UnitsInPrevisio { get; set; }
        public int Confirmed { get; set; }

        public DTOProductSkuExtended(Guid oGuid) : base(oGuid)
        {
        }
    }
}
