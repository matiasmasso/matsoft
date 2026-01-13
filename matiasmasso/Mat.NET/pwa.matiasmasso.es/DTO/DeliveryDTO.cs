using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class DeliveryModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public PurchaseOrderModel.Cods Cod { get; set; }
        public GuidNom? Contact { get; set; } // to deprecate
        public Guid? ContactGuid { get; set; }
        public Amt? Amt { get; set; }
        public GuidNom? Invoice { get; set; }
        public Guid? Deutor { get; set; }
        public GuidNom? Transmission { get; set; }
        public GuidNom? Mgz { get; set; }
        public string? Nom { get; set; }
        public AddressModel? Address { get; set; }
        public string? Tel { get; set; }
        public string? Incoterm { get; set; }

        public bool Req { get; set; }

        public ZonaModel.ExportCods? ExportCod { get; set; }

        public CustomerModel.PortsCodes PortsCod { get; set; }
        public CustomerModel.CashCodes CashCod { get; set; }

        public RetencioCods RetencioCod { get; set; }


        public string? Fpg { get; set; }
        public string? Obs { get; set; }
        public string? ObsTransp { get; set; }
        public DTO.Integracions.Vivace.Trace? Trace { get; set; }

        public List<Item> Items { get; set; } = new();
        public List<PurchaseOrderClass> PurchaseOrders { get; set; } = new();
        public List<DocfileModel> AttachedDocs { get; set; } = new();
        public UsrLogModel? UsrLog { get; set; } = new();

        public enum RetencioCods
        {
            notSet = -1,
            free = 0,
            altres = 1,
            transferencia = 2,
            VISA = 3
        }

        public DeliveryModel() : base() { }
        public DeliveryModel(Guid guid) : base(guid) { }

        public static DeliveryModel Factory(EmpModel.EmpIds emp, UserModel user, PurchaseOrderModel.Cods cod, DateTime? fch = null)
        {
            DeliveryModel retval = new DeliveryModel();
            retval.Emp = emp;
            retval.Cod = cod;
            retval.Fch = fch ?? DateTime.Now;
            retval.UsrLog = UsrLogModel.Factory(user);
            return retval;
        }

        public Item AddItem(PurchaseOrderModel.Item item)
        {
            var retval = new Item
            {
                Qty = item.Qty,
                Price = item.Price,
                Dto = item.Dto,
                Sku = item.Sku,
                PncGuid = item.Guid
            };
            Items.Add(retval);
            return retval;
        }

        public bool HasDiscount() => Items.Any(x => x.Dto != null && x.Dto != 0);
        public decimal BaseImponible() => Items.Sum(x => x.Amt());
        public List<TaxModel> Vats(List<TaxModel> allTaxes) => TaxModel.Closest(allTaxes, Fch.ToDateOnly());

        public  List<TaxModel.TipusBaseQuota> GetIvaBaseQuotas( List<TaxModel> Taxes)
        {
            List<TaxModel.TipusBaseQuota> retval = new List<TaxModel.TipusBaseQuota>();
            if (HasIva())
            {
                foreach (DeliveryModel.Item oItm in Items)
                {
                    TaxModel.TipusBaseQuota? oIvaBaseQuota = retval.FirstOrDefault(x => x.Tax.Codi == oItm.IvaCod);
                    if (oIvaBaseQuota == null)
                    {
                        TaxModel oTax = Taxes.First(x => x.Codi == oItm.IvaCod);
                        oIvaBaseQuota = new TaxModel.TipusBaseQuota(oTax, new Amt(oItm.Amt()));
                        retval.Add(oIvaBaseQuota);
                        if (HasReq())
                        {
                            TaxModel.Codis oTaxReqCodi = TaxModel.ReqForIvaCod(oIvaBaseQuota.Tax.Codi);
                            TaxModel oTaxReq = Taxes.First(x => x.Codi == oTaxReqCodi);
                            TaxModel.TipusBaseQuota oReqBaseQuota = new TaxModel.TipusBaseQuota(oTaxReq, new Amt(oItm.Amt()));
                            retval.Add(oReqBaseQuota);
                        }
                    }
                    else
                    {
                        oIvaBaseQuota.AddBase(new Amt(oItm.Amt()));
                        if (HasReq())
                        {
                            TaxModel.Codis oTaxReqCodi = TaxModel.ReqForIvaCod(oIvaBaseQuota.Tax.Codi);
                            TaxModel.TipusBaseQuota oReqBaseQuota = retval.First(x => x.Tax.Codi == oTaxReqCodi);
                            oReqBaseQuota.AddBase(new Amt(oItm.Amt()));
                        }
                    }
                }
            }
            return retval;
        }

        public bool IsConsumer() => Contact!.Guid == CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid;

        public bool HasIva() => ExportCod == ZonaModel.ExportCods.National;

        public bool HasReq() => HasIva() && Req;
        public decimal VatAmt(List<TaxModel>? allTaxes)
        {
            decimal retval = 0;
            if(allTaxes != null)
            {
                var taxes = Vats(allTaxes);
                var quotas = GetIvaBaseQuotas(allTaxes);
                foreach (var quota in quotas)
                {
                    retval += quota.Quota.Eur ?? 0;
                }
            }
            return retval;
        }

        public decimal Cash(List<TaxModel> allTaxes) => BaseImponible() + VatAmt(allTaxes);


        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Contact?.Nom ?? "" + " " + Id.ToString() + " " + Nom ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }


        public string PageUrl() => Globals.PageUrl("delivery", Guid.ToString());

        public string DownloadUrl() => Globals.PageUrl("delivery/pdf", Guid.ToString());

        public string Formatted() => string.Format("{0:0000}{1:000000}", Fch.Year, Id);

        public string Caption(LangDTO lang) => $"{lang.Tradueix("Albarán","Albarà","Delivery note")} {Id}/{Fch.Year}";
        public override string ToString() => $"alb.{Id}"; // del {Fch:dd/MM/yy} {Nom}";

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public class Item : BaseGuid, IModel
        {
            public int? Lin { get; set; }
            public int? Qty { get; set; }
            public decimal? Price { get; set; }
            public decimal? Dto { get; set; }
            public decimal Amt() => Math.Round((Qty ?? 0) * (Price ?? 0) * (100 - (Dto ?? 0)) / 100, 2);
            public Guid? Sku { get; set; }
            public Guid? PdcGuid { get; set; }
            public Guid? PncGuid { get; set; }
            public Guid? MgzGuid { get; set; }
            public PurchaseOrderModel.Item.ChargeCods ChargeCod { get; set; }
            public TaxModel.Codis IvaCod { get; set; } = TaxModel.Codis.iva_Standard;

            public DeliveryModel? Parent { get; set; } //just for UI component


            public enum Cods
            {
                Sale = 51
            }

            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }


            public decimal UnitPriceVatIncluded(List<TaxModel> allTaxes, bool hasIva, bool hasReq = false)
            {
                var ivaBaseQuotas = IvaBaseQuotas(allTaxes, hasIva, hasReq);
                var retval = Price ?? 0;
                foreach(var value in ivaBaseQuotas)
                {
                    retval += value.Quota?.Eur ?? 0;
                }
                return retval;
            }

            public decimal AmtVatIncluded(List<TaxModel> allTaxes, bool hasIva, bool hasReq = false)
            {
                return (Qty ?? 0) * UnitPriceVatIncluded(allTaxes, hasIva, hasReq);
            }

            public List<TaxModel.TipusBaseQuota> IvaBaseQuotas(List<TaxModel> allTaxes, bool hasIva, bool hasReq = false)
            {
                List<TaxModel.TipusBaseQuota> retval = new();
                if (hasIva)
                {
                    TaxModel oTax = allTaxes.First(x => x.Codi == IvaCod);
                    var oIvaBaseQuota = new TaxModel.TipusBaseQuota(oTax, new Amt(Amt()));
                    retval.Add(oIvaBaseQuota);
                    if (hasReq)
                    {
                        TaxModel.Codis oTaxReqCodi = TaxModel.ReqForIvaCod(oIvaBaseQuota.Tax.Codi);
                        TaxModel oTaxReq = allTaxes.First(x => x.Codi == oTaxReqCodi);
                        TaxModel.TipusBaseQuota oReqBaseQuota = new TaxModel.TipusBaseQuota(oTaxReq, new Amt(Amt()));
                        retval.Add(oReqBaseQuota);
                    }
                }
                return retval;
            }


        }

        public PurchaseOrderClass? PurchaseOrder(Item item)=>PurchaseOrders.Where(x=>x.Guid == item.PdcGuid).FirstOrDefault();

        public class PurchaseOrderClass:BaseGuid
        {
            public DateOnly Fch { get; set; }
            public string? Concept { get; set; }

            public PurchaseOrderClass() : base() { }
            public PurchaseOrderClass(Guid guid) : base(guid) { }

            public string FullConcept(LangDTO lang)
            {
                var prefix = lang.Tradueix("Su pedido", "La seva comanda", "Your purchase order");
                var from = lang.Tradueix("del", "del", "from");
                return $"{prefix} {Concept} {from} {Fch:dd/MM/yy}";
            }

        }

        public class List
        {
            public List<int> Years { get; set; } = new();
            public List<DeliveryModel> Items { get; set; } = new();
        }
    }
}
