using System;

namespace DTO
{
    public class DTOTaller : DTOContact
    {
        public DTOTransportista transportista { get; set; }

        public DTOTaller() : base()
        {
        }

        public DTOTaller(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOTaller fromContact(DTOContact oContact)
        {
            DTOTaller retval = null;
            if (oContact == null)
                retval = new DTOTaller();
            else
            {
                retval = new DTOTaller(oContact.Guid);
                {
                    var withBlock = retval;
                    withBlock.Nom = oContact.Nom;
                    withBlock.NomComercial = oContact.NomComercial;
                    withBlock.FullNom = oContact.FullNom;
                    withBlock.Nifs = oContact.Nifs;
                    withBlock.Address = oContact.Address;
                    withBlock.ContactClass = oContact.ContactClass;
                    withBlock.Lang = oContact.Lang;
                    withBlock.Rol = oContact.Rol;
                }
            }
            return retval;
        }
    }
}
