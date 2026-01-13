using System;

namespace DTO
{
    public class DTOCliCreditLog : DTOBaseGuid
    {
        public DTOCustomer Customer { get; set; }
        public DateTime FchCreated { get; set; }
        public DTOUser UsrCreated { get; set; }
        public DateTime FchLastEdited { get; set; }
        public DTOUser UsrLastEdited { get; set; }
        public string Obs { get; set; }
        public Cods Cod { get; set; }
        public DTOAmt Amt { get; set; }

        public enum Cods
        {
            NotSet,
            Caducat
        }

        public DTOCliCreditLog() : base()
        {
        }

        public DTOCliCreditLog(Guid oGuid) : base(oGuid)
        {
        }
    }
}
