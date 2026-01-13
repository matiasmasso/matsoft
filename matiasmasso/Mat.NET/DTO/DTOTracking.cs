using System;
using System.Collections.Generic;
using System.Linq;


namespace DTO
{
    public class DTOTracking : DTOBaseGuid
    {
        public DTOCod Cod { get; set; }
        public DTOGuidNom Target { get; set; }
        public String Obs { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }

        public DTOTracking() : base()
        {
            this.UsrLog = new DTOUsrLog2();
        }

        public DTOTracking(Guid oGuid) : base(oGuid)
        {
            this.UsrLog = new DTOUsrLog2();
        }

        static public DTOTracking Factory(DTOCod.Wellknowns cod, DTOUser user, String obs = null)
        {
            DTOTracking retval = new DTOTracking();
            retval.Cod = DTOCod.Wellknown(cod);
            retval.UsrLog = DTOUsrLog2.Factory(user);
            retval.Obs = obs;
            return retval;
        }

        public class Collection : List<DTOTracking>
        {
            public bool isAlreadyRead()
            {
                bool retval = this.Any(x => x.Cod.Equals(DTOCod.Wellknown(DTOCod.Wellknowns.Read)));
                return retval;
            }
        }


    }
}
