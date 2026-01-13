using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOImportPrevisio : DTOBaseGuid
    {
        public DTOImportacio Importacio { get; set; }
        public string NumComandaProveidor { get; set; }
        public int Lin { get; set; }
        public string SkuRef { get; set; }
        public DTOProductSku Sku { get; set; }
        public string SkuNom { get; set; }
        public int Qty { get; set; }
        public DTOPurchaseOrderItem PurchaseOrderItem { get; set; }
        public DTOInvoiceReceived.Item InvoiceReceivedItem { get; set; }

        public List<ValidationErrors> errors { get; set; }

        public enum ValidationErrors
        {
            skuNotFound,
            orderNotFound
        }

        public enum ValidationSolutions
        {
            selectSku,
            selectOrder
        }

        public DTOImportPrevisio() : base()
        {
            errors = new List<ValidationErrors>();
        }

        public DTOImportPrevisio(Guid oGuid) : base(oGuid)
        {
            errors = new List<ValidationErrors>();
        }
    }
}
