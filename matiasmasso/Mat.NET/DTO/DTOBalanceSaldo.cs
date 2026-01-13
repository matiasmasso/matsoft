using System;

namespace DTO
{
    public class DTOBalanceSaldo : DTOPgcCta
    {
        public decimal CurrentDeb { get; set; }
        public decimal CurrentHab { get; set; }
        public decimal PreviousDeb { get; set; }
        public decimal PreviousHab { get; set; }
        public DTOContact Contact { get; set; }

        public DTOBalanceSaldo() : base()
        {
        }

        public DTOBalanceSaldo(Guid oGuid) : base(oGuid)
        {
        }

        public bool IsDeutor()
        {
            return CurrentDeb > CurrentHab;
        }

        public bool IsCreditor()
        {
            return CurrentHab > CurrentDeb;
        }
    }
}
