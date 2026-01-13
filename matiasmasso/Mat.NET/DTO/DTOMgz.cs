using System;

namespace DTO
{
    public class DTOMgz : DTOContact// Warehouse
    {
        public string abr { get; set; }
        public new bool IsLoaded { get; set; }

        public enum Wellknowns
        {
            notSet,
            vivaceLliça,
            vivaceLaRoca2,
            matiasMasso
        }

        public DTOMgz() : base()
        {
        }

        public DTOMgz(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOMgz Wellknown(DTOMgz.Wellknowns id)
        {
            DTOMgz retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOMgz.Wellknowns.vivaceLliça:
                    {
                        sGuid = "41a81aca-1c01-44fc-bf57-2728b03f74d8";
                        break;
                    }

                case DTOMgz.Wellknowns.vivaceLaRoca2:
                    {
                        sGuid = "88A2C2F3-9E14-421A-B727-7647ECD07165";
                        break;
                    }

                case DTOMgz.Wellknowns.matiasMasso:
                    {
                        sGuid = "4C37D20F-A975-4C63-BB9C-F997A7080DF1";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOMgz(oGuid);
            }
            return retval;
        }

        public static DTOMgz FromContact(DTOContact oContact)
        {
            DTOMgz retval = null;
            if (oContact == null)
                retval = new DTOMgz();
            else if (oContact is DTOMgz)
                retval = (DTOMgz)oContact;
            else
            {
                retval = new DTOMgz(oContact.Guid);
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

        public static string AbrOrNom(DTOMgz oMgz)
        {
            string retval = oMgz.abr ?? oMgz.Nom;
            return retval;
        }
    }
}
