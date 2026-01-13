using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCcd
    {
        public DTOExercici Exercici { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOContact Contact { get; set; }
        public DateTime Fch { get; set; }
        public List<DTOCcb> Ccbs { get; set; }

        public DTOCcd() : base()
        {
        }

        public static DTOCcd Factory(DTOExercici oExercici, DTOPgcCta oCta, DTOContact oContact, DateTime FchTo = default(DateTime))
        {
            DTOCcd retval = new DTOCcd();
            {
                var withBlock = retval;
                withBlock.Exercici = oExercici;
                withBlock.Cta = oCta;
                withBlock.Contact = oContact;
                withBlock.Fch = FchTo;
            }
            return retval;
        }

        public bool Unequals(DTOCcb oCcb)
        {
            bool retval;
            if (oCcb.Contact == null)
                retval = UnEquals(oCcb.Cca.Exercici, oCcb.Cta.Guid, null/* TODO Change to default(_) if this is not a reference type */);
            else
                retval = UnEquals(oCcb.Cca.Exercici, oCcb.Cta.Guid, oCcb.Contact.Guid);
            return retval;
        }

        public bool UnEquals(DTOExercici oExercici, object oCtaGuid, object oContactGuid)
        {
            bool retval = true;
            if (Exercici != null)
            {
                if (Exercici.Equals(oExercici))
                {
                    if (oCtaGuid == null)
                        retval = Cta != null;
                    else if (!Convert.IsDBNull(oCtaGuid))
                    {
                        if (oCtaGuid is System.Guid)
                        {
                            if (oCtaGuid.Equals(Cta.Guid))
                            {
                                if (Contact == null)
                                {
                                    if (Convert.IsDBNull(oContactGuid))
                                        retval = false;
                                    else
                                        retval = oContactGuid != null;
                                }
                                else if (!Convert.IsDBNull(oContactGuid))
                                {
                                    if (oContactGuid is Guid)
                                        retval = !oContactGuid.Equals(Contact.Guid);
                                }
                            }
                        }
                    }
                }
            }
            return retval;
        }


        public static string FullNom(DTOCcd oCcd, DTOLang oLang)
        {
            string retval = string.Format("{0}{1:00000} {2} {3}", oCcd.Cta.Id, oCcd.Contact.Id, oCcd.Cta.Nom.Tradueix(oLang), oCcd.Contact.Nom);
            return retval;
        }
    }
}
