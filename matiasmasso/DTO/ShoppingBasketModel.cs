using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DTO.Integracions.Shop4moms;

namespace DTO
{
    public class ShoppingBasketModel : BaseGuid, IModel
    {
        public UserModel? User { get; set; }
        public DateTime? Fch { get; set; }
        public MarketPlaceModel? MarketPlace { get; set; }
        public string? OrderNum { get; set; }
        public string? TpvOrderNum { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }
        public Guid? Country { get; set; }
        public string? ZipCod { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public decimal? Amount { get; set; }
        public string? TrpObs { get; set; }

        public decimal? Ports { get; set; }

        public PurchaseOrderModel? PurchaseOrder { get; set; }
        public List<Item> Items { get; set; } = new();

        public LangDTO? Lang { get; set; }



        public ShoppingBasketModel() : base()
        {
            OrderNum = DTO.Helpers.CryptoHelper.RandomString(10);
        }
        public ShoppingBasketModel(Guid guid) : base(guid) { }

        public ShoppingBasketModel(UserModel user, MarketPlaceModel? marketPlace )
        {
            User = user;
            MarketPlace = marketPlace;
            OrderNum = DTO.Helpers.CryptoHelper.RandomString(10);
        }

        public bool IsEmpty() => Items.Count == 0;

        public void AddItem(ProductSkuModel sku, decimal? price)
        {
            Items.Add(new Item { Sku = sku.Guid, Price = price });
        }

        public decimal Cash() => Items.Sum(x => x.Amount()) + (Ports ?? 0);
        public string Fullnom() => string.Format("{0} {1} {2}", FirstName, LastName, LastName2).Trim();

        public string ZipLocation() => string.Format("{0} {1}", ZipCod, Location).Trim();
        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

        public string FullAddress(CacheDTO? cache)
        {
            var retval = string.Format("{0}-{1}", Address, Location);
            if(cache != null && Country != CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!.Guid) {
                retval = string.Format("{0} ({1})", retval, cache.Country(Country));
            }
            return retval;

        }

        public bool ShouldShowTotals() => (HasPorts() && Items.Count > 0) || (Items.Count > 1);
        public bool HasPorts() => Ports != null && Ports > 0;

        public class Item
        {
            public Guid? Sku { get; set; }
            public int Qty { get; set; } = 1;
            public decimal? Price { get; set; }
            public decimal? Dto { get; set; }

            //ref to PurchaseOrderItem so it can be passed to DeliveryItem
            public Guid? PncGuid { get; set; }
            public decimal Amount() => Math.Round(Qty * (Price ?? 0) * (100-(Dto ?? 0))/100,2) ;
            public bool IsEmpty() => Qty <= 0;
        }

        public ConsumerTicketModel ConsumerTicket(PurchaseOrderModel purchaseOrder)
        {
            var retval = ConsumerTicketModel.Factory(User!, Lang!, MarketPlace!, OrderNum!);
            retval.Nom = FirstName;
            retval.Cognom1 = LastName;
            retval.Cognom2 = LastName2;
            retval.Address = Address;
            retval.ConsumerZip = ZipCod;
            retval.ConsumerLocation = Location;
            retval.BuyerEmail = User.EmailAddress;
            //retval.ConsumerProvincia = Prov
            //retval.Tel = Tel;

            retval.PurchaseOrder = new GuidNom(purchaseOrder.Guid, purchaseOrder.ToString());
            foreach (var item in Items)
            {
                retval.Items.Add(new ConsumerTicketModel.Item
                {
                    Qty = item.Qty,
                    Price = item.Price ?? 0,
                    Sku = (Guid)item.Sku!,
                    PncGuid = (Guid)item.PncGuid!
                });
            }
            return retval;
        }

        public PurchaseOrderModel PurchaseOrderFactory()
        {
            decimal VatTipus = 21;
            decimal VatFactor = DevengaIva() ? 100 / (100 + VatTipus) : 1;


            var retval = new PurchaseOrderModel()
            {
                Emp = User!.Emp,
                UsrLog = UsrLogModel.Factory(User),
                Fch =DateOnly.FromDateTime( DateTime.Today),
                Contact = new GuidNom(CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid, "consumidor"),
                Src = PurchaseOrderModel.Sources.Marketplace,
                Concept = OrderNum,
                Cod = PurchaseOrderModel.Cods.Customer,
                Norep = true
            };
            foreach (var x in Items)
            {
                var item = new PurchaseOrderModel.Item
                {
                    Qty = x.Qty,
                    Pending = 0, // no units left since delivery is generated right afterwards
                    ChargeCod = PurchaseOrderModel.Item.ChargeCods.chargeable,
                    Price = x.Price == null ? null : Math.Round((decimal)(x.Price * VatFactor), 2),
                    Sku = x.Sku
                };
                retval.Items.Add(item);
                x.PncGuid = item.Guid;
            }
            if(Ports != null && Ports > 0)
            {
                var item = new PurchaseOrderModel.Item
                {
                    Qty = 1,
                    Pending = 0, // no units left since delivery is generated right afterwards
                    ChargeCod = PurchaseOrderModel.Item.ChargeCods.chargeable,
                    Price = Ports,
                    Sku = ProductSkuModel.Wellknown(ProductSkuModel.Wellknowns.Ports)!.Guid
                };
                retval.Items.Add(item);
            }
            return retval;
        }

        public static ShoppingBasketModel FakeBasket(LangDTO lang)
        {
            var user = UserModel.Wellknown(UserModel.Wellknowns.matias)!;
            var marketPlace = MarketPlaceModel.Wellknown(MarketPlaceModel.Wellknowns.Shop4moms);

            var retval = new ShoppingBasketModel(user, marketPlace)
            {
                Lang = lang,
                OrderNum = "V57PV2VAFT",
                FirstName = "Pepito",
                LastName = "FakeCognom1",
                LastName2 = "FakeCognom2",
                Address = "Fake Address",
                Location = "Fake location",
                Country = CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!.Guid,
                TrpObs = "Fake obs",
                Items = new List<ShoppingBasketModel.Item>()
                {
                    new ShoppingBasketModel.Item{
                        Qty=2,
                        Sku=ProductSkuModel.Wellknown(ProductSkuModel.Wellknowns.mamaRoo5grey)!.Guid,
                        Price=200
                    }
                }
            };

            return retval;


        }


        /* Unmerged change from project 'DTO (net7.0-android)'
        Before:
                public string HtmlReport(DTO.Integracions.Shop4moms cache)
        After:
                public string HtmlReport(Shop4moms.Shop4moms cache)
        */

        /* Unmerged change from project 'DTO (net7.0-windows10.0.19041.0)'
        Before:
                public string HtmlReport(DTO.Integracions.Shop4moms cache)
        After:
                public string HtmlReport(Shop4moms cache)
        */
        public string HtmlReport(List<ProductCategoryModel>? categories, List<ProductSkuModel>? skus)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");

            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td>{0}</td>", Lang?.Tradueix("Pedido num.", "Comanda num.", "Order num."));
            sb.AppendFormat("<td style='padding-left:10px;'>{0}</td>", OrderNum);
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td>{0}</td>", Lang.Tradueix("Destinatario", "Destinatari", "Destination"));
            sb.AppendFormat("<td style='padding-left:10px;'>{0}</td>", Fullnom());
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td>{0}</td>", Lang.Tradueix("Dirección", "Adreça", "Address"));
            sb.AppendFormat("<td style='padding-left:10px;'>{0}</td>", Address);
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td>{0}</td>", Lang.Tradueix("Población", "Població", "Location"));
            sb.AppendFormat("<td style='padding-left:10px;'>{0}</td>", ZipLocation());
            sb.AppendLine("</tr>");
            if (!string.IsNullOrEmpty(TrpObs))
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td>", Lang.Tradueix("Observaciones transporte", "Observacions transport", "Comments for transport"));
                sb.AppendFormat("<td style='padding-left:10px;'>{0}</td>", TrpObs);
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");

            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("<tr><td>&nbsp;</td></tr>"); // spacer before itemms

            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");

            sb.AppendLine("<table style='width:100%'>");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<th style='text-align:left;width:100%'>{0}<th>", Lang.Tradueix("Producto", "Producte", "Product", "Produto"));
            sb.AppendFormat("<th style='text-align:right;padding-right:10px;'>{0}<td>", Lang.Tradueix("Unidades", "Unitats", "Units", "Quantidade"));
            sb.AppendFormat("<th style='text-align:right;padding-right:10px;'>{0}<td>", Lang.Tradueix("Precio", "Preu", "Price", "Preço"));
            sb.AppendFormat("<th style='text-align:right;'>{0}<td>", Lang.Tradueix("Importe", "Import", "Amount"));
            sb.AppendLine("</tr>");

            foreach (var item in Items)
            {
                var sku = skus?.FirstOrDefault(x=>x.Guid == item.Sku);
                var category = categories?.FirstOrDefault(x => x.Guid == sku?.Category);
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style=''>{0} {1}<td>", category?.Nom?.Tradueix(Lang!), sku?.Nom?.Tradueix(Lang) ?? "");
                sb.AppendFormat("<td style='text-align:right;padding-right:10px;'>{0:N0}<td>", item.Qty);
                sb.AppendFormat("<td style='text-align:right;padding-right:10px;white-space:nowrap;'>{0:N2} €<td>", item.Price);
                sb.AppendFormat("<td style='text-align:right;white-space:nowrap;'>{0:N2} €<td>", item.Amount());
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td style=''>{0}<td>", Lang.Tradueix("Total"));
            sb.AppendFormat("<td>&nbsp;<td>");
            sb.AppendFormat("<td>&nbsp;<td>");
            sb.AppendFormat("<td style='text-align:right;white-space:nowrap;'>{0:N2} €<td>", Cash());
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            var retval = sb.ToString();
            return retval;
        }

        public bool IsSpain() => Country != null && new CountryModel((Guid)Country).IsSpain();
        public bool IsPortugal() => Country != null && new CountryModel((Guid)Country).IsPortugal();
        public bool IsCanarias() => IsSpain() && !string.IsNullOrEmpty(ZipCod) && (ZipCod.StartsWith("35") || ZipCod.StartsWith("38"));
        public bool IsCeuta() => IsSpain() && !string.IsNullOrEmpty(ZipCod) && ZipCod.StartsWith("51");
        public bool IsMelilla() => IsSpain() && !string.IsNullOrEmpty(ZipCod) && ZipCod.StartsWith("52");
        public bool IsPortugalInsular() => IsPortugal() && !string.IsNullOrEmpty(ZipCod) && ZipCod.StartsWith("9");
        public bool DevengaIva() => IsSpain() && !IsCanarias() && !IsCeuta() && !IsMelilla();

        public bool NoShipmentsToThisAddress()=> IsCanarias() || IsCeuta() || IsMelilla() || IsPortugalInsular();

    }
}

