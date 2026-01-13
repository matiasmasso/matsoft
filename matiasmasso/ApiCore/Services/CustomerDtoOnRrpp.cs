using Microsoft.EntityFrameworkCore;
using DTO;
using Org.BouncyCastle.Asn1.X509;

namespace Api.Services
{
    public class CustomerDtosOnRrppService
    {
        public static List<ImplicitDiscountModel> GetValues(Entities.MaxiContext db, UserModel user)
        {
            return db.EmailClis
                .AsNoTracking()
                .Include(x => x.Contact)
                .ThenInclude(x => x.CustomerDtos)
                .Where(x => x.EmailGuid == user.Guid)
                .SelectMany(x => x.Contact.CustomerDtos)
                .Select(x => new ImplicitDiscountModel(x.Guid)
                {
                    TargetCod = ImplicitDiscountModel.TargetCods.client,
                    Target = x.Customer,
                    Product = (Guid)x.Product!,
                    Fch = x.Fch,
                    Dto = (decimal)x.Dto!,
                    Obs = x.Obs
                })
                .ToList();
        }
        public static List<ImplicitDiscountModel> FromCustomer(Entities.MaxiContext db, Guid customerGuid)
        {
            return db.CliGrals
                .AsNoTracking()
                .Include(x => x.CustomerDtos)
                .Where(x => x.Guid == customerGuid)
                .SelectMany(x => x.CustomerDtos)
                .Select(x => new ImplicitDiscountModel(x.Guid)
                {
                    TargetCod = ImplicitDiscountModel.TargetCods.client,
                    Target = x.Customer,
                    Product = (Guid)x.Product!,
                    Fch = x.Fch,
                    Dto = (decimal)x.Dto!,
                    Obs = x.Obs
                })
                .ToList();
        }
    }
}
