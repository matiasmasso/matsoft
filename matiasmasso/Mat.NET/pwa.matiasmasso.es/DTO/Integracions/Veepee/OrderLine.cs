using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Veepee
{
    public class OrderLine
    {
        public string? Batch { get; set; }
        public string? OrderNumber { get; set; }
        public string? DeliveryOrder { get; set; }
        public string? ParcelNumber { get; set; }
        public string? Transporter { get; set; }
        public string? DestinationAddress { get; set; }
        public string? ProductNumber { get; set; }
        public string? Ean { get; set; }
        public string? SupplierRef { get; set; }
        public string? ProductLabel { get; set; }
        public int Qty { get; set; }
        public int QtyAssigned { get; set; }
        public int LabeledQty { get; set; }
        public int SentQty { get; set; }
        public int StockOutQty { get; set; }
        public string? Weight { get; set; }
        public ProductSkuModel? Sku { get; set; }

        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public bool Accepted { get; set; }

        public PurchaseOrderModel? PurchaseOrder { get; set; }
        public PurchaseOrderModel.Item? PurchaseOrderItem { get; set; }
        public DeliveryModel? Delivery { get; set; }
        public DeliveryModel.Item? DeliveryItem { get; set; }
        public TransmissionModel? Transmission { get; set; }
    }
}
