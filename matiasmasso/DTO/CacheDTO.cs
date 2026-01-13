using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static DTO.CacheDTO;
using static DTO.LangTextModel;
using static DTO.StoreLocatorDTO;

namespace DTO
{
    public class CacheDTO
    {
        public EmpModel.EmpIds EmpId { get; set; }
        public List<Table> Tables { get; set; } = new();

        //public CatalogModel Catalog { get; set; } = new();
        public AtlasModel Atlas { get; set; } = new();
        public List<UserModel> Users { get; set; } = new();
        public List<ProductBrandModel> Brands { get; set; } = new();
        public List<ProductDeptModel> Depts { get; set; } = new();
        public List<ProductCategoryModel> Categories { get; set; } = new();
        public List<ProductSkuModel> Skus { get; set; } = new();
        public List<SkuStockModel> SkuStocks { get; set; } = new();
        public List<SkuPncModel> SkuPncs { get; set; } = new();
        public List<GuidDecimal> RetailPrices { get; set; } = new();
        public List<CountryModel> Countries { get; set; } = new();
        public List<RegionModel> Regions { get; set; } = new();
        public List<ProvinciaModel> Provincias { get; set; } = new();
        public List<ZonaModel> Zonas { get; set; } = new();
        public List<LocationModel> Locations { get; set; } = new();
        public List<ZipModel> Zips { get; set; } = new();
        public List<CanonicalUrlModel> ProductUrls { get; set; } = new();
        public List<ProductPluginModel> ProductPlugins { get; set; } = new();
        public List<NoticiaModel> Noticias { get; set; } = new();

        public List<GuidNom> Contacts { get; set; } = new();
        public List<PgcCtaModel> PgcCtas { get; set; } = new();

        public List<StringLocalizerModel> StringsLocalizer { get; set; } = new();


        public CacheDTO(EmpModel.EmpIds empId)
        {
            EmpId = empId;
            Tables = Enum.GetValues<Table.TableIds>().Select(x => new Table
            {
                Id = (int)x
            }).ToList();
        }

        public UserModel? User(Guid guid) => Users.FirstOrDefault(x => x.Guid.Equals(guid));
        public class Payload
        {
            public int EmpId { get; set; }
        }

        public CacheDTO Request(params Table.TableIds[] tables)
        {
            var retval = new CacheDTO(EmpId);
            retval.Tables.Clear();
            foreach (var tableId in tables)
            {
                var item = Tables.FirstOrDefault(x => x.Id == (int)tableId);
                if (item != null) retval.Tables.Add(item);
            }
            return retval;
        }


        public CacheDTO CatalogRequest() => Request(Table.TableIds.Tpa, Table.TableIds.Dept, Table.TableIds.Stp, Table.TableIds.Art, Table.TableIds.PriceListItem_Customer, Table.TableIds.Arc, Table.TableIds.Pnc, Table.TableIds.UrlSegment, Table.TableIds.StringLocalizer);
        public CacheDTO AtlasRequest() => Request(Table.TableIds.Country, Table.TableIds.Regio, Table.TableIds.Provincia, Table.TableIds.Zona, Table.TableIds.Location, Table.TableIds.Zip, Table.TableIds.CliGral);

        public CacheDTO MmoStartupRequest()
        {
            return Request(Table.TableIds.StringLocalizer
                , Table.TableIds.Tpa
                , Table.TableIds.Dept
                , Table.TableIds.Stp
                , Table.TableIds.Art
                , Table.TableIds.PriceListItem_Customer
                , Table.TableIds.Arc
                , Table.TableIds.Pnc
                , Table.TableIds.UrlSegment
                , Table.TableIds.Country
                , Table.TableIds.Regio
                , Table.TableIds.Provincia
                , Table.TableIds.Zona
                , Table.TableIds.Location
                , Table.TableIds.Zip
                , Table.TableIds.CliGral
                );
        }
        public ProductBrandModel? Brand(Guid? guid) => guid == null ? null : Brands.FirstOrDefault(x => x.Guid == guid);

