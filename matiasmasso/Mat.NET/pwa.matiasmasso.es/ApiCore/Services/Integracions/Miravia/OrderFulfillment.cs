using Api.Entities;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System;

namespace Api.Services.Integracions.Miravia
{
    public class OrderFulfillment
    {

        public static ConsumerTicketModel? Register(DTO.Integracions.Miravia.Order basket)
        {
            ConsumerTicketModel? ticket = null;
            using (var db = new MaxiContext())
            {
                var emp = EmpModel.EmpIds.MatiasMasso;
                var lang = LangDTO.Esp();
                var miravia = MarketPlaceModel.Wellknown(MarketPlaceModel.Wellknowns.Miravia)!;

                UserModel? user;
                var emailAddress = basket.Items
                    .Where(x => !string.IsNullOrEmpty(x.DigitalDeliveryInfo))
                    .Select(x => x.DigitalDeliveryInfo)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(emailAddress))
                {
                    user = UserModel.Wellknown(UserModel.Wellknowns.info);
                } else
                    user = UserService.FromEmailAddress((int)emp, emailAddress);

                if(user == null)
                {
                    user = new UserModel
                    {
                        Emp = emp,
                        EmailAddress = emailAddress,
                        Rol = UserModel.Rols.lead,
                        Nom = basket.CustomerFirstName,
                        Cognoms = basket.CustomerLastName,
                        Country = CountryDTO.Wellknown(CountryDTO.Wellknowns.Spain),
                        Source = UserModel.Sources.marketPlace,
                        Zip = new ZipModel(basket.ZipGuid),
                        Lang = lang
                    };
                    UserService.Update(user);
                }

                var liquid = Decimal.Parse(basket.Price ?? "0", System.Globalization.CultureInfo.InvariantCulture);
                var transport = Decimal.Parse(basket.ShippingFeeOriginal ?? "0", System.Globalization.CultureInfo.InvariantCulture);
                var tipusComisio = 15; //TO DO: take from Miravia properties
                var comisio = Math.Round((liquid+transport) * tipusComisio / 100, 2);
                var cca = CcaModel.Factory(emp, user, CcaModel.CcdEnum.AlbaraBotiga);
                var ctas = PgcCtasService.GetValues(db);
                var ctaMarketPlaces = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.Marketplaces)!;
                var ctaComisio = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.FrasComisionsPendentsDeRebre)!;
                var ctaTransport = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.FrasTransportPendentsDeRebre)!;
                var ctaTransportSubvencio = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.AbonamentsPendentsDeRebre)!;
                var ctaClients = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.Clients)!;
                var consumer = CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!;
                var mgz = MgzModel.Default() == null ? null : new GuidNom(MgzModel.Default()!.Guid, MgzModel.Default()!.Abr);

                var purchaseOrder = PurchaseOrderFactory(basket);
                var delivery = new DeliveryModel
                {
                    Emp = emp,
                    Fch = DateTime.Now,
                    Cod = purchaseOrder.Cod,
                    Contact = purchaseOrder.Contact,
                    PortsCod = CustomerModel.PortsCodes.reculliran,
                    CashCod = CustomerModel.CashCodes.credit,
                    Nom = basket.AddressShipping?.FullNom(),
                    Address = new AddressModel { Text = basket.AddressShipping.Address1, Zip = new ZipDTO(basket.ZipGuid) },
                    Deutor = miravia.Guid,
                    ExportCod = ZonaModel.ExportCods.National,
                    Incoterm = "EXW",
                    Mgz = mgz,
                    //ObsTransp = basket.TrpObs ?? "",
                    //Fpg = tpvLog.FormaDePago(),
                    UsrLog = purchaseOrder.UsrLog,
                    Amt = new Amt(liquid),
                    Items = purchaseOrder.Items.Select(x => new DeliveryModel.Item
                    {
                        Qty = x.Qty,
                        Price = x.Price,
                        Dto = x.Dto,
                        Sku = x.Sku,
                        PncGuid = x.Guid,
                        PdcGuid = purchaseOrder.Guid,
                        MgzGuid = mgz?.Guid
                    }).ToList(),
                    AttachedDocs = basket.ShipmentLabels
                };

                //build and save the consumer ticket
                 ticket = ConsumerTicketModel.Factory( EmpModel.EmpIds.MatiasMasso, (Guid)basket.Operator!, lang, miravia, basket.OrderId);
                ticket.Nom = basket.AddressShipping.FirstName;
                ticket.Cognom1 = basket.AddressShipping.LastName;
                ticket.Address = basket.AddressShipping.Address1;
                ticket.ConsumerZip = basket.AddressShipping.PostCode;
                ticket.ConsumerLocation = basket.AddressShipping.Address3;
                ticket.Zip = basket.ZipGuid;
                ticket.Cca = new GuidNom(cca.Guid, cca.Concept);
                ticket.PurchaseOrder = new GuidNom(purchaseOrder.Guid);
                ticket.Delivery = new GuidNom(delivery.Guid);
                ticket.BuyerEmail = basket.Items?.FirstOrDefault()?.DigitalDeliveryInfo;
                ticket.Goods = liquid;
                ticket.Comision = comisio;
                ConsumerTicketService.Update(db, ticket);

                //register address
                var adr = new AddressModel
                {
                    Contact = ticket.Guid,
                    Cod = AddressModel.Cods.Fiscal,
                    Text = basket.AddressShipping.Address1,
                    ZipGuid = basket.ZipGuid
                };
                AddressService.Update(db, adr);

                //build and save purchase order
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

                //deliver the order
                DeliveryService.Update(db, delivery);

                //build and save accounts registry
                cca.Concept = ($"Despeses ticket {ticket.Id} de {basket.AddressShipping.NomAndLocation()} per Miravia").TruncateWithEllipsis(60);
                cca.AddDebit(comisio, ctaComisio, miravia.Guid);
                cca.AddDebit(transport, ctaTransport, miravia.Guid); //carrec per transport (shippingoriginal, no esta clar si es aquest
                cca.AddCredit(transport, ctaTransportSubvencio, miravia.Guid); //subvencio
                cca.AddSaldo(ctaClients, miravia.Guid);
                CcaService.Update(db, cca);

                db.SaveChanges();
            }

            return ticket;
        }

        public static PurchaseOrderModel PurchaseOrderFactory(DTO.Integracions.Miravia.Order basket)
        {
            var emp = EmpModel.EmpIds.MatiasMasso;
            var user = UserModel.Wellknown(UserModel.Wellknowns.info); // our corporate email since Miravia does not provide the consumer email
            decimal VatTipus = 21;
            decimal VatFactor = 100 / (100 + VatTipus);


            var retval = new PurchaseOrderModel()
            {
                Emp = emp,
                UsrLog = UsrLogModel.Factory(user),
                Fch = DateOnly.FromDateTime(DateTime.Now),
                Contact = new GuidNom(CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid, "consumidor"),
                Src = PurchaseOrderModel.Sources.Marketplace,
                Concept = basket.OrderNumber,
                Cod = PurchaseOrderModel.Cods.Customer,
                Norep = true
            };
            foreach (var x in basket.Items)
            {
                var item = new PurchaseOrderModel.Item
                {
                    Qty = 1,
                    Pending = 0, // no units left since delivery is generated right afterwards
                    ChargeCod = PurchaseOrderModel.Item.ChargeCods.chargeable,
                    Price = x.PaidPrice == null ? null : Math.Round((decimal)((decimal)x.PaidPrice * VatFactor), 2),
                    Sku = x.SkuGuid
                };
                retval.Items.Add(item);
                x.PncGuid = item.Guid;
            }
            //if (Ports != null && Ports > 0)
            //{
            //    var item = new PurchaseOrderModel.Item
            //    {
            //        Qty = 1,
            //        Pending = 0, // no units left since delivery is generated right afterwards
            //        ChargeCod = PurchaseOrderModel.Item.ChargeCods.chargeable,
            //        Price = Ports,
            //        Sku = ProductSkuModel.Wellknown(ProductSkuModel.Wellknowns.Ports)!.Guid
            //    };
            //    retval.Items.Add(item);
            //}
            return retval;
        }

    }
}
