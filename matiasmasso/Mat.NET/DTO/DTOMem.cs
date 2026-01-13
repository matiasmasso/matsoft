using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOMem : DTOBaseGuid
    {
        public DTOGuidNom Contact { get; set; }
        public DateTime Fch { get; set; }
        public string Text { get; set; }
        public Cods Cod { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }
        public List<DTODocFile> Docfiles { get; set; }

        public enum Cods
        {
            Despaitx,
            Rep,
            Impagos,
            NotSet
        }



        public DTOMem() : base()
        {
            UsrLog = new DTOUsrLog2();
            Docfiles = new List<DTODocFile>();
        }

        public DTOMem(Guid oGuid) : base(oGuid)
        {
            UsrLog = new DTOUsrLog2();
            Docfiles = new List<DTODocFile>();
        }

        public static DTOMem Factory(DTOUser user)
        {
            DTOMem retval = new DTOMem();
            retval.UsrLog = DTOUsrLog2.Factory(user);
            retval.Fch = DTO.GlobalVariables.Today();
            return retval;
        }

        public String Url(bool absoluteUrl = false)
        {
            String retval = DTOWebDomain.Default(absoluteUrl).Url("mem", this.Guid.ToString());
            return retval;
        }

    }


}
