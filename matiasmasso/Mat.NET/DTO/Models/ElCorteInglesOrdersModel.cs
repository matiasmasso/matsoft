using System;

namespace DTO.Models
{
    public class ElCorteInglesOrderModel
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public string ECINum { get; set; }
        public string Depto { get; set; }
        public string Centre { get; set; }
        public decimal Eur { get; set; }
        public DTOPurchaseOrder.ShippingStatusCods ShippingStatus { get; set; }
    }
}
