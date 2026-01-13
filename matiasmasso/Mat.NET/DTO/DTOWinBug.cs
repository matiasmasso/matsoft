using System;

namespace DTO
{
    public class DTOWinBug : DTOBaseGuid
    {
        public DTOUser User { get; set; }
        public DateTime Fch { get; set; }
        public string Obs { get; set; }


        public DTOWinBug() : base()
        {
        }

        public DTOWinBug(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOWinBug Factory(string sObs, DTOUser oUser = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOWinBug retval = new DTOWinBug();
            {
                var withBlock = retval;
                withBlock.Fch = DTO.GlobalVariables.Now();
                withBlock.Obs = sObs;
                withBlock.User = oUser;
            }
            return retval;
        }
    }
}
