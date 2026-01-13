using Api.Entities;
using DTO;
using System.Linq;

namespace Api.Services
{
    public class StoreLocatorService
    {

        public static StoreLocatorDTO Factory(Entities.MaxiContext db, ProductModel product, LangDTO lang)
        {
            StoreLocatorDTO retval = new StoreLocatorDTO();
            retval.Offline = Offline(db, product, lang);
            retval.Online = Online(db, product);
            return retval;
        }


        public static StoreLocatorDTO.OfflineClass Offline(Entities.MaxiContext db, ProductModel product, LangDTO lang, bool forRaffles = false)
        {
            var retval = new StoreLocatorDTO.OfflineClass();
            StoreLocatorDTO.Country country = new();
            StoreLocatorDTO.Zona zona = new();
            StoreLocatorDTO.Location location = new();

            var entities = Entities(db, product, lang, forRaffles);


            foreach (var x in entities)
            {
                if (retval.Countries.Count == 0 || !x.CountryGuid.Equals(country.Guid))
                {
                    country = new StoreLocatorDTO.Country
                    {
                        Guid = x.CountryGuid,
                        Nom = new LangTextDTO(x.CountryEsp, x.CountryCat, x.CountryEng, x.CountryPor).Tradueix(lang)
                    };
                    retval.Countries.Add(country);
                }
                if (country.Zonas.Count == 0 || x.Zona != zona.Nom)
                {
                    zona = new StoreLocatorDTO.Zona { Nom = x.Zona };
                    country.Zonas.Add(zona);
                }
                if (zona.Locations.Count == 0 || x.Location != location.Nom)
                {
                    location = new StoreLocatorDTO.Location { Nom = x.Location };
                    zona.Locations.Add(location);
                }
                var distributor = new StoreLocatorDTO.Distributor
                {
                    Guid = x.Guid,
                    Nom = x.Nom,
                    Address = x.Address,
                };

                location.Distributors.Add(distributor);
            }

            SetTopAreas(entities, retval!, lang);

            return retval;
        }

        private static void SetTopAreas(List<StoreLocatorDTO.FlatDistributor> entities, StoreLocatorDTO.OfflineClass offline, LangDTO lang)
        {
            var defaultCountryGuid = lang.DefaultCountry().Guid;
            var defaultCountry = offline.Countries.FirstOrDefault(x => x.Guid == defaultCountryGuid);
            offline.SelectedCountry = defaultCountry == null ? 0 : offline.Countries.IndexOf(defaultCountry);

            foreach (var country in offline.Countries)
            {
                country.SelectedZona = TopZona(entities, country);

                foreach (var zona in country.Zonas)
                {
                    zona.SelectedLocation = TopLocation(entities, country, zona);
                }
            }
        }

        private static int TopZona(List<StoreLocatorDTO.FlatDistributor> entities, StoreLocatorDTO.Country country)
        {
            var topEntity = entities.
                Where(x => x.CountryGuid.Equals(country.Guid)).
                        OrderBy(y => y.Priority).
                        ThenByDescending(y => y.Score).
                First();
            var topZona = country.Zonas.FirstOrDefault(x => x.Nom == topEntity.Zona);
            var retval = topZona == null ? 0 : country.Zonas.IndexOf(topZona);
            return retval;
        }

        private static int TopLocation(List<StoreLocatorDTO.FlatDistributor> entities, StoreLocatorDTO.Country country, StoreLocatorDTO.Zona zona)
        {
            var topEntity = entities.
                            Where(x => x.CountryGuid.Equals(country.Guid) && x.Zona == zona.Nom).
                            OrderBy(y => y.Priority).
                            ThenByDescending(y => y.Score).
                            First();
            var topLocation = zona.Locations.FirstOrDefault(x => x.Nom == topEntity.Location);
            var retval = topLocation == null ? 0 : zona.Locations.IndexOf(topLocation);
            return retval;
        }


