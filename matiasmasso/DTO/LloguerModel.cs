using DocumentFormat.OpenXml.Office.CoverPageProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LloguerModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds EmpId { get; set; }
        public ContactModel? Customer { get; set; }
        public ImmobleModel? Immoble { get; set; }

        public Cods? Cod { get; set; }
        public ContractModel? Contract { get; set; }
        public Decimal? Base { get; set; }
        public bool? Iva { get; set; }
        public bool? Irpf { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }

        public enum Cods { 
            Vivenda,
            Local,
            Parking
        }

        public LloguerModel() : base() { }
        public LloguerModel(Guid guid) : base(guid) { }

        public class Quota : BaseGuid, IModel
        {
            public LloguerModel? Lloguer { get; set; }
            public DateOnly? Fch { get; set; }
            public InvoiceSentModel? Invoice { get; set; }

            public Quota() : base() { }
            public Quota(Guid guid) : base(guid) { }
        }

    }
}
