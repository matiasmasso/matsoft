using DTO;

namespace Api.Services
{
    public class ProBasketService
    {
        public static ProBasketDTO Basket(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new ProBasketDTO();
                retval.Destinations = Destinations(db, user);
                retval.Destination = retval.Destinations.Count == 1 ? retval.Destinations[0] : null;
                retval.ChannelProducts = ChannelProducts(db, user);
                retval.CustomerPortfolio = CustomerPortfolioService.FromUser(db, user);
                return retval;

            }
        }


        public static List<ProBasketDTO.DestinationClass> Destinations(Entities.MaxiContext db, UserModel user)
        {
            return db.EmailClis
                .Join(db.CliClients, email => email.ContactGuid, client => client.Guid, (email, client) => new { email, client })
                .Join(db.VwAddresses, x => x.email.ContactGuid, adr => adr.SrcGuid, (x, adr) => new { x, adr })
                .Where(x => x.x.email.EmailGuid == user.Guid && x.x.client.Gu.Obsoleto == false)
                .Select(x => new ProBasketDTO.DestinationClass
                {
                    Guid = x.x.email.ContactGuid,
                    Nom = ContactModel.RaoSocialAndNomComercial(x.x.client.Gu.RaoSocial, x.x.client.Gu.NomCom),
                    Adr = x.adr.Adr,
                    Location = LocationModel.FullNom(x.adr.LocationNom, x.adr.ZonaNom, x.adr.ProvinciaNom, x.adr.CountryIso, x.adr.CountryEsp)
                })
                .ToList();
        }

        public static List<Guid> Skus(Entities.MaxiContext db, UserModel user)
        {
            return db.EmailClis
                .Join(db.VwCustomerSkus, email => email.ContactGuid, sku => sku.Customer, (email, sku) => new { email, sku })
                .Where(x => x.email.EmailGuid == user.Guid)
                .Select(y => y.sku.SkuGuid)
                .ToList();

        }

        public static List<ProductChannelModel> ChannelProducts(Entities.MaxiContext db, UserModel user)
        {
            var ccxsQuery = UserService.CcxsQuery(db, user);
            var retval = ccxsQuery
                .Join(db.CliGrals, ccx => ccx, customer => customer.Guid, (ccx, customer) => new { ccx, customer })
                .Join(db.ProductChannels, x => x.customer.ContactClassNavigation!.DistributionChannel, y => y.DistributionChannel, (x, y) => new { x, y })
                .Select(z => new ProductChannelModel
                {
                    Product = z.y.Product,
                    Channel = z.y.DistributionChannel,
                    Cod = z.y.Cod
                })
                .ToList();

            return retval;
        }

    }
}
