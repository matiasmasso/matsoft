using System;

namespace DTO
{
    public class DTOGuidNomAmt : DTOGuidNom
    {
        public DTOAmt Amt { get; set; }

        public DTOGuidNomAmt(Guid oGuid) : base(oGuid)
        {
        }

        public DTOGuidNomAmt() : base()
        {
        }
    }
}
