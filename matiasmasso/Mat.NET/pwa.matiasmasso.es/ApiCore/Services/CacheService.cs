using Api.Shared;
using DTO;
using static DTO.CacheDTO.Table;

namespace Api.Services
{
    public class CacheService
    {
        //gets a cache refreshed on requested tables if needed
        //this should be the standard way to access the cache to make sure the data is updated
        public static CacheDTO Request(params CacheDTO.Table.TableIds[] tables)
        {
            var serverCache = Api.Shared.AppState.DefaultCache();
            var clientRequest = serverCache.Request(tables);
            UpdateServerIfNeeded(serverCache, clientRequest);
            return serverCache;
        }

        //initiates/updates server cache catalog if needed for server calls
        public static CacheDTO CatalogRequest(params CacheDTO.Table.TableIds[] additionalTables)
        {
            CacheDTO.Table.TableIds[] catalogTables = { CacheDTO.Table.TableIds.Tpa
                    , CacheDTO.Table.TableIds.Dept
                    , CacheDTO.Table.TableIds.Stp
                    , CacheDTO.Table.TableIds.Art
                    , CacheDTO.Table.TableIds.PriceListItem_Customer
                    , CacheDTO.Table.TableIds.Arc
                    , CacheDTO.Table.TableIds.Pnc
                    , CacheDTO.Table.TableIds.UrlSegment  };

            var tables = new List<TableIds>();
            tables.AddRange(catalogTables);
            tables.AddRange(additionalTables);
            return Request(tables.ToArray());
        }


        //builds a partial client cache with those requested tables which have been found outdated
        public static CacheDTO ClientCache(CacheDTO clientRequest)
        {
            var emp = clientRequest.EmpId;
            var serverCache = AppState.Cache(emp);

            //make sure we compare tables with most recent cache
            UpdateServerIfNeeded(serverCache, clientRequest);

            var dirtyTables = serverCache.ClientDirtyTables(clientRequest.Tables);
            var retval = serverCache.UpdatedClient(dirtyTables);
            return retval;
        }


        //updates server cache on those tables requested by the client which have been found outdated
        public static void UpdateServerIfNeeded(CacheDTO serverCache, CacheDTO clientRequest)
        {
            using(var db = new Entities.MaxiContext())
            {
                var dirtyTables = SQLService.DirtyTables(db, serverCache.Tables, clientRequest.Tables);
                foreach (var table in dirtyTables)
                {
                    try
                    {
                    switch ((CacheDTO.Table.TableIds)table.Id)
                    {
                        case CacheDTO.Table.TableIds.Tpa:
                            serverCache.Brands = ProductBrandsService.All(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Dept:
                            serverCache.Depts = ProductDeptsService.All(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Stp:
                            serverCache.Categories = ProductCategoriesService.All(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Art:
                            serverCache.Skus = ProductSkusService.All(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.PriceListItem_Customer:
                            serverCache.RetailPrices = PriceListCustomerService.Current(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Arc:
                            serverCache.SkuStocks = SkuStocksService.All(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Pnc:
                            serverCache.SkuPncs = SkuPncsService.All(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Country:
                            serverCache.Countries = CountriesService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Regio:
                            serverCache.Regions = RegionsService.All(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Provincia:
                            serverCache.Provincias = ProvinciasService.All(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Zona:
                            serverCache.Zonas = ZonasService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Location:
                            serverCache.Locations = LocationsService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Zip:
                            serverCache.Zips = ZipsService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.UrlSegment:
                            serverCache.ProductUrls = ProductsService.CanonicalUrls(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.ProductPlugin:
                            serverCache.ProductPlugins = ProductPluginsService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.Noticia:
                            serverCache.Noticias = NoticiasService.All(db, user: null, forCache: true);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.CliGral:
                            serverCache.Contacts = ContactsService.Cache(db, serverCache.EmpId);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.PgcCta:
                            serverCache.PgcCtas = PgcCtasService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                        case CacheDTO.Table.TableIds.StringLocalizer:
                            serverCache.StringsLocalizer = StringsLocalizerService.GetValues(db);
                            serverCache.Tables.First(x => x.Id == table.Id).Fch = DateTime.Now;
                            break;
                    }

                    } catch (Exception ex)
                    {
                        var message = ex.Message;
                    }
                }
            }
        }
    }
}
