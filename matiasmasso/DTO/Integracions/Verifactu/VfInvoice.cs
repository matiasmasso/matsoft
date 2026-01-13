using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Verifactu
{


    public class VfInvoice
    {
        public string ServiceKey { get; set; }
        public string Status { get; set; } = "POST";
        public string InvoiceType { get; set; } = VfInvoiceTypes.F1.ToString();
        public string InvoiceID { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public string SellerID { get; set; } 
        public string CompanyName { get; set; } 
        public string RelatedPartyID { get; set; }
        public string RelatedPartyName { get; set; }
        public string Text { get; set; }
        public List<VfTaxItem> TaxItems { get; set; }

        public enum VfInvoiceTypes
        {
            F1, // Factura
            F2, // Factura simplificada
            F3, // Nota de crédito
            F4  // Nota de débito
        }
        public VfInvoice()
        {
            ServiceKey = "bWF0aWFzQG1hdGlhc21hc3NvLmVzOjg4Lml1eXZZVC44OA==";
            TaxItems = new List<VfTaxItem>();
        }

        public void InsertTaxItem(VfTaxItem item)
        {
            TaxItems.Add(item);
        }

        // Métodos simulados para integración con AEAT
        public string GetUrlValidate()
        {
            // Devuelve una URL para validar la factura con la AEAT (simulada)
            return $"https://verifactu.aeat.es/validate/{InvoiceID}";
        }

        public string GetRegistroAlta()
        {
            // Devuelve XML de alta de factura (simplificado)
            return $"<Factura><ID>{InvoiceID}</ID><Fecha>{InvoiceDate}</Fecha></Factura>";
        }

        public string GetRegistroAnulacion()
        {
            // Devuelve XML de anulación de factura (simplificado)
            return $"<Anulacion><ID>{InvoiceID}</ID></Anulacion>";
        }

        // Simulación de envío y anulación
        public VfInvoiceResult Send()
        {
            return new VfInvoiceResult { ResultCode = 0, ResultMessage = "Factura enviada correctamente" };
        }

        public VfInvoiceResult Delete()
        {
            return new VfInvoiceResult { ResultCode = 0, ResultMessage = "Factura anulada correctamente" };
        }
    }
}
