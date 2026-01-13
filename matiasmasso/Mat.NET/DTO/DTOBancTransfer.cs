using System;

namespace DTO
{
    public class DTOBancTransfer : DTOBaseGuid
    {
        public DTOBanc BancEmissor { get; set; }
        public DTOCca Cca { get; set; }
        public DTOContact Beneficiari { get; set; }
        public DTOBankBranch BeneficiariBankBranch { get; set; }
        public string Account { get; set; }
        public DTOAmt Amt { get; set; }
        public string Concepte { get; set; }

        public DTOBancTransfer() : base()
        {
        }

        public DTOBancTransfer(Guid oGuid) : base(oGuid)
        {
        }
    }

    public class DTOBancTransferItem : DTOCcb
    {
        public DTOIban Iban { get; set; }
        public string Concept { get; set; }
    }

}
