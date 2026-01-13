using Microsoft.EntityFrameworkCore;
using DTO;

namespace Api.Services
{
    public class ChannelDtosOnRrppService
    {
        public static List<ImplicitDiscountModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ChannelDtos
                    .AsNoTracking()
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new ImplicitDiscountModel(x.Guid)
                    {
                        TargetCod = ImplicitDiscountModel.TargetCods.canal,
                        Target = x.Channel,
                        Product = x.Product,
                        Fch = x.Fch,
                        Dto = x.Dto,
                        Obs = x.Obs
                    })
                    .ToList();
            }
        }
    }
}
