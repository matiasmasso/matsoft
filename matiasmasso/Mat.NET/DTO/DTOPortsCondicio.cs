using System;

namespace DTO
{
    public class DTOPortsCondicio : DTOBaseGuid
    {
        public Ids Id { get; set; }
        public string Nom { get; set; }
        public int UnitsQty { get; set; }
        public DTOAmt UnitsMinPreu { get; set; }
        public DTOAmt PdcMinVal { get; set; }
        public Cods Cod { get; set; }
        public DTOAmt Fee { get; set; }

        public enum Ids
        {
            segunZona,
            peninsulaBalears,
            andorra,
            canaries,
            resteDelMon,
            eCom,
            portugal
        }

        public enum Cods
        {
            portsPagats,
            carrecEnFactura,
            portsDeguts,
            reculliran
        }

        public DTOPortsCondicio() : base()
        {
        }

        public DTOPortsCondicio(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOPortsCondicio Wellknown(Ids id)
        {
            DTOPortsCondicio retval = null;
            switch (id)
            {
                case Ids.peninsulaBalears:
                    retval = new DTOPortsCondicio(new Guid("8889F388-46ED-47F2-B0EA-922E48FC52DF"));
                    break;
                case Ids.andorra:
                    retval = new DTOPortsCondicio(new Guid("58E0A260-0E26-49B8-8F35-9D2E7FAD0814"));
                    break;
                case Ids.portugal:
                    retval = new DTOPortsCondicio(new Guid("859E5B7A-0400-46C5-B5BA-BE80EAC929A0"));
                    break;
                case Ids.canaries:
                    retval = new DTOPortsCondicio(new Guid("C97040E9-5755-4C17-BC04-D5A58B62466D"));
                    break;
                case Ids.resteDelMon:
                    retval = new DTOPortsCondicio(new Guid("10611042-C222-459F-8AA6-84F2341CFAFE"));
                    break;
                case Ids.eCom:
                    retval = new DTOPortsCondicio(new Guid("0FEA5318-102F-499C-A7C8-7A61CE02BF1D"));
                    break;
            }

            return retval;
        }

        public static DTOPortsCondicio Default(DTOCustomer customer)
        {
            DTOPortsCondicio retval = null;
            if (customer.ContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.online)))
            {
                retval = DTOPortsCondicio.Wellknown(Ids.eCom);
            }
            else
                switch (customer.Address.Country().ISO)
                {
                    case "AD":
                        retval = DTOPortsCondicio.Wellknown(Ids.andorra);
                        break;
                    case "PT":
                        if (customer.Address.ExportCod() == DTOInvoice.ExportCods.extracomunitari)
                            retval = DTOPortsCondicio.Wellknown(Ids.resteDelMon);
                        else
                            retval = DTOPortsCondicio.Wellknown(Ids.portugal);
                        break;
                    case "ES":
                        if (customer.Address.ExportCod() == DTOInvoice.ExportCods.extracomunitari)
                            retval = DTOPortsCondicio.Wellknown(Ids.canaries);
                        else
                            retval = DTOPortsCondicio.Wellknown(Ids.peninsulaBalears);
                        break;
                    default:
                        retval = DTOPortsCondicio.Wellknown(Ids.resteDelMon);
                        break;
                }
            return retval;
        }

    }
}
