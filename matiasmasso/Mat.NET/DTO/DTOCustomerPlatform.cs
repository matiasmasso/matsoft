namespace DTO
{
    using System;
    using System.Collections.Generic;

    public class DTOCustomerPlatform : DTOContact
    {
        public DTOContact Customer { get; set; }
        public List<DTOPlatformDestination> Destinations { get; set; }
        public List<DTODelivery> Deliveries { get; set; }
        public DTOAmt BaseImponible { get; set; }

        public DTOCustomerPlatform() : base()
        {
        }

        public DTOCustomerPlatform(Guid oGuid) : base(oGuid)
        {
            Destinations = new List<DTOPlatformDestination>();
        }

        public static DTOCustomerPlatform FromContact(DTOContact oContact)
        {
            DTOCustomerPlatform retval = null;
            if (oContact == null)
                retval = new DTOCustomerPlatform();
            else if (oContact is DTOCustomerPlatform)
                retval = (DTOCustomerPlatform)oContact;
            else
            {
                retval = new DTOCustomerPlatform(oContact.Guid);
                {
                    var withBlock = retval;
                    withBlock.Emp = oContact.Emp;
                    withBlock.Nom = oContact.Nom;
                    withBlock.NomComercial = oContact.NomComercial;
                    withBlock.SearchKey = oContact.SearchKey;
                    withBlock.FullNom = oContact.FullNom;
                    withBlock.Nifs = oContact.Nifs;
                    withBlock.Address = oContact.Address;
                    withBlock.ContactClass = oContact.ContactClass;
                    withBlock.Lang = oContact.Lang;
                    withBlock.Rol = oContact.Rol;
                    withBlock.NomAnterior = oContact.NomAnterior;
                    withBlock.NomNou = oContact.NomNou;
                    withBlock.Website = oContact.Website;
                    withBlock.Logo = oContact.Logo;
                    withBlock.GLN = oContact.GLN;
                    withBlock.Telefon = oContact.Telefon;
                    withBlock.Tels = oContact.Tels;
                    withBlock.ContactPersons = oContact.ContactPersons;
                    withBlock.Obsoleto = oContact.Obsoleto;
                }
            }
            return retval;
        }
    }


    public class DTOPlatformDestination : DTOContact
    {
        public DTOCustomerPlatform Platform { get; set; }


        public DTOPlatformDestination() : base()
        {
        }

        public DTOPlatformDestination(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOPlatformDestination FromContact(DTOContact oContact)
        {
            DTOPlatformDestination retval = new DTOPlatformDestination(oContact.Guid);
            retval.Emp = oContact.Emp;
            retval.FullNom = oContact.FullNom;
            return retval;
        }
    }
}
