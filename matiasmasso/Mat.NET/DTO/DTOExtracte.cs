using MatHelperStd;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOExtracte
    {
        public DTOExercici Exercici { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOContact Contact { get; set; }
        public List<DTOCcb> Ccbs { get; set; }
        public List<int> Years { get; set; }

        public static DTOExtracte Factory(DTOExercici oExercici, DTOPgcCta oCta, DTOContact oContact)
        {
            DTOExtracte retval = new DTOExtracte();
            {
                var withBlock = retval;
                withBlock.Exercici = oExercici;
                withBlock.Cta = oCta;
                withBlock.Contact = oContact;
            }
            return retval;
        }

        public static string Filename(List<DTOCcb> oCcbs, DTOLang oLang)
        {
            string retval = "M+O.extracte.xlsx";
            if (oCcbs.Count > 0)
            {
                DTOCcb oCcb = oCcbs.First();
                DTOPgcCta oCta = oCcb.Cta;
                string sCta = DTOPgcCta.FullNom(oCta, oLang);
                DTOContact oContact = oCcb.Contact;
                if (oContact == null)
                    retval = string.Format("M+O.extracte.{0}.xlsx", sCta);
                else
                {
                    string sContact = TextHelper.CleanForUrl(oContact.FullNom);
                    if (sContact == "")
                        retval = string.Format("M+O.extracte.{0}.xlsx", sCta);
                    else
                        retval = string.Format("M+O.extracte.{0}.{1}.xlsx", sCta, sContact).Replace(TextHelper.VbChr(34), "");
                }
            }
            return retval;
        }
    }
}