        private static List<StoreLocatorDTO.FlatDistributor> Entities(Entities.MaxiContext db, ProductModel? product, LangDTO lang, bool forRaffles = false)
        {
            var retval = new List<StoreLocatorDTO.FlatDistributor>();

            IQueryable<VwStoreLocator>? query = null;
            if (forRaffles)
            {
                var country = lang.Id == LangDTO.Ids.POR ? CountryModel.Wellknown(CountryModel.Wellknowns.Portugal)!.Guid : CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!.Guid;
                var cache = CacheService.CatalogRequest();
                if (product != null)
                {
                    var premiumLines = PremiumLinesService.All(product);

                    //if product belongs to a PremiumLine, restrict raffle distributors to those signing any premiumline the product belongs to
                    if (premiumLines.Count == 0)
                        query = db.VwStoreLocators.Where(x => x.Raffles == true && x.Country == country);
                    else
                        //TODO: is assuming any customer with a Premium Line is valid, while it should restrict to customers with any PremiumLines in the list, but it would break the Linq query
                        query = db.VwStoreLocators.Where(x => x.Raffles == true && x.Country == country && x.PremiumLine != null);

                    //for raffles, offer brand distributors rather than restricting to category distributors
                    product = cache.Brand(product);
                }

            }
            else
                query = db.VwStoreLocators;

            if (product != null)
            {
                switch (product.Src)
                {
                    case ProductModel.SourceCods.Brand:
                        retval = query!.
                            Where(x => x.Brand.Equals(product.Guid)).
                            GroupBy(y => new { y.Country, y.CountryEsp, y.CountryCat, y.CountryEng, y.CountryPor, y.AreaNom, y.Cit, y.Client, y.Nom, y.Adr }).
                            Select(z => new StoreLocatorDTO.FlatDistributor
                            {
                                CountryGuid = (Guid)z.Key.Country!,
                                CountryEsp = z.Key.CountryEsp,
                                CountryCat = z.Key.CountryCat,
                                CountryEng = z.Key.CountryEng,
                                CountryPor = z.Key.CountryPor,
                                Zona = z.Key.AreaNom!,
                                Location = z.Key.Cit,
                                Guid = z.Key.Client,
                                Nom = z.Key.Nom,
                                Address = z.Key.Adr,
                                Priority = z.Max(s => (int)s.ConsumerPriority!),
                                Score = z.Sum(s => (decimal)s.Val!)
                            }).
                        ToList();
                        break;
                    case ProductModel.SourceCods.Dept:
                        retval = query.
                            Where(x => x.Dept.Equals(product.Guid)).
                            GroupBy(y => new { y.Country, y.CountryEsp, y.CountryCat, y.CountryEng, y.CountryPor, y.AreaNom, y.Cit, y.Client, y.Nom, y.Adr }).
                            Select(z => new StoreLocatorDTO.FlatDistributor
                            {
                                CountryGuid = (Guid)z.Key.Country!,
                                CountryEsp = z.Key.CountryEsp,
                                CountryCat = z.Key.CountryCat,
                                CountryEng = z.Key.CountryEng,
                                CountryPor = z.Key.CountryPor,
                                Zona = z.Key.AreaNom!,
                                Location = z.Key.Cit,
                                Guid = z.Key.Client,
                                Nom = z.Key.Nom,
                                Address = z.Key.Adr,
                                Priority = z.Max(s => (int)s.ConsumerPriority!),
                                Score = z.Sum(s => (decimal)s.Val!)
                            }).
                        ToList();
                        break;
                    case ProductModel.SourceCods.Category:
                        retval = query.
                            Where(x => x.Category.Equals(product.Guid)).
                            GroupBy(y => new { y.Country, y.CountryEsp, y.CountryCat, y.CountryEng, y.CountryPor, y.AreaNom, y.Cit, y.Client, y.Nom, y.Adr }).
                            Select(z => new StoreLocatorDTO.FlatDistributor
                            {
                                CountryGuid = (Guid)z.Key.Country!,
                                CountryEsp = z.Key.CountryEsp,
                                CountryCat = z.Key.CountryCat,
                                CountryEng = z.Key.CountryEng,
                                CountryPor = z.Key.CountryPor,
                                Zona = z.Key.AreaNom!,
                                Location = z.Key.Cit,
                                Guid = z.Key.Client,
                                Nom = z.Key.Nom,
                                Address = z.Key.Adr,
                                Priority = z.Max(s => (int)s.ConsumerPriority!),
                                Score = z.Sum(s => (decimal)s.Val!)
                            }).
                        ToList();
                        break;
                    default:
                        //sku: get category distributors
                        retval = query.
                            Where(x => x.Category.Equals(((ProductSkuModel)product).Category)).
                            GroupBy(y => new { y.Country, y.CountryEsp, y.CountryCat, y.CountryEng, y.CountryPor, y.AreaNom, y.Cit, y.Client, y.Nom, y.Adr }).
                            Select(z => new StoreLocatorDTO.FlatDistributor
                            {
                                CountryGuid = (Guid)z.Key.Country!,
                                CountryEsp = z.Key.CountryEsp,
                                CountryCat = z.Key.CountryCat,
                                CountryEng = z.Key.CountryEng,
                                CountryPor = z.Key.CountryPor,
                                Zona = z.Key.AreaNom!,
                                Location = z.Key.Cit,
                                Guid = z.Key.Client,
                                Nom = z.Key.Nom,
                                Address = z.Key.Adr,
                                Priority = z.Max(s => (int)s.ConsumerPriority!),
                                Score = z.Sum(s => (decimal)s.Val!)
                            }).
                        ToList();
                        break;
                }
            }

            retval = retval.
                OrderBy(o => o.CountryEsp).
                ThenBy(q => q.Zona).
                ThenBy(q => q.Location).
                ThenBy(q => q.Priority).
                ThenByDescending(p => p.Score).
                ToList();

            return retval;
        }

        public static StoreLocatorDTO.OnlineClass Online(Entities.MaxiContext db, ProductModel product)
        {
            var retval = new StoreLocatorDTO.OnlineClass();
            var yesterday = DateTime.Today.AddHours(-24);
            retval.LandingPages = db.VwWtbolDisplays.
                          OrderByDescending(x => x.SiteStock).
                          ThenBy(y => y.Inventory).
                          Where(z => z.Product.Equals(product.Guid)).
                           Select(w => new StoreLocatorDTO.LandingPage()
                           {
                               Guid = (Guid)w.LandingPage!,
                               Nom = w.Nom ?? "",
                               Web = w.Web ?? "",
                               Url = w.Url ?? "",
                               InStock = w.SiteStock > 0 && w.FchLastStocks > yesterday
                           }
                          ).ToList();
            return retval;
        }

    }
}
