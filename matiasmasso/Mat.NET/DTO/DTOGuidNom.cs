using System;

namespace DTO
{
    public class DTOGuidNom : DTOBaseGuid
    {
        public string Nom { get; set; }
        // Property Nom As String

        public DTOGuidNom(Guid oGuid, string sNom = "") : base(oGuid)
        {
            Nom = sNom;
        }

        public DTOGuidNom() : base()
        {
        }

        public static DTOGuidNom Factory(Guid oGuid, string sNom = "")
        {
            var retval = new DTOGuidNom(oGuid);
            {
                retval.Nom = sNom;
            }
            return retval;
        }

        public Compact CompactCustomer()
        {
            Compact retval = Compact.Factory(this.Guid, this.Nom);
            return retval;
        }

        public class Compact
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }

            public static Compact Factory(Guid guid, String nom = "")
            {
                Compact retval = new Compact();
                retval.Guid = guid;
                retval.Nom = nom;
                return retval;
            }

            public bool Equals(DTOBaseGuid oCandidate)
            {
                bool retval = false;
                if (oCandidate != null)
                    retval = Guid.Equals(oCandidate.Guid);
                return retval;
            }
        }
    }
}
