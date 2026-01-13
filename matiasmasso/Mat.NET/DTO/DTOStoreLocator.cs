using System.Collections.Generic;

namespace DTO
{
    public class DTOStoreLocator
    {
        public DTOLang Lang { get; set; }
        public StoreLocatorQuery Query { get; set; }

        public List<DTOArea> Countries { get; set; }
        public List<DTOArea> Zonas { get; set; }
        public List<DTOArea> Locations { get; set; }

        public DTOArea Country { get; set; }
        public DTOArea Zona { get; set; }
        public DTOArea Location { get; set; }
        public List<DTOProductDistributor> Distributors { get; set; }

        public DTOStoreLocator()
        {
            Query = new StoreLocatorQuery();
            Countries = new List<DTOArea>();
            Zonas = new List<DTOArea>();
            Locations = new List<DTOArea>();
            Distributors = new List<DTOProductDistributor>();
        }

        public static DTOStoreLocator Factory(DTOLang oLang, DTOProduct oProduct, DTOLocation oLocation)
        {
            DTOStoreLocator retval = new DTOStoreLocator();
            {
                var withBlock = retval;
                withBlock.Lang = oLang;
                withBlock.Query.Product = oProduct;
                withBlock.Query.Location = oLocation;
            }
            return retval;
        }

        public class StoreLocatorQuery
        {
            public DTOProduct Product { get; set; }
            public DTOLocation Location { get; set; }
        }
    }
}
