using System;

namespace DTO
{
    public class DTOProveidor : DTOContact
    {
        public DTOPgcCta defaultCtaCarrec { get; set; }
        public DTOPgcCta defaultCtaCreditora { get; set; }
        public DTOCur Cur { get; set; }
        public DTOCommercialMargin commercialMargin { get; set; }
        public IRPFCods IRPF_Cod { get; set; }
        public DTOPaymentTerms paymentTerms { get; set; }
        public DTOIncoterm IncoTerm { get; set; }
        public DTOProductBrand.CodStks codStk { get; set; }


        public new bool IsLoaded { get; set; }


        public enum IRPFCods
        {
            exento,
            standard,
            reducido,
            custom
        }

        public enum Wellknowns
        {
            Roemer,
            FourMoms,
            Mayborn,
            Inglesina
        }

        public DTOProveidor() : base()
        {
            paymentTerms = new DTOPaymentTerms();
        }

        public DTOProveidor(Guid oGuid) : base(oGuid)
        {
            paymentTerms = new DTOPaymentTerms();
        }


        public static DTOProveidor FromContact(DTOContact oContact)
        {
            DTOProveidor retval = null;
            if (oContact == null)
                retval = new DTOProveidor();
            else if (oContact is DTOProveidor)
                retval = (DTOProveidor)oContact;
            else
            {
                retval = new DTOProveidor(oContact.Guid);
                {
                    var withBlock = retval;
                    withBlock.Emp = oContact.Emp;
                    withBlock.Nom = oContact.Nom;
                    withBlock.NomComercial = oContact.NomComercial;
                    withBlock.FullNom = oContact.FullNom;
                    withBlock.Nifs = oContact.Nifs;
                    withBlock.Address = oContact.Address;
                    withBlock.ContactClass = oContact.ContactClass;
                    withBlock.Lang = oContact.Lang;
                    withBlock.Rol = oContact.Rol;
                    withBlock.Cur = DTOCur.Eur();
                }
            }
            return retval;
        }


        public static DTOProveidor Wellknown(DTOProveidor.Wellknowns id)
        {
            DTOProveidor retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOProveidor.Wellknowns.Roemer:
                    {
                        sGuid = "47C3A677-89C3-4B5E-86A4-25434CE415D5";
                        break;
                    }

                case DTOProveidor.Wellknowns.Inglesina:
                    {
                        sGuid = "A80228D2-0308-42E7-8405-48F4713E4413";
                        break;
                    }

                case DTOProveidor.Wellknowns.FourMoms:
                    {
                        sGuid = "CBA06D6C-8DB5-45FF-9927-FC284441366C";
                        break;
                    }

                case DTOProveidor.Wellknowns.Mayborn:
                    {
                        sGuid = "DB65A3B7-B25A-4728-81F6-D69A3BDBEAAD";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOProveidor(oGuid);
            }
            return retval;
        }

        public static decimal IRPF(DTOProveidor oProveidor, DateTime DtFch)
        {
            switch (oProveidor.IRPF_Cod)
            {
                case DTOProveidor.IRPFCods.reducido:
                    {
                        decimal retval = DTOTax.closest(DTOTax.Codis.irpf_Professionals_Reduit, DtFch).tipus;
                        return retval;
                    }

                case DTOProveidor.IRPFCods.standard:
                    {
                        decimal retval = DTOTax.closest(DTOTax.Codis.irpf_Professionals_Standard, DtFch).tipus;
                        return retval;
                    }

                default:
                    {
                        return 0;
                    }
            }
        }

        public static DTOCommercialMargin GetCommercialMargin(DTOProveidor oProveidor = null)
        {
            DTOCommercialMargin retval = new DTOCommercialMargin();
            {
                var withBlock = retval;
                withBlock.CostToRetail = 155;
            }
            return retval;
        }

    }
}
