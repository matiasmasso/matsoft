using MatHelperStd;
using System;

namespace DTO
{
    public class DTOCliReturn : DTOBaseGuid
    {
        public DTOCustomer Customer { get; set; }
        public DTOMgz Mgz { get; set; }
        public DateTime Fch { get; set; }
        public string RefMgz { get; set; }
        public int Bultos { get; set; }
        public DTODelivery Entrada { get; set; }
        public string Auth { get; set; }
        public string Obs { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public DTOCliReturn() : base()
        {
        }

        public DTOCliReturn(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOCliReturn Factory(DTOUser oUser)
        {
            DTOCliReturn retval = new DTOCliReturn();
            {
                var withBlock = retval;
                withBlock.Mgz = oUser.Emp.Mgz;
                withBlock.Auth = TextHelper.RandomString(6).ToUpper();
                withBlock.Bultos = 1;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }
    }
}
