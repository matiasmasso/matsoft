using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSpvIn : DTOBaseGuid
    {

        // Public Shadows Property Guid As Guid
        // Public Shadows Property Guid As Guid

        public DTOEmp emp { get; set; }
        public int id { get; set; }
        public DateTime fch { get; set; }
        public string expedicio { get; set; }
        public int bultos { get; set; }
        public int kg { get; set; }
        public decimal m3 { get; set; }
        public string obs { get; set; }
        public List<DTOSpv> spvs { get; set; }

        public int spvCount { get; set; }
        public int usrNum { get; set; }
        public DTOUser user { get; set; }

        public static DTOSpvIn Factory()
        {
            DTOSpvIn retval = new DTOSpvIn();
            retval.Guid = Guid.NewGuid();
            return retval;
        }

        public DTOSpvIn() : base()
        {
            spvs = new List<DTOSpv>();
        }

        public DTOSpvIn(Guid oGuid) : base(oGuid)
        {
            spvs = new List<DTOSpv>();
        }

        public static DTOSpvIn Factory(DTOUser oUser, DateTime DtFch)
        {
            DTOSpvIn retval = new DTOSpvIn();
            {
                var withBlock = retval;
                withBlock.user = oUser;
                withBlock.emp = oUser.Emp;
                withBlock.fch = DtFch;
            }
            return retval;
        }

        public static string deleteQuery(List<DTOSpvIn> src)
        {
            string retval = "";
            switch (src.Count)
            {
                case 0:
                    {
                        retval = "No hi han entrades sel·leccionades";
                        break;
                    }

                case 1:
                    {
                        retval = string.Format("retrocedim la entrada {0}?", src.First().id);
                        break;
                    }

                default:
                    {
                        retval = "retrocedim les entrades sel·leccionades?";
                        break;
                    }
            }
            return retval;
        }
    }
}
