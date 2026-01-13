using DocumentFormat.OpenXml.Office2013.PowerPoint;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PremiumLineService
    {
    }
    public class PremiumLinesService
    {
        public static List<PremiumLineModel> All(ProductModel product)
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db, product);
            }
        }
        public static List<PremiumLineModel> All(Entities.MaxiContext db, ProductModel product)
        {
            return db.PremiumProducts
                .Join(db.VwProductParents, prem => prem.Product, parent => parent.Parent, (prem, parent) => new { prem, parent })
                .Where(x => x.parent.Child == product.Guid)
                .GroupBy(x => new { x.prem.PremiumLineNavigation.Guid, x.prem.PremiumLineNavigation.Nom})
                .Select(x => new PremiumLineModel(x.Key.Guid)
                {
                    Nom = x.Key.Nom
                })
                .ToList();
        }

    }
}
