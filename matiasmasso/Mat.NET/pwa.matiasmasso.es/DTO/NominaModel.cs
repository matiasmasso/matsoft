using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTO
{
    public class NominaModel : BaseGuid, IModel
    {
        public CcaModel? Cca { get; set; }
        public Decimal? Devengat { get; set; } = 0;
        public Decimal? Dietes { get; set; } = 0;
        public Decimal? SegSocial { get; set; } = 0;
        public Decimal? IrpfBase { get; set; } = 0;
        public Decimal? Irpf { get; set; } = 0;
        public Decimal? Embargos { get; set; } = 0;
        public Decimal? Deutes { get; set; } = 0;
        public Decimal? Anticips { get; set; } = 0;

        public Decimal? Liquid { get; set; } = 0;
        public Guid? Staff { get; set; }

        public string? IbanDigits { get; set; }

        public List<Item> Items { get; set; } = new();

        public string PdfUrl() => Globals.RemoteApiUrl("staff/nomina/pdf", base.Guid.ToString());

        public NominaModel() : base()
        {
            Cca = new CcaModel();
            Guid = Cca.Guid;
        }
        public NominaModel(Guid guid) : base(guid)
        {
            Cca = new CcaModel(guid);
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public class Item
        {
            public Concepte? Concepte { get; set; }
            public int? Qty { get; set; }
            public decimal? Price { get; set; }
            public decimal? Devengo { get; set; }
            public decimal? Deduccio { get; set; }

            public Item(Concepte? oConcepte) : base()
            {
                Concepte = oConcepte;
            }

            public decimal? Import
            {
                get
                {
                    decimal? retval = null;
                    if (Price != null)
                        retval = Qty * Price;
                    return retval;
                }
            }

            public override string ToString()
            {
                return Concepte?.Name ?? "?";
            }
        }

        public class Concepte
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Concepte(int iId, string sName = "") : base()
            {
                Id = iId;
                Name = sName;
            }
        }
    }
}
