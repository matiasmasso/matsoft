using System;

namespace DTO
{
    public class DTOBanc : DTOContact
    {
        public string Abr { get; set; }
        public DTOAmt Classificacio { get; set; }
        public DTOAmt Disposat { get; set; }
        public DTOIban Iban { get; set; }
        public string SepaCoreIdentificador { get; set; }
        public string NormaRMECedent { get; set; }
        public decimal ComisioGestioCobr { get; set; }

        public string ConditionsUnpayments { get; set; }
        public string ConditionsTransfers { get; set; }
        public new bool IsLoaded { get; set; }

        public enum Wellknowns
        {
            Cx,
            CaixaBank,
            Santander,
            DeutscheBank
        }


        public DTOBanc() : base()
        {
        }

        public DTOBanc(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOBanc Wellknown(DTOBanc.Wellknowns id)
        {
            DTOBanc retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOBanc.Wellknowns.Cx:
                    {
                        sGuid = "8014A3C5-7B26-4C82-8719-B0C1D0384777";
                        break;
                    }

                case DTOBanc.Wellknowns.CaixaBank:
                    {
                        sGuid = "C52FA12B-BBA1-4BD8-BB97-334DDB2B12D4";
                        break;
                    }

                case DTOBanc.Wellknowns.Santander:
                    {
                        sGuid = "A48D18C1-6F99-49CD-8AA5-E3AF3FE5DAE0";
                        break;
                    }

                case DTOBanc.Wellknowns.DeutscheBank:
                    {
                        sGuid = "27B23889-DA93-41A8-8476-926C98AE47AA";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOBanc(oGuid);
            }
            return retval;
        }


        public string abrOrNom()
        {
            string retval = Abr;
            if (retval == "")
                retval = base.NomComercial;
            if (retval == "")
                retval = base.Nom;
            return retval;
        }
    }
}
