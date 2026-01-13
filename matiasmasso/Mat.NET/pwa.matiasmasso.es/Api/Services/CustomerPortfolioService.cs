using DTO;

namespace Api.Services
{
    public class CustomerPortfolioService
    {
        public static List<CustomerPortfolioModel> FromUser(Entities.MaxiContext db, UserModel user)
        {
            var ccxsQuery = UserService.CcxsQuery(db, user);
            var retval = ccxsQuery
                .Join(db.CliTpas, ccx => ccx, clitpa => clitpa.CliGuid, (ccx, clitpa) => new { ccx, clitpa })
                .Select(x => new CustomerPortfolioModel {
                    Customer = x.clitpa.CliGuid,
                    Product = x.clitpa.ProductGuid,
                    Cod  = x.clitpa.Cod
                })
                .ToList();
            return retval;
        }
    }
}
