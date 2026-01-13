using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOMarketPlace : DTOBaseGuid
    {
        public string Nom { get; set; }
        public string Web { get; set; }

        public decimal Commission { get; set; }
        public List<DTOMarketplaceSku> Catalog { get; set; } 

        public enum Wellknowns
        {
            NotSet,
            AmazonSeller,
            Worten,
            Promofarma
        }

        public DTOMarketPlace() : base()
        {
        }

        public DTOMarketPlace(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOMarketPlace Wellknown(Wellknowns id)
        {
            DTOMarketPlace retval = null;
            switch (id)
            {
                case Wellknowns.AmazonSeller:
                    retval = new DTOMarketPlace(new Guid("9f9ac46b-72d1-46d1-b928-d3c61480bda9"));
                    break;
                case Wellknowns.Worten:
                    retval = new DTOMarketPlace(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.worten).Guid);
                    break;
                case Wellknowns.Promofarma:
                    retval = new DTOMarketPlace(new Guid("150136FD-BCAF-459F-954F-C92B5E9DF7B0"));
                    break;
                default:
                    break;
            }
            return retval;
        }

        public DTOContact Contact()
        {
            DTOContact retval = new DTOContact(this.Guid);
            retval.FullNom = this.Nom;
            return retval;
        }

        public class Collection : List<DTOMarketPlace>
        {

        }
    }
}