        public ProductBrandModel? Brand(ProductModel? product)
        {
            ProductBrandModel? retval = null;
            if (product != null)
            {
                switch (product.Src)
                {
                    case ProductModel.SourceCods.Brand:
                        retval = Brand(product.Guid);
                        break;
                    case ProductModel.SourceCods.Dept:
                        var dept = Dept(product.Guid);
                        retval = dept == null ? null : Brand(dept.Brand);
                        break;
                    case ProductModel.SourceCods.Category:
                        var category = Category(product.Guid);
                        retval = category == null ? null : Brand((Guid)category.Brand!);
                        break;
                    case ProductModel.SourceCods.Sku:
                        var sku = Sku(product.Guid);
                        var skuCategory = sku == null ? null : Category(sku.Category);
                        retval = skuCategory == null ? null : Brand((Guid)skuCategory.Brand!);
                        break;
                }
            }
            return retval;
        }
        public ProductDeptModel? Dept(Guid? guid) => guid == null ? null : Depts.FirstOrDefault(x => x.Guid == guid);
        public ProductCategoryModel? Category(Guid? guid) => guid == null ? null : Categories.FirstOrDefault(x => x.Guid == guid);
        public string ImageUrl(ProductCategoryModel category) => Globals.ApiUrl("productCategory/image", category.Guid.ToString() + ".jpg");
        public string? ImageUrl(ProductSkuModel sku) => SkuImageUrl(sku?.Guid);
        public string? SkuImageUrl(Guid? guid) => guid == null ? null : Globals.ApiUrl("productSku/image", guid.ToString() + ".jpg");
        public ProductSkuModel? Sku(Guid? guid) => guid == null ? null : Skus.FirstOrDefault(x => x.Guid == guid);
        public ProductSkuModel? Sku(string? ean) => ean == null ? null : Skus.FirstOrDefault(x => x.Ean == ean);
        public bool IsAvailable(ProductSkuModel? sku) => sku == null ? false : SkuStock(sku?.Guid)?.Stock > 0;

        public List<ProductSkuModel> GetSkus(ProductCategoryModel category) => Skus.Where(x => x.Category == category.Guid).ToList() ?? new();

        public Decimal? SkuPrice(Guid? guid) => guid == null ? null : RetailPrices.FirstOrDefault(x => x.Guid == guid)?.Value;
        public SkuStockModel? SkuStock(Guid? guid, Guid? mgz = null)
        {
            mgz = mgz == null ? MgzModel.Default().Guid : mgz;
            return guid == null ? null : SkuStocks.FirstOrDefault(x => x.Sku == guid && x.Mgz == mgz);
        }

        public string SkuFullNom(Guid? guid, LangDTO? lang) => guid == null ? "" : SkuFullNom(Sku(guid), lang);

        public string SkuFullNom(ProductSkuModel? sku, LangDTO lang)
        {
            string retval = string.Empty;
            if (sku != null)
            {
                var category = Categories.First(x => x.Guid == sku.Category);
                retval = string.Format("{0} {1}", category?.Nom?.Tradueix(lang), sku?.Nom?.Tradueix(lang));
            }
            return retval;
        }
        public SkuPncModel? SkuPnc(Guid? guid) => guid == null ? null : SkuPncs.FirstOrDefault(x => x.Sku == guid);
        public CountryModel? Country(Guid? guid) => guid == null ? null : Countries.FirstOrDefault(x => x.Guid == guid);
        public List<GuidNom>CountriesGuidNoms(LangDTO lang) => Countries.Select(x=>x.ToGuidNom(lang)).ToList();
       
        public List<ProvinciaModel> CountryProvincias(Guid countryGuid) => Provincias.Where(x=>Regions.Any(y=>x.Region==y.Guid && y.Country == countryGuid)).ToList();
        public ProvinciaModel? Provincia(Guid? guid) => guid == null ? null : Provincias.FirstOrDefault(x => x.Guid == guid);
        public ZonaModel? Zona(Guid? guid) => guid == null ? null : Zonas.FirstOrDefault(x => x.Guid == guid);
        public LocationModel? Location(Guid? guid) => guid == null ? null : Locations.FirstOrDefault(x => x.Guid == guid);

        public LocationModel? Location(LocationModel.Wellknowns id)
        {
            LocationModel? retval = null;
            if (id == LocationModel.Wellknowns.Barcelona)
                retval = Location(new Guid("3B94D3E8-9A82-4628-88ED-CD7BC0F6FD36"));
            return retval;
        }

        public ZipModel? ZipFromLocation(CountryModel country, string location, string zipCod)
        {
            ZipModel? retval = null;
            var countryZonas = Zonas.Where(x => x.Country == country.Guid).ToList();
            var countryLocations = Locations.Where(x => countryZonas.Any(y => x.Zona == y.Guid)).ToList();
            var countryZips = Zips.Where(x => countryLocations.Any(y => x.Location == y.Guid)).ToList();
            var zips = countryZips.Where(x => x.ZipCod == zipCod).ToList();
            var locations = countryLocations.Where(x => x.Nom != null && x.Nom.ToLower() == location.ToLower()).ToList();
            retval = zips.Where(x => locations.Any(y => x.Location == y.Guid)).FirstOrDefault();
            return retval;
        }

