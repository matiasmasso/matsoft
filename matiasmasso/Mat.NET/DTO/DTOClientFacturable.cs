using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOClientFacturable
    {
        public DTOCustomer Customer { get; set; }
        public List<DTOInvoice> Invoices { get; set; }
        public List<DTODelivery> AlbaransPerFacturar { get; set; }
        public bool Facturable { get; set; }


        public DTOClientFacturable(DTOCustomer oCustomer = null/* TODO Change to default(_) if this is not a reference type */) : base()
        {
            Customer = oCustomer;
            Invoices = new List<DTOInvoice>();
            AlbaransPerFacturar = new List<DTODelivery>();
        }

        public DTOAmt Total()
        {
            decimal DcEur = Invoices.SelectMany(x => x.Deliveries).Sum(y => y.Import.Eur);
            var retval = DTOAmt.Factory(DcEur);
            return retval;
        }
    }
}
