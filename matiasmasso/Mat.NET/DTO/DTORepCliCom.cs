using System;

namespace DTO
{
    public class DTORepCliCom : DTOBaseGuid
    {
        public DTORep Rep { get; set; }
        public DTOCustomer Customer { get; set; }
        public ComCods ComCod { get; set; }
        public DateTime Fch { get; set; }
        public string Obs { get; set; }
        public DTOUser UsrCreated { get; set; }
        public DateTime FchCreated { get; set; }

        public enum ComCods
        {
            Standard,
            Reduced,
            Excluded
        }

        public DTORepCliCom() : base()
        {
        }

        public DTORepCliCom(Guid oGuid) : base(oGuid)
        {
        }

        public static DTORepCom RepCom(DTORepCliCom oRepCliCom, DTORepProduct oRepProduct)
        {
            DTORepCom retval = null/* TODO Change to default(_) if this is not a reference type */;
            switch (oRepCliCom.ComCod)
            {
                case DTORepCliCom.ComCods.Standard:
                    {
                        retval = new DTORepCom();
                        retval.Rep = oRepProduct.Rep;
                        retval.Com = oRepProduct.ComStd;
                        break;
                    }

                case DTORepCliCom.ComCods.Reduced:
                    {
                        retval = new DTORepCom();
                        retval.Rep = oRepProduct.Rep;
                        retval.Com = oRepProduct.ComRed;
                        break;
                    }
            }
            return retval;
        }

        public static decimal Com(DTORepCliCom oRepCliCom, DTORepProduct oRepProduct)
        {
            decimal retval = 0;
            switch (oRepCliCom.ComCod)
            {
                case DTORepCliCom.ComCods.Standard:
                    {
                        retval = oRepProduct.ComStd;
                        break;
                    }

                case DTORepCliCom.ComCods.Reduced:
                    {
                        retval = oRepProduct.ComRed;
                        break;
                    }
            }
            return retval;
        }
    }
}