        public CountryModel? Country(CountryModel.Wellknowns id)
        {
            if (id == CountryModel.Wellknowns.Spain)
                return Country(new Guid("AEEA6300-DE1D-4983-9AA2-61B433EE4635"));
            else if (id == CountryModel.Wellknowns.Portugal)
                return Country(new Guid("631B1258-9761-4254-8ED9-25B9E42FD6D1"));
            else if (id == CountryModel.Wellknowns.Andorra)
                return Country(new Guid("AE3E6755-8FB7-40A5-A8B3-490ED2C44061"));
            else if (id == CountryModel.Wellknowns.Germany)
                return Country(new Guid("B21500BA-2891-4742-8CFF-8DD65EBB0C82"));
            else
                return null;
        }

        public List<CountryModel> FavoriteCountries()
        {
            var retval = new List<CountryModel>();
            retval.Add(Country(CountryModel.Wellknowns.Spain)!);
            retval.Add(Country(CountryModel.Wellknowns.Portugal)!);
            retval.Add(Country(CountryModel.Wellknowns.Andorra)!);
            return retval;
        }

        public ProductModel? ProductFromGuid(Guid? guid)
        {
            ProductModel? retval = null;
            if (guid != null)
            {
                var brand = Brand((Guid)guid);
                if (brand != null)
                    retval = brand;
                else
                {
                    var dept = Dept((Guid)guid);
                    if (dept != null)
                        retval = dept;
                    else
                    {
                        var category = Category((Guid)guid);
                        if (category != null)
                            retval = category;
                        else
                        {
                            var sku = Sku((Guid)guid);
                            if (sku != null)
                                retval = sku;
                        }
                    }
                }
            }
            return retval;
        }
        public string? FullNom(Guid? guid, LangDTO lang)
        {
            string? retval = null;
            var product = ProductFromGuid(guid);
            if (product != null)
            {
                switch (product.Src)
                {
                    case ProductModel.SourceCods.Brand:
                        retval = product.Nom.Tradueix(lang);
                        break;
                    case ProductModel.SourceCods.Dept:
                        ProductDeptModel dept = (ProductDeptModel)product;
                        if (dept != null)
                        {
                            var brand = Brand(dept.Brand);
                            retval = string.Format("{0} {1}", brand.Nom.Tradueix(lang), dept.Nom.Tradueix(lang));
                        }
                        break;
                    case ProductModel.SourceCods.Category:
                        ProductCategoryModel category = (ProductCategoryModel)product;
                        if (category != null)
                        {
                            var brand = Brand((Guid)category.Brand!);
                            retval = string.Format("{0} {1}", brand?.Nom.Tradueix(lang) ?? "", category.Nom.Tradueix(lang));
                        }
                        break;
                    case ProductModel.SourceCods.Sku:
                        ProductSkuModel sku = (ProductSkuModel)product;
                        if (sku != null)
                        {
                            var item = Category(sku.Category);
                            if (item != null)
                            {
                                var brand = Brand((Guid)item.Brand!);
                                retval = string.Format("{0} {1} {2}", brand?.Nom.Tradueix(lang) ?? "", item.Nom.Tradueix(lang), sku.Nom.Tradueix(lang));
                            }
                        }
                        break;
                }
            }

            return retval;
        }
        public ProductModel? ProductFromUrl(string url)
        {
            ProductModel? retval = null;
            CanonicalUrlModel? item = null;
            LangDTO? lang = LangDTO.FromUrl(url);
            if (lang == null)
            {
                var trimmedUrl = url.StartsWith("/") ? url.Substring(1) : url;
                var items = ProductUrls.Where(x => !string.IsNullOrEmpty(x.Url.Esp) && x.Url.Esp.StartsWith("tom")).OrderBy(x => x.Url.Esp).ToList();
                item = ProductUrls.FirstOrDefault(x => x.Url.Matches(trimmedUrl));
            }
            else
            {
                var langPos = url.ToLower().IndexOf(lang.Culture2Digits() + '/');
                var trimmedUrl = url.Substring(langPos + 3);
                item = ProductUrls.FirstOrDefault(x => x.Url.Text(lang) == trimmedUrl);
            }
            if (item != null)
                retval = ProductFromGuid(item.Target);
            return retval;
        }

        public string? ProductUrl(Guid guid, LangDTO lang) => ProductUrls.FirstOrDefault(x => x.Target == guid)?.Url.Tradueix(lang);

