using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCustomerCluster : DTOGuidNom
    {
        public DTOHolding holding { get; set; }
        public string obs { get; set; }

        public List<DTOCustomer> Customers { get; set; }

        public DTOCustomerCluster() : base()
        {
            Customers = new List<DTOCustomer>();
        }

        public DTOCustomerCluster(Guid oGuid) : base(oGuid)
        {
            Customers = new List<DTOCustomer>();
        }

        public static DTOCustomerCluster Factory(DTOHolding oHolding)
        {
            DTOCustomerCluster retval = new DTOCustomerCluster();
            retval.holding = oHolding;
            return retval;
        }
    }
}
