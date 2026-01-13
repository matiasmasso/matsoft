using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOStatement
    {
        public List<DTOPgcCta> Ctas { get; set; }
        public List<DTOGuidNom.Compact> Contacts { get; set; }
        public List<Item> Items { get; set; }

        public DTOStatement()
        {
            Ctas = new List<DTOPgcCta>();
            Contacts = new List<DTOGuidNom.Compact>();
            Items = new List<Item>();
        }

        public class Item
        {
            public Guid CcbGuid { get; set; }
            public Guid CcaGuid { get; set; }
            public Guid CtaGuid { get; set; }
            public Guid PndGuid { get; set; }
            public string Hash { get; set; }
            public DateTime Fch { get; set; }
            public int CcaId { get; set; }
            public DTOCca.CcdEnum Ccd { get; set; }
            public string Concept { get; set; }
            public DTOAmt.Compact Amt { get; set; }
            public DTOCcb.DhEnum Dh { get; set; }

            public DTOCcb Ccb()
            {
                DTOCcb retval = new DTOCcb(CcbGuid);
                retval.Cca = Cca();
                retval.Cta = new DTOPgcCta(CtaGuid);
                retval.Amt = Amt.ToAmt();
                retval.Dh = Dh;
                if (!PndGuid.Equals(Guid.Empty))
                {
                    retval.Pnd = new DTOPnd(PndGuid);
                }
                return retval;
            }

            public DTOCca Cca()
            {
                DTOCca retval = new DTOCca(CcaGuid);
                retval.Fch = Fch;
                retval.Id = CcaId;
                retval.Ccd = Ccd;
                retval.Concept = Concept;
                if (!string.IsNullOrEmpty(Hash))
                {
                    retval.DocFile = new DTODocFile(Hash);
                }
                return retval;
            }


        }
    }
}
