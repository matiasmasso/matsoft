using System;

namespace DTO
{
    public class DTOAlbBloqueig : DTOBaseGuid
    {
        public DTOUser User { get; set; }
        public DTOContact Contact { get; set; }
        public Codis Codi { get; set; }
        public DateTime Fch { get; set; }

        public enum Codis
        {
            PDC,
            ALB
        }

        public DTOAlbBloqueig() : base()
        {
        }

        public DTOAlbBloqueig(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOAlbBloqueig Factory(DTOUser oUser, DTOContact oContact, Codis oCodi)
        {
            DTOAlbBloqueig retval = new DTOAlbBloqueig();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Contact = oContact;
                withBlock.Codi = oCodi;
            }
            return retval;
        }
    }
}
