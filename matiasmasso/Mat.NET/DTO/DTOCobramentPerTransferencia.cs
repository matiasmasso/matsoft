using System;

namespace DTO
{
    public class DTOCobramentPerTransferencia
    {
        public DTOUser User { get; set; }
        public DTODelivery Delivery { get; set; }
        public DateTime Fch { get; set; }
        public DTOContact Contact { get; set; }
        public DTOBanc Banc { get; set; }
        public string Concepte { get; set; }
        public DTOAmt Amt { get; set; }
        public DTODocFile DocFile { get; set; }

        public static DTOCobramentPerTransferencia Factory(DTOUser user, DTODelivery delivery, DateTime fch, DTOContact contact, DTOBanc banc, string concepte, DTOAmt amt, DTODocFile docFile)
        {
            DTOCobramentPerTransferencia retval = new DTOCobramentPerTransferencia();
            {
                var withBlock = retval;
                withBlock.User = user;
                withBlock.Delivery = delivery;
                withBlock.Fch = fch;
                withBlock.Contact = contact;
                withBlock.Banc = banc;
                withBlock.Concepte = concepte;
                withBlock.Amt = amt;
                withBlock.DocFile = docFile;
            }
            return retval;
        }
    }
}
