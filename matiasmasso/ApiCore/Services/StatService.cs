using DTO;

namespace Api.Services
{
    public class StatService
    {

        public static StatDTO Load(StatDTO.StatRequest request)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new StatDTO();

                var query = db.VwStats.AsQueryable();

                if (request.Filters.Any(x => x.Cod == (int)StatDTO.FilterCods.Marketplace))
                {
                    var marketplace = request.Filters.First(x => x.Cod == (int)StatDTO.FilterCods.Marketplace).Guid;
                    query = query.Where(x => x.Emp == request.Emp
                    && x.Year == request.Year
                    && x.RepGuid == marketplace);
                }

                var entities = ((IQueryable<Entities.VwStat>)query).ToList();

                retval.Items = entities.Select(x => new StatDTO.Item
                {
                    Brand = x.Brand,
                    Category = (Guid)x.Category!,
                    Sku = x.ArtGuid,
                    Country = x.CountryGuid,
                    Zona = x.ZonaGuid,
                    Location = x.LocationGuid,
                    Customer = x.CliGuid,
                    Channel = x.DistributionChannel,
                    Year = (int)x.Year!,
                    Month = (int)x.Month!,
                    Qty = (int)x.Qty!,
                    Eur = (decimal)x.Eur!
                }).ToList();

                return retval;
            }
        }
    }
}
