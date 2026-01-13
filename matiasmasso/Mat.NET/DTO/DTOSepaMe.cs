using System;

namespace DTO
{
    public class DTOSepaMe : DTOBaseGuid
    {
        public DTOBanc Banc { get; set; }
        public DTOContact Lliurador { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public string Ref { get; set; }
        public DTODocFile DocFile { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }


        public DTOSepaMe() : base() { }
        public DTOSepaMe(Guid guid) : base(guid) { }

        public static DTOSepaMe Factory(DTOBanc banc, DTOUser user)
        {
            DTOSepaMe retval = new DTOSepaMe();
            retval.Banc = banc;
            retval.FchFrom = DTO.GlobalVariables.Today();
            retval.UsrLog = DTOUsrLog2.Factory(user);
            return retval;
        }

    }
}
