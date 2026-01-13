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
        public static Cache Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new Cache();
                LoadCatalog(db, ref retval);
                LoadContentUrls(db, ref retval);
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
        public static void SaveBasket2(ShoppingBasketModel basket)
        {
            var purchaseOrder = basket.PurchaseOrder();
            var consumerTicket = basket.ConsumerTicket(purchaseOrder);
            var mgz = MgzModel.Wellknown(MgzModel.Wellknowns.vivaceLliça);
            var delivery = consumerTicket.Deliver(mgz);

            using (var db = new Entities.MaxiContext())
            {
                PurchaseOrderService.Update(db, purchaseOrder);
                ConsumerTicketService.Update(db, consumerTicket);
                DeliveryService.Update(db, delivery);
                db.SaveChanges();
            }
        }

        private static void LoadContentUrls(Entities.MaxiContext db, ref Cache value)
        {
            var src = LangTextModel.Srcs.Shop4momsUrl;
            value.ContentUrls = db.VwLangTexts
                .Where(x => x.Src == (int)src)
                .Select(x => new LangTextModel(x.Guid, src)
                {
                    Esp = x.Esp,
                    Cat = x.Cat,
                    Eng = x.Eng,
                    Por = x.Por
                }).ToList();
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
        public static void LoadCatalog(Entities.MaxiContext db, ref Cache value)
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
                sb.AppendLine("SELECT VwSkuNom.CategoryGuid , VwSkuNom.CategoryCodi, VwSkuNom.SkuRef, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ");
                sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ");
                sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ");
                sb.AppendLine(", VwSkuRetail.Retail, Offers.Price AS Offer ");
                sb.AppendLine("FROM VwSkuNom ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" + mgz.ToString() + "' ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ");
                sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwSkuNom.SkuGuid = VwSkuRetail.SkuGuid ");
                sb.AppendLine("LEFT OUTER JOIN Offers ON VwSkuNom.SkuGuid = Offers.Sku AND Offers.Parent = '" + marketPlace.Guid.ToString() + "' ");
                sb.AppendLine("WHERE VwSkuNom.BrandGuid='" + brand.Guid.ToString() + "' ");
                sb.AppendLine("AND VwSkuNom.Obsoleto = 0 AND VwSkuNom.CategoryCodi<3 ");
                sb.AppendLine("ORDER BY VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.SkuNomEsp ");
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
                                Nom = new LangTextModel(reader.GetString("CategoryNomEsp"), reader.GetString("CategoryNomCat"), reader.GetString("CategoryNomEng"), reader.GetString("CategoryNomPor")),
                                Codi = reader.GetInt32("CategoryCodi")
                            };
                            value.Categories.Add(category);
                        }

                        var skuGuid = reader.GetGuid("SkuGuid");

                        try
                        {
                            var sku = new ProductSkuModel(skuGuid)
                            {
                                Category = categoryGuid,
                                Ref=reader.GetString("SkuRef"),
                                Nom = new LangTextModel(reader.GetString("SkuNomEsp"), reader.GetString("SkuNomCat"), reader.GetString("SkuNomEng"), reader.GetString("SkuNomPor"))
                            };
                            value.Skus.Add(sku);

                            if (!reader.IsDBNull("Retail"))
                            {
                                value.Prices.Add(new GuidDecimal(skuGuid, reader.GetDecimal("Retail")));
                            }
                            if (!reader.IsDBNull("Offer"))
                            {
                                value.Offers.Add(new GuidDecimal(skuGuid, reader.GetDecimal("Offer")));
                            }
                            if (!reader.IsDBNull("Stock"))
                            {
                                value.Stocks.Add(new SkuStockDTO { Sku= skuGuid, Stock= reader.GetInt32("Stock") });
                            }

                        }
                        catch (Exception ex)
                        {
                            var s = ex.Message;
                        }


                    }
                }
            }
        }
    }
}
