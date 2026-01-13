using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PriceListSupplierService
    {
        public static List<GuidDecimal> Current()
        {
            using (var db = new MaxiContext())
            {
                return db.VwSkuCosts
                    .AsNoTracking()
                    .Select(x => new GuidDecimal(
                        x.SkuGuid,
                        Math.Round(x.Price - (100 * x.DiscountOnInvoice) / 100)
                        )).ToList();
            }
        }
    }
}