        public string? ExpandPlugins(string? src, LangDTO lang)
        {
            string? retval = src;
            if (src != null)
            {
                var guids = ProductPluginModel.ExtractPluginGuids(src);
                foreach (var guid in guids)
                {
                    var plugin = ProductPlugins.FirstOrDefault(x => x.Guid == guid);
                    if (plugin == null)
                        plugin = ProductPluginModel.CategoryCollection(this, guid);
                    if (plugin != null)
                        retval = Regex.Replace(src, plugin.Snippet(), plugin.Expansion(this, lang), RegexOptions.IgnoreCase);
                }
                retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", ProductPluginModel.StoreLocatorCallToActionExpanded(lang));
            }
            return retval;
        }

        public void AddTables(List<Table> src, params Table.TableIds[] request)
        {
            foreach (var tableId in request)
            {
                var item = src.FirstOrDefault(x => x.Id == (int)tableId);
                if (item != null) Tables.Add(item);
            }
        }

        public class Table
        {
            public int Id { get; set; }
            public DateTime? Fch { get; set; } //Date the table was last updated

            public enum TableIds
            {
                Tpa,
                Dept,
                Stp,
                Art,
                PriceListItem_Customer,
                Pnc,
                Arc,
                Country,
                Regio,
                Provincia,
                Zona,
                Location,
                Zip,
                CliGral,
                PgcCta,
                UrlSegment,
                ProductPlugin,
                Noticia,
                StringLocalizer
            }

            public override string ToString()
            {
                return String.Format("{0} {1:dd/MM/yy HH:mm}", ((TableIds)Id).ToString(), Fch);
            }
        }

        //on server, prepares response to client
        public CacheDTO UpdatedClient(List<CacheDTO.Table> dirtyTables)
        {
            var retval = new CacheDTO(EmpId);
            retval.Tables = dirtyTables;
            Merge(dirtyTables, this, retval);
            return retval;
        }


        //on client, updates cache with response from server
        public void Merge(CacheDTO serverCache)
        {
            Merge(serverCache.Tables, serverCache, this);
        }

        private static void Merge(List<CacheDTO.Table> dirtyTables, CacheDTO src, CacheDTO destination)
        {
            foreach (var table in dirtyTables)
            {
                var destinationTable = destination.Tables.First(x => x.Id == table.Id);
                switch ((CacheDTO.Table.TableIds)table.Id)
                {
                    case CacheDTO.Table.TableIds.Tpa:
                        destination.Brands = src.Brands;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Dept:
                        destination.Depts = src.Depts;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Stp:
                        destination.Categories = src.Categories;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Art:
                        destination.Skus = src.Skus;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Arc:
                        destination.SkuStocks = src.SkuStocks;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Pnc:
                        destination.SkuPncs = src.SkuPncs;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.PriceListItem_Customer:
                        destination.RetailPrices = src.RetailPrices;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Country:
                        destination.Countries = src.Countries;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Regio:
                        destination.Regions = src.Regions;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Provincia:
                        destination.Provincias = src.Provincias;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Zona:
                        destination.Zonas = src.Zonas;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Location:
                        destination.Locations = src.Locations;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Zip:
                        destination.Zips = src.Zips;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.UrlSegment:
                        destination.ProductUrls = src.ProductUrls;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.ProductPlugin:
                        destination.ProductPlugins = src.ProductPlugins;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.Noticia:
                        destination.Noticias = src.Noticias;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.CliGral:
                        destination.Contacts = src.Contacts;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.PgcCta:
                        destination.PgcCtas = src.PgcCtas;
                        destinationTable.Fch = table.Fch;
                        break;
                    case CacheDTO.Table.TableIds.StringLocalizer:
                        destination.StringsLocalizer = src.StringsLocalizer;
                        destinationTable.Fch = table.Fch;
                        break;

                }
            }
        }

        //gets a list of tables the client need to update
        public List<CacheDTO.Table> ClientDirtyTables(List<CacheDTO.Table> request)
        {
            var retval = new List<CacheDTO.Table>();
            foreach (var clientTable in request)
            {
                var serverTable = Tables.FirstOrDefault(x => x.Id == clientTable.Id);
                if (serverTable != null && (clientTable.Fch == null || serverTable.Fch > clientTable.Fch))
                    retval.Add(serverTable);
            }
            return retval;
        }

        public string Localizer(LangDTO lang, string stringKey, params object?[] parameters)
        {
            var retval = StringsLocalizer
                .Where(x => x.StringKey == stringKey)
                .FirstOrDefault()?.Tradueix(lang) ?? stringKey;
            if (parameters.Length > 0)
                retval = string.Format(retval, parameters);
            return retval;
        }
    }
}
