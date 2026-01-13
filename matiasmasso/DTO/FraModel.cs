using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FraModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public int Serie { get; set; }
        public int FraNum { get; set; }

        public DateOnly Fch { get; set; }
        public DateOnly Vto { get; set; }
        public Guid CliGuid { get; set; }
        public string? Nom { get; set; }
        public string? Address { get; set; }

        public Guid? ZipGuid { get; set; }
        public LangDTO? Lang { get; set; }
        public string? Nif { get; set; }
        public int? NifCod { get; set; }

        public decimal? BaseImponible { get; set; }
        public decimal? IVApct { get; set; }
        public decimal? IRPFpct { get; set; }
        public Guid? CcaGuid { get; set; }
        public CcaModel? Cca { get; set; }

        public FraModel() : base() { }
        public FraModel(Guid guid) : base(guid) { }

        public decimal IVA => Math.Round((BaseImponible ?? 0) * (IVApct ?? 0) / 100, 2);
        public decimal IRPF => Math.Round((BaseImponible ?? 0) * (IRPFpct ?? 0) / 100, 2);
        public decimal Total => (BaseImponible ?? 0) + IVA - IRPF;

        public bool Matches(decimal? searchterm)
        {
            var retval = false;

            if (searchterm == null)
                retval = true;
            else
                retval = Total == searchterm;
            return retval;
        }
    }
}
