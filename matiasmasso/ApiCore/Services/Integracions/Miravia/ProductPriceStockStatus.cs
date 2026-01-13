using DTO.Integracions.Miravia;

namespace Api.Services.Integracions.Miravia
{
    public class ProductPriceStockStatus
    {
        public static ProductPriceStockStatusModel GetValue()
        {
            ProductPriceStockStatusModel retval = new();
            var magatzem = DTO.MgzModel.Default();
            var marketplace = DTO.MarketPlaceModel.Wellknown(DTO.MarketPlaceModel.Wellknowns.Miravia);
            using (var db = new Entities.MaxiContext())
            {
                retval.Items = db.VwCustomerDeptSkus
                    .Where(x => x.Customer == marketplace!.Guid
                    && !x.SkuNoEcom
                    && (x.MgzGuid == null || x.MgzGuid == magatzem!.Guid))
                    .Select(x => new ProductPriceStockStatusModel.Item
                    {
                        SellerSku = x.SkuId.ToString(),
                        Price = x.Retail,
                        SpecialPrice = x.Retail,
                        DefaultStock = x.Stock ?? 0
                    }).ToList();
            }
            return retval;
        }

    }
}
