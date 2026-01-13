using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOXecsPresentacio
    {
        public DTOUser User { get; set; }
        public DTOCca Cca { get; set; }
        public DateTime Fch { get; set; }
        public DTOBanc Banc { get; set; }
        public DTOXec.ModalitatsPresentacio Modalitat { get; set; }
        public List<DTOXec> Xecs { get; set; }

        public DTOXecsPresentacio() : base()
        {
            Xecs = new List<DTOXec>();
        }

        public static DTOXecsPresentacio Factory(DTOUser oUser, DateTime DtFch, DTOBanc oBanc = null/* TODO Change to default(_) if this is not a reference type */, DTOXec.ModalitatsPresentacio oModalitat = DTOXec.ModalitatsPresentacio.NotSet)
        {
            DTOXecsPresentacio retval = new DTOXecsPresentacio();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Fch = DtFch;
                withBlock.Banc = oBanc;
                withBlock.Modalitat = oModalitat;
            }
            return retval;
        }

        public static DTOXecsPresentacio Factory(DTOCca oCca, DTOUser oUser, List<DTOXec> oXecs)
        {
            var retval = DTOXecsPresentacio.Factory(oUser, oCca.Fch);
            {
                var withBlock = retval;
                withBlock.Xecs = oXecs;
                if (withBlock.Xecs.Count > 0)
                {
                    DTOXec oFirstXec = withBlock.Xecs.First();
                    withBlock.Banc = oFirstXec.NBanc;
                    withBlock.Modalitat = oFirstXec.CodPresentacio;
                }
            }
            return retval;
        }
    }
}
