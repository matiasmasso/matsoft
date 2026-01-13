using System;

namespace DTO
{
    public class DTOContactDoc : DTOBaseGuid
    {
        public DTOContact Contact { get; set; }
        public Types Type { get; set; }
        public DateTime Fch { get; set; }
        public string Ref { get; set; }
        public DTODocFile DocFile { get; set; }
        public bool Obsoleto { get; set; }

        public enum Types
        {
            NotSet,
            NIF,
            Escriptura,
            Contracte,
            SeguretatSocial,
            Academic,
            Tarjeta,
            Retencions,
            Model_145
        }

        public DTOContactDoc(System.Guid oGuid) : base(oGuid)
        {
        }

        public DTOContactDoc() : base()
        {
        }
    }
}
