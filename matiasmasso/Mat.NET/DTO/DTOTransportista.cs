using System;

namespace DTO
{
    public class DTOTransportista : DTOContact
    {
        public string Abr { get; set; }
        public int Cubicaje { get; set; }
        public string TrackingUrlTemplate { get; set; }
        public bool Activat { get; set; }

        public decimal noCubicarPerSotaDe { get; set; }
        public int compensaPercent { get; set; }
        public bool allowReembolsos { get; set; }
        public bool transportaMobiliari { get; set; }

        public enum Wellknowns
        {
            souto,
            tnt,
            seur,
            txt
        }

        public enum CodTarifa
        {
            preuHastaKg,
            preuPerKg
        }

        public DTOTransportista() : base()
        {
        }

        public DTOTransportista(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOTransportista Wellknown(Wellknowns id)
        {
            DTOTransportista retval = null;
            string sGuid = "";
            switch (id)
            {
                case Wellknowns.souto:
                    {
                        sGuid = "4D92D321-C045-49D6-BEAD-181169002128";
                        break;
                    }

                case Wellknowns.tnt:
                    {
                        sGuid = "9EE52C83-BFCE-41C2-9435-8D0516C26424";
                        break;
                    }
                case Wellknowns.txt:
                    {
                        sGuid = "8FC74DDC-C659-4EEE-9AFA-D79A6900E5A8";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOTransportista(oGuid);
            }
            return retval;
        }


        public static DTOTransportista FromContact(DTOContact oContact)
        {
            DTOTransportista retval = null;
            if (oContact != null)
            {
                retval = new DTOTransportista(oContact.Guid);
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
