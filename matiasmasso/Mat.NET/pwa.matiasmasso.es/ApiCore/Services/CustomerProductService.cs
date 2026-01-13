using DTO;

namespace Api.Services
{
    public class CustomerProductsService
    {
        public static List<CustomerProductModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.ArtCustRefs
                    .Select(x => new CustomerProductModel
                    {
                        Customer = x.CliGuid,
                        Sku = x.ArtGuid,
                        Ref = x.Ref
                    }).ToList();
            }
        }
    }
}
