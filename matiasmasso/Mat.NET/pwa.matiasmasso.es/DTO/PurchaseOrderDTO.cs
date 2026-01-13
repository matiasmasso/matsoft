using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{

    public class PurchaseOrderModel:BaseGuid,IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public int? Id { get; set; }
        public Cods Cod { get; set; } = Cods.NotSet;

        public PurchaseOrderModel.Sources? Src { get; set; }
        public DateOnly? Fch { get; set; }
        public DateTime? FchCreated { get; set; }
        public GuidNom? UsrCreated { get; set; }
        public GuidNom? Contact { get; set; }
        public string? Concept { get; set; }
        public Amt? Amt { get; set; }

        public bool Norep { get; set; }

        public UsrLogModel? UsrLog { get; set; } = new();

        public string? Hash { get; set; }

        public List<Item> Items { get; set; } = new();

        public enum Cods
        {
            NotSet,
            Supplier,
            Customer
        }

        public enum Sources
        {
            no_Especificado,
            telefonico,
            fax,
            eMail,
            representante,
            representante_por_Web,
            cliente_por_Web,
            matPocket,
            fira,
            cliente_XML,
            edi,
            garantia,
            iPhone,
            cliente_por_WebApi,
            ExcelMayborn,
            Marketplace
        }
        public PurchaseOrderModel() : base() { }
        public PurchaseOrderModel(Guid guid) : base(guid) { }

        public static PurchaseOrderModel Factory(UserModel user, Guid customer)
        {
            return new PurchaseOrderModel
            {
                Emp = user.Emp,
                Fch = DateOnly.FromDateTime(DateTime.Now),
                Cod = Cods.Customer,
                Contact = new GuidNom(customer),
                UsrLog = UsrLogModel.Factory(user)
            };
        }

        public bool IsEmpty() => Items.Count == 0;

        public decimal Sum() => Items.Sum(x => x.Amt());

        public bool HasDiscount() => Items.Any(x => x.Dto != null && x.Dto != 0);

        public new string ToString() => string.Format("{0} Pedido {1} del {2:dd/MM/yy} {3}", Contact?.Nom, Id, Fch, Concept);


        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Contact?.Nom ?? "" + " " + Id.ToString() + " " + Concept ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public class Item:BaseGuid
        {
            public int Lin { get; set; }
            public int Qty { get; set; }
            public int Pending { get; set; }
            public decimal? Price { get; set; }
            public decimal? Dto { get; set; }
            public ChargeCods ChargeCod { get; set; } = ChargeCods.chargeable;

            //public PurchaseOrderModel? PurchaseOrder { get; set; }

            //public PurchaseOrderModel.Item PurchaseOrderItem {get; set; }

            public enum ChargeCods
            {
                chargeable,
                FOC // free of charge
            }

            public decimal Amt()
            {
                decimal retval = 0;
                if(Qty != 0 && Price != null )
                {
                    var dto = Dto ?? 0;
                    retval = Math.Round(Qty * (decimal)Price * (100 - dto) / 100, 2);
                }
                    
                return retval;
            }
            public Guid? Sku { get; set; }

            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }
        }

        /// <summary>
        /// Data needed for new purchase orders
        /// </summary>
        public class Resources
        {
            public Guid? SelectedDestination { get; set; }
            public List<Guid>? Destinations { get; set; }
            public List<Guid>? AvailableDestinations { get; set; }
            public List<CustomerPortfolioModel>? CustomerPortfolio { get; set; }
            public List<ImplicitDiscountModel>? CustomerDtosOnRrpp { get; set; }
            public List<GuidDecimal>? CustomPricelist { get; set; }
        }

    }

    public class PurchaseOrderListDTO
    {
        public List<int> Years{get; set; } = new();
        public List<Item> Items { get; set; } = new();

        public PurchaseOrderListDTO():base() { }
        public class Item
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public DateOnly Fch { get; set; }
            public GuidNom? Contact { get; set; }
            public string? Concept { get; set; }
            public Amt? Amt { get; set; }
            public string? Hash { get; set; }

            public int Src { get; set; }
            public string? User { get; set; }

            public Item():base(){}


            public bool HasDoc() => !string.IsNullOrEmpty(Hash);
            public bool Matches(string? searchTerm)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    var searchTarget = Contact?.Nom ?? "" + " " + Concept ?? "";
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }

            public string PageUrl() => Globals.PageUrl("purchaseOrder", Guid.ToString());

            public string DownloadUrl() => Globals.PageUrl("purchaseOrder/pdf", Guid.ToString());

            public new string ToString() => string.Format("DTO.PurchaseOrderListDTO.Item: {0} Pedido {1} del {2:dd/MM/yy} {3}", Contact?.Nom ?? "", Id, Fch, Concept);

        }


    }

}
