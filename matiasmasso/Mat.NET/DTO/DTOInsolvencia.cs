using System;

namespace DTO
{
    public class DTOInsolvencia : DTOBaseGuid
    {
        public DTOCustomer Customer { get; set; }
        public DateTime Fch { get; set; }
        public DTOAmt Nominal { get; set; }

        public DTOInsolvencia() : base()
        {
        }

        public DTOInsolvencia(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOInsolvencia Factory()
        {
            DTOInsolvencia retval = new DTOInsolvencia();
            {
                var withBlock = retval;
                withBlock.Fch = DTO.GlobalVariables.Today();
            }
            return retval;
        }
    }
}
