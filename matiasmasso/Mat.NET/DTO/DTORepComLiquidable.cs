using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORepComLiquidable : DTOBaseGuid
    {
        public DTORep Rep { get; set; }
        public DTOInvoice Fra { get; set; }
        public DTORepLiq RepLiq { get; set; }
        public DTOAmt BaseFras { get; set; }
        public DTOAmt Comisio { get; set; }
        public string Obs { get; set; }
        public bool Liquidable { get; set; }

        public PaymentStatus Status { get; set; }

        public List<DTODeliveryItem> Items { get; set; }

        public enum PaymentStatus
        {
            NoProblem,
            Unpayments,
            Insolvent
        }

        public DTORepComLiquidable() : base()
        {
            Liquidable = true;
            Items = new List<DTODeliveryItem>();
        }

        public DTORepComLiquidable(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTODeliveryItem>();
        }
    }
}
