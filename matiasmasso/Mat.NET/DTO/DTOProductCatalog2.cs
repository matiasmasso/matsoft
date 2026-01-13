using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductCatalog2
    {
        public DateTime FchCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<DTOProductBrand> Brands { get; set; }

        private IEnumerable<DTOProductSku> Skus()
        {
            return Brands.SelectMany(x => x.Categories).SelectMany(x => x.Skus);
        }
        public DTOProductSku FindSku(Guid? guid)
        {
            DTOProductSku retval = null;
            if (guid != null)
                retval = Skus().FirstOrDefault(z => z.Guid.Equals(guid));
            return retval;
        }
        public DTOProductSku FindSku(DTOEan ean)
        {
            DTOProductSku retval = null;
            if (ean != null & ean.Value.Length == 13)
                retval = Skus().FirstOrDefault(z => ean.Equals(z.Ean13));
            return retval;
        }
        public DTOProductSku FindSku(int skuId)
        {
            DTOProductSku retval = null;
            if (skuId > 0)
                retval = Skus().FirstOrDefault(z => z.Id == skuId);
            return retval;
        }
        public DTOProductSku FindSkuByRefPrv(string refPrv)
        {
            DTOProductSku retval = null;
            if (!string.IsNullOrEmpty(refPrv))
                retval = Skus().FirstOrDefault(z => z.RefProveidor == refPrv);
            return retval;
        }
    }
}
