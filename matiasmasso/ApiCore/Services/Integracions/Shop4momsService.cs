using Api.Entities;
using Api.Extensions;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Integracions.Shop4moms;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Services.Integracions
{
    public class Shop4momsService
    {

        public static List<ProductCategoryModel> Categories()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            using (var db = new Entities.MaxiContext())
            {
                return db.Vw4momsCategories
                    .Where(x => x.Brand == brand.Guid)
                    .OrderBy(x => x.Codi)
                    .ThenBy(x => x.Ord)
                    .ThenBy(x => x.NomEsp)
                    .Select(x => new ProductCategoryModel(x.Guid)
                    {
                        Brand = brand.Guid,
                        Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.ProductNom, x.NomEsp, x.NomCat, x.NomEng, x.NomPor),
                        Codi = x.Codi,
                        Ord = x.Ord,
                        Excerpt = new LangTextModel(x.Guid, LangTextModel.Srcs.Shop4momsProductExcerpt, x.ExcerptEsp, x.ExcerptCat, x.ExcerptEng, x.ExcerptPor),
                        Content = new LangTextModel(x.Guid, LangTextModel.Srcs.Shop4momsProductText, x.ContentEsp, x.ContentCat, x.ContentEng, x.ContentPor),
                        HasImage = x.HasImage == 1,
                        Obsoleto = x.Obsoleto
                        
                    })
                    .ToList();
            }
        }
        public static List<ProductSkuModel> Skus()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Vw4momsSkus
                    .Where(x => x.BrandGuid == brand.Guid)
                    .Select(x => new ProductSkuModel(x.SkuGuid)
                    {
                        Category = x.CategoryGuid,
                        Ref = x.SkuRef,
                        Nom = new LangTextModel(x.SkuGuid, LangTextModel.Srcs.ProductNom, x.SkuNomEsp, x.SkuNomCat, x.SkuNomEng, x.SkuNomPor),
                        NomLlarg = new LangTextModel(x.SkuGuid, LangTextModel.Srcs.SkuNomLlarg, x.SkuNomLlargEsp, x.SkuNomLlargCat, x.SkuNomLlargEng, x.SkuNomLlargPor),
                        Excerpt = new LangTextModel(x.SkuGuid, LangTextModel.Srcs.Shop4momsProductExcerpt, x.ExcerptEsp, x.ExcerptCat, x.ExcerptEng, x.ExcerptPor),
                        Content = new LangTextModel(x.SkuGuid, LangTextModel.Srcs.Shop4momsProductText, x.ContentEsp, x.ContentCat, x.ContentEng, x.ContentPor),
                        HasImage = x.HasImage == 1,
                        ImgExists = x.HasImage == 1,
                        Obsoleto = x.Obsoleto == 1
                    })
                    .ToList();
                return retval;
            }
        }

        public static List<GuidInt> SkuStocks()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Vw4momsSkuStocks
                    .Where(x => x.Brand == brand.Guid && ((x.Stock ?? 0) - (x.Clients ?? 0) >= 0))
                    .Select(x => new GuidInt((Guid?)x.SkuGuid ?? Guid.NewGuid(), (x.Stock ?? 0) - (x.Clients ?? 0)))
                    .ToList();
                return retval;
            }
        }


        public static List<GuidDecimal> SkuRetails()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            using (var db = new Entities.MaxiContext())
            {
                return db.Vw4momsSkuRetails
                    .Where(x => x.Brand == brand.Guid)
                    .Select(x => new GuidDecimal(x.SkuGuid, x.Retail))
                    .ToList();
            }
        }
        public static RouteModel.Collection Routes()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new RouteModel.Collection();
                retval.AddRange(ContentRoutes(db));
                retval.AddRange(ProductRoutes(db));
                return retval;
            }
        }

        public static List<ProductPluginModel> ProductPlugins()
        {
            using (var db = new Entities.MaxiContext())
            {
                return ProductPluginsService.GetValues(db);
            }
        }

        public static List<YouTubeMovieModel> Videos()
        {
            return new();
            //using (var db = new Entities.MaxiContext())
            //{
            //    var retval =  db.VwYouTubes
            //}
        }

        #region Deprecated

        public static DTO.Integracions.Shop4moms.Cache Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DTO.Integracions.Shop4moms.Cache();
                LoadRoutes(db, ref retval);
                LoadCatalog(db, ref retval);
                LoadProductPlugins(db, ref retval);
                LoadTpv(db, ref retval);
                LoadStringsLocalizer(db, ref retval);
                return retval;
            }
        }

        public static void SaveBasket(ShoppingBasketModel basket)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.SaveChanges();
            }
        }

        private static void LoadTpv(Entities.MaxiContext db, ref Cache value)
        {
            Guid tpv4moms = DTO.Integracions.Redsys.Tpv.Wellknown(DTO.Integracions.Redsys.Tpv.Ids.Shop4moms)!.Guid;
            //value.Tpv = TpvService.Find(db, tpv4moms);
        }

        private static void LoadStringsLocalizer(Entities.MaxiContext db, ref Cache value)
        {
            value.StringsLocalizer = StringsLocalizerService.GetValues(db);
        }

        public static ShoppingBasketModel LogResponse(DTO.Integracions.Redsys.TpvLog tpvLog)
        {
            ShoppingBasketModel? basket = null;
            using (var db = new MaxiContext())
            {
                //recover persisted basket from Tpv order number and expand user 
                basket = ShoppingBasketService.FromTpvOrder(db, tpvLog.Ds_Order)!;
                basket.User = UserService.Find(basket.User!.Guid)!;
                CcaModel? cca = null;

                if (tpvLog.Success())
                {
                    ZipDTO? zip = ShoppingBasketService.FindOrCreateZip(basket);
                    GuidNom? mgz = MgzModel.Default() == null ? null : new GuidNom(MgzModel.Default()!.Guid, MgzModel.Default()!.Abr);

                    //recover shop4moms tpv. TODO: not all tpvs will come from 4moms shop
                    var tpv = TpvService.Find(db, DTO.Integracions.Redsys.Tpv.Ids.Shop4moms);

                    //build and save accounts registry
                    cca = TpvLogService.LogCca(db, tpv, tpvLog, basket);
                    CcaService.Update(db, cca);

                    //build and save purchase order
                    var purchaseOrder = basket.PurchaseOrderFactory();

                    var itemsWith = new List<PurchaseOrderModel.Item>();
                    foreach (var item in purchaseOrder.Items)
                    {
                        var skuWiths = SkuWithsService.GetChildren(db, (Guid)item.Sku!);
                        foreach (var skuWith in skuWiths)
                        {
                            var itemWith = new PurchaseOrderModel.Item
                            {
                                Qty = (int)skuWith.Value!,
                                Pending = 0, // no units left since delivery is generated right afterwards
                                ChargeCod = PurchaseOrderModel.Item.ChargeCods.FOC,
                                Price = 0,
                                Sku = skuWith.Guid
                            };
                            itemsWith.Add(itemWith);
                        }
                    }

                    if (itemsWith.Count > 0) purchaseOrder.Items.AddRange(itemsWith);

                    PurchaseOrderService.Update(db, purchaseOrder);
                    basket.PurchaseOrder = purchaseOrder;
                    ShoppingBasketService.UpdateHeader(db, basket);

                    var cache = Api.Shared.AppState.DefaultCache();
                    var zipModel = cache.Zips.FirstOrDefault(x => x.Guid == zip?.Guid);
                    var location = cache.Location(zipModel?.Location);
                    var zona = cache.Zona(location?.Zona);

                    //deliver the order
                    var delivery = new DeliveryModel
                    {
                        Emp = purchaseOrder.Emp,
                        Fch = DateTime.Now,
                        Cod = purchaseOrder.Cod,
                        Contact = purchaseOrder.Contact,
                        PortsCod = CustomerModel.PortsCodes.pagats,
                        CashCod = CustomerModel.CashCodes.visa,
                        Nom = $"{basket.FirstName} {basket.LastName} {basket.LastName2}",
                        Address = new AddressModel { Text = basket.Address, Zip = zip },
                        Tel = basket.Tel?.Replace(" ",""),
                        ExportCod = zona?.ExportCod,
                        Incoterm = "DAP",
                        Mgz = mgz,
                        ObsTransp = basket.TrpObs ?? "",
                        Fpg = tpvLog.FormaDePago(),
                        UsrLog = purchaseOrder.UsrLog,
                        Items = purchaseOrder.Items.Select(x => new DeliveryModel.Item
                        {
                            Qty = x.Qty,
                            Price = x.Price,
                            Dto = x.Dto,
                            Sku = x.Sku,
                            PncGuid = x.Guid,
                            PdcGuid = purchaseOrder.Guid,
                            MgzGuid = mgz?.Guid
                        }).ToList()

                    };

                    DeliveryService.Update(db, delivery);

                    //build and save the consumer ticket
                    var ticket = ConsumerTicketModel.Factory(basket.User, basket.Lang!, basket.MarketPlace!, basket.OrderNum!);
                    ticket.Nom = basket.FirstName;
                    ticket.Cognom1 = basket.LastName;
                    ticket.Cognom2 = basket.LastName2;
                    ticket.Address = basket.Address;
                    ticket.ConsumerZip = basket.ZipCod;
                    ticket.ConsumerLocation = basket.Location;
                    ticket.Zip = zip?.Guid;
                    ticket.Tel = basket.Tel?.Replace(" ","");
                    ticket.Cca = new GuidNom(cca.Guid, cca.Concept);
                    ticket.PurchaseOrder = new GuidNom(purchaseOrder.Guid);
                    ticket.Delivery = new GuidNom(delivery.Guid);
                    ticket.Goods = basket.Items?.Sum(x => x.Amount());
                    ConsumerTicketService.Update(db, ticket);

                }

                //edit original book request to reflect tpv result
                TpvLogService.LogResponse(db, tpvLog, cca);

                db.SaveChanges();
            }

            return basket;
        }


        private static void LoadProductPlugins(Entities.MaxiContext db, ref Cache value)
        {
            value.ProductPlugins = ProductPluginsService.GetValues(db);
        }

        public static LangTextModel? Content(string segment)
        {
            using (var db = new Entities.MaxiContext())
            {
                var oSrc = LangTextModel.Srcs.Shop4momsUrl;
                Guid? guid = db.LangTexts
                    .Where(x => x.Src == (int)oSrc && x.Text == segment)
                    .Select(x => x.Guid)
                    .FirstOrDefault();

                LangTextModel? retval = null;
                if (guid != null)
                {
                    oSrc = LangTextModel.Srcs.Shop4momsText;
                    retval = db.VwLangTexts
                        .Where(x => x.Guid == guid && x.Src == (int)oSrc)
                        .Select(x => new LangTextModel(x.Guid, oSrc)
                        {
                            Esp = x.Esp,
                            Cat = x.Cat,
                            Eng = x.Eng,
                            Por = x.Por
                        })
                        .FirstOrDefault();
                }

                return retval;
            }
        }


        private static void LoadRoutes(Entities.MaxiContext db, ref Cache value)
        {
            value.Routes = new RouteModel.Collection();
            value.Routes.AddRange(ContentRoutes(db));
            value.Routes.AddRange(ProductRoutes(db));
        }

        private static List<RouteModel> ContentRoutes(Entities.MaxiContext db)
        {
            var src = LangTextModel.Srcs.Shop4momsUrl;
            return db.LangTexts
                .Where(x => x.Src == (int)src)
                .Select(x => new RouteModel(x.Guid)
                {
                    Src = RouteModel.Srcs.Content,
                    Lang = new LangDTO(x.Lang),
                    Segment = x.Text
                }).ToList();
        }

        private static List<RouteModel> ProductRoutes(Entities.MaxiContext db)
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            return db.Vw4momsProductRoutes
                .Where(x => x.Brand == brand.Guid)
                .OrderBy(x => x.Category)
                .ThenBy(x => x.Sku)
                .ThenBy(x => x.Lang)
                .Select(x => new RouteModel(x.Sku == null ? (Guid)x.Category! : (Guid)x.Sku)
                {
                    Src = x.Sku == null ? RouteModel.Srcs.Category : RouteModel.Srcs.Sku,
                    Lang = new LangDTO(x.Lang),
                    Segment = x.Sku == null ? x.CategorySegment : string.Format("{0}/{1}", x.CategorySegment, x.SkuSegment)
                }).ToList();
        }


        private static List<RouteModel> ProductRoutes_Deprecated(Entities.MaxiContext db)
        {
            List<RouteModel> retval = new();
            Guid brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!.Guid;

            var connectionDb = db.Database.GetDbConnection();
            if (connectionDb.State.Equals(ConnectionState.Closed))
                connectionDb.Open();
            using (var command = connectionDb.CreateCommand())
            {
                var sb = new StringBuilder();
                sb.AppendLine("SELECT VwProductGuid.Category, VwProductGuid.Sku ");
                sb.AppendLine("       , CategoryUrl.Lang ");
                sb.AppendLine("       , CategoryUrl.Segment AS CategorySegment ");
                sb.AppendLine("	      , SkuUrl.Segment AS SkuSegment ");
                sb.AppendLine("FROM   dbo.VwProductGuid ");
                sb.AppendLine("LEFT OUTER JOIN UrlSegment AS CategoryUrl ON dbo.VwProductGuid.Category = CategoryUrl.Target ");
                sb.AppendLine("LEFT OUTER JOIN UrlSegment AS SkuUrl ON dbo.VwProductGuid.Sku = SkuUrl.Target ");
                sb.AppendLine("WHERE VwProductGuid.Brand='" + brand.ToString() + "' ");
                sb.AppendLine("AND CategoryUrl.Canonical=1  ");
                sb.AppendLine("AND (SkuUrl.Canonical IS NULL OR SkuUrl.Canonical = 1) ");
                sb.AppendLine("AND (SkuUrl.Lang IS NULL OR SkuUrl.Lang = CategoryUrl.Lang ) ");
                sb.AppendLine("ORDER by VwProductGuid.Category, VwProductGuid.Sku, CategoryUrl.Lang ");

                var SqlCommand = sb.ToString();
                command.CommandText = SqlCommand;
                RouteModel item;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var skuGuid = reader.GetNullableGuid("Sku");
                        if (skuGuid == null)
                            item = new RouteModel(reader.GetGuid("Category"))
                            {
                                Src = RouteModel.Srcs.Category,
                                Lang = new LangDTO(reader.GetString("Lang")),
                                Segment = reader.GetString("CategorySegment")
                            };
                        else
                            item = new RouteModel((Guid)skuGuid)
                            {
                                Src = RouteModel.Srcs.Sku,
                                Lang = new LangDTO(reader.GetString("Lang")),
                                Segment = string.Format("{0}/{1}", reader.GetString("CategorySegment"), reader.GetString("SkuSegment"))
                            };
                        retval.Add(item);
                    }
                }
            }
            return retval;
        }


        public static void LoadCatalog(Entities.MaxiContext db, ref DTO.Integracions.Shop4moms.Cache value)
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            var mgz = new Guid("41A81ACA-1C01-44FC-BF57-2728B03F74D8"); //TODO get default mgz
            var marketPlace = MarketPlaceModel.Wellknown(MarketPlaceModel.Wellknowns.Shop4moms)!;
            ProductCategoryModel category = new ProductCategoryModel();

            var connectionDb = db.Database.GetDbConnection();
            if (connectionDb.State.Equals(ConnectionState.Closed))
                connectionDb.Open();
            using (var command = connectionDb.CreateCommand())
            {
                var sb = new StringBuilder();
                sb.AppendLine("SELECT VwSkuNom.CategoryGuid , VwSkuNom.CategoryCodi, VwSkuNom.SkuRef, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ");
                sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ");
                sb.AppendLine(", VwSkuNom.SkuNomLlargEsp, VwSkuNom.SkuNomLlargCat, VwSkuNom.SkuNomLlargEng, VwSkuNom.SkuNomLlargPor ");
                sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ");
                sb.AppendLine(", VwSkuRetail.Retail, Offers.Price AS Offer ");
                sb.AppendLine(", CategoryText.ExcerptEsp AS CategoryExcerptEsp, CategoryText.ExcerptCat AS CategoryExcerptCat, CategoryText.ExcerptEng AS CategoryExcerptEng, CategoryText.ExcerptPor AS CategoryExcerptPor ");
                sb.AppendLine(", CategoryText.ContentEsp AS CategoryContentEsp, CategoryText.ContentCat AS CategoryContentCat, CategoryText.ContentEng AS CategoryContentEng, CategoryText.ContentPor AS CategoryContentPor ");
                sb.AppendLine(", SkuText.ExcerptEsp AS SkuExcerptEsp, SkuText.ExcerptCat AS SkuExcerptCat, SkuText.ExcerptEng AS SkuExcerptEng, SkuText.ExcerptPor AS SkuExcerptPor ");
                sb.AppendLine(", SkuText.ContentEsp AS SkuContentEsp, SkuText.ContentCat AS SkuContentCat, SkuText.ContentEng AS SkuContentEng, SkuText.ContentPor AS SkuContentPor ");
                sb.AppendLine("FROM VwSkuNom ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" + mgz.ToString() + "' ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwSkuNom.SkuGuid = VwSkuRetail.SkuGuid ");
                sb.AppendLine("LEFT OUTER JOIN VwShopProductText CategoryText ON VwSkuNom.CategoryGuid = CategoryText.Guid ");
                sb.AppendLine("LEFT OUTER JOIN VwShopProductText SkuText ON VwSkuNom.SkuGuid = SkuText.Guid ");
                sb.AppendLine("LEFT OUTER JOIN Offers ON VwSkuNom.SkuGuid = Offers.Sku AND Offers.Parent = '" + marketPlace.Guid.ToString() + "' ");
                sb.AppendLine("WHERE VwSkuNom.BrandGuid='" + brand.Guid.ToString() + "' ");
                sb.AppendLine("AND VwSkuNom.Obsoleto = 0 AND VwSkuNom.CategoryCodi<3 AND VwSkuNom.SkuImageExists = 1 ");
                sb.AppendLine("ORDER BY VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryGuid, VwSkuNom.SkuNomEsp ");
                var SqlCommand = sb.ToString();
                command.CommandText = SqlCommand;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categoryGuid = reader.GetGuid("CategoryGuid");
                        if (categoryGuid != category.Guid)
                        {
                            category = new ProductCategoryModel(categoryGuid)
                            {
                                Brand = brand.Guid,
                                Nom = new LangTextModel(reader.GetNullableString("CategoryNomEsp"), reader.GetNullableString("CategoryNomCat"), reader.GetNullableString("CategoryNomEng"), reader.GetNullableString("CategoryNomPor")),
                                Codi = reader.GetNullableInt32("CategoryCodi") ?? 0,
                                Ord = reader.GetNullableInt32("CategoryOrd") ?? 0,
                                Excerpt = new LangTextModel(categoryGuid, LangTextModel.Srcs.Shop4momsProductExcerpt)
                                {
                                    Esp = reader.GetNullableString("CategoryExcerptEsp"),
                                    Cat = reader.GetNullableString("CategoryExcerptCat"),
                                    Eng = reader.GetNullableString("CategoryExcerptEng"),
                                    Por = reader.GetNullableString("CategoryExcerptPor")
                                },
                                Content = new LangTextModel(categoryGuid, LangTextModel.Srcs.Shop4momsProductText)
                                {
                                    Esp = reader.GetNullableString("CategoryContentEsp"),
                                    Cat = reader.GetNullableString("CategoryContentCat"),
                                    Eng = reader.GetNullableString("CategoryContentEng"),
                                    Por = reader.GetNullableString("CategoryContentPor")
                                }
                            };
                            value.Categories.Add(category);
                        }

                        var skuGuid = reader.GetGuid("SkuGuid");

                        try
                        {
                            var sku = new ProductSkuModel(skuGuid)
                            {
                                Category = categoryGuid,
                                Ref = reader.GetNullableString("SkuRef"),
                                Nom = new LangTextModel(reader.GetNullableString("SkuNomEsp"), reader.GetNullableString("SkuNomCat"), reader.GetNullableString("SkuNomEng"), reader.GetNullableString("SkuNomPor")),
                                NomLlarg = new LangTextModel(skuGuid, LangTextModel.Srcs.SkuNomLlarg)
                                {
                                    Esp = reader.GetNullableString("SkuNomLlargEsp"),
                                    Cat = reader.GetNullableString("SkuNomLlargCat"),
                                    Eng = reader.GetNullableString("SkuNomLlargEng"),
                                    Por = reader.GetNullableString("SkuNomLlargPor")
                                },
                                Excerpt = new LangTextModel(skuGuid, LangTextModel.Srcs.Shop4momsProductExcerpt)
                                {
                                    Esp = reader.GetNullableString("skuExcerptEsp"),
                                    Cat = reader.GetNullableString("skuExcerptCat"),
                                    Eng = reader.GetNullableString("skuExcerptEng"),
                                    Por = reader.GetNullableString("skuExcerptPor")
                                },
                                Content = new LangTextModel(skuGuid, LangTextModel.Srcs.Shop4momsProductText)
                                {
                                    Esp = reader.GetNullableString("skuContentEsp"),
                                    Cat = reader.GetNullableString("skuContentCat"),
                                    Eng = reader.GetNullableString("skuContentEng"),
                                    Por = reader.GetNullableString("skuContentPor")
                                }

                            };
                            value.Skus.Add(sku);

                            if (!reader.IsDBNull("Retail"))
                            {
                                value.RetailPrices.Add(new GuidDecimal(skuGuid, reader.GetDecimal("Retail")));
                            }
                            if (!reader.IsDBNull("Offer"))
                            {
                                value.Offers.Add(new GuidDecimal(skuGuid, reader.GetDecimal("Offer")));
                            }
                            if (!reader.IsDBNull("Stock"))
                            {
                                value.SkuStocks.Add(new SkuStockModel { Sku = skuGuid, Stock = reader.GetInt32("Stock") });
                            }

                        }
                        catch (System.Exception ex)
                        {
                            var s = ex.Message;
                        }


                    }
                }
            }
        }

        public static ContentModel? Content(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwContents
                .Where(x => x.Guid == guid)
                .Select(x => new ContentModel(guid)
                {
                    Caption = new LangTextModel(guid, LangTextModel.Srcs.ContentTitle)
                    {
                        Esp = x.TitleEsp,
                        Cat = x.TitleCat,
                        Eng = x.TitleEng,
                        Por = x.TitlePor
                    },
                    Excerpt = new LangTextModel(guid, LangTextModel.Srcs.ContentExcerpt)
                    {
                        Esp = x.ExcerptEsp,
                        Cat = x.ExcerptCat,
                        Eng = x.ExcerptEng,
                        Por = x.ExcerptPor
                    },
                    Content = new LangTextModel(guid, LangTextModel.Srcs.ContentText)
                    {
                        Esp = x.ContentEsp,
                        Cat = x.ContentCat,
                        Eng = x.ContentEng,
                        Por = x.ContentPor
                    }
                })
                .FirstOrDefault();
            }
        }

        #endregion
    }
}
